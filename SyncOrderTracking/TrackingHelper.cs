using Dapper;
using SyncOrderTracking.BLL;
using SyncOrderTracking.Models;
using SyncOrderTracking.ru.dpd.wstest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SyncOrderTracking
{
    public class TrackingHelper
    {
        public static int mainTime;
        public static int mainAGid;
        public static string mainAGname;
        public static int mainOverTime;
        public static string mainSynSign;
        public static string ProcessName;
        /// <summary>
        /// 同步运单的最新状态
        /// </summary>
        public static void DoSyncTrackStates()
        {
            //获取指定商业伙伴同步的时间间隔和服务开启的状态
            string strSql = "SELECT bit_synOpen,int_synSpacing FROM tb_Agent WHERE int_AGid=" + mainAGid.ToString();
            Agent agent = SQLHelper.GetObject<Agent>(strSql).FirstOrDefault();
            int? intSpacing = 60;
            if (agent == null)
                return;
            intSpacing = agent.int_synSpacing;

            IList<SFITrackNum> tracks = new List<SFITrackNum>();

            string strRes = "";
            int intNew = 0;
            int intError = 0;
            int intCompleted = 0;
            //DateTime.

            long intProcessCode = DateTimeManger.DateDiff(DateInterval.Minute, DateTime.Now.AddDays(-3), DateTime.Now);
            string strSysCode = DateTime.Now.ToString("yyyyMMddhhmmss");
            string logMsg = string.Empty;
            //记录系统运行的日志
            logMsg = $"{strSysCode}-Agent:{mainAGname} 检查点数据刷新服务启动{ProcessName}";
            SQLHelper.ExecuteStoredProcedure(Constants.sp_AddSystemLog, new { mainAGid = mainAGid, result = logMsg });
            try
            {
                if (!SQLHelper.ExecuteStoredProcedure(Constants.sp_Update_SFI_TrackNum, new { intProcessCode = intProcessCode, mainSynSign = mainSynSign, mainAGid = mainAGid, intSpacing = intSpacing, mainOverTime = mainOverTime }))
                {
                    logMsg = $"{strSysCode}-Agent:{ mainAGname} 没有需要刷新的数据{ProcessName}";
                    SQLHelper.ExecuteStoredProcedure(Constants.sp_AddSystemLog, new { mainAGid = mainAGid, result = logMsg });
                    return;
                }

                tracks = SQLHelper.GetObjectByProc<SFITrackNum>(Constants.sp_Get_SFI_TrackNum, new { intProcessCode = intProcessCode, mainSynSign = mainSynSign }).ToList();
                if (tracks == null | tracks.Count == 0)
                {
                    logMsg = $"{strSysCode}-Agent:{mainAGname} 没有需要刷新的数据{ProcessName}";
                    SQLHelper.ExecuteStoredProcedure(Constants.sp_AddSystemLog, new { mainAGid = mainAGid, result = logMsg });
                    return;
                }
                logMsg = $" {strSysCode}-Agent:{mainAGname} 开始获取数据 - { tracks.Count}{ProcessName}";
                SQLHelper.ExecuteStoredProcedure(Constants.sp_AddSystemLog, new { mainAGid = mainAGid, result = logMsg });
                foreach (SFITrackNum track in tracks)
                {
                    try
                    {
                        //通过dpd单号去获取运单的状态信息
                        IList<TrackRecord> records = GetDPDInfo(track.vchar_AGnum);
                        intNew = 0;
                        intError = 0;
                        if (records.Count == 0)
                        {
                            #region 没有获取到运单的状态信息
                            strRes = WriteServerLog(track, "0|0|0|", $"未获取导数据，请尝试手工获取{ProcessName}");
                            #endregion
                        }
                        else
                        {
                            #region 获取到运单的状态信息
                            foreach (TrackRecord record in records)
                            {
                                strRes = UpdateTrackRecord(track, record);
                                if (strRes == "N")
                                {
                                    intNew += 1;
                                }
                                else if (strRes == "F")
                                {
                                    intError += 1;
                                }
                            }
                            strRes = WriteServerLog(track, $"{records.Count}|{intNew}|{intError}|", string.Empty);
                            #endregion
                        }
                        SQLHelper.ExecuteStoredProcedure(Constants.sp_Update_SFI_TrackNumByID, new { int_Mappingid = track.int_Mappingid });
                        intCompleted += 1;
                    }
                    catch (Exception ex)
                    {
                        strRes = WriteServerLog(track, "0|0|0|", ex.Message);
                        SQLHelper.ExecuteStoredProcedure(Constants.sp_Update_SFI_TrackNumByID, new { int_Mappingid = track.int_Mappingid });
                    }
                }
            }
            catch (Exception ex)
            {
                logMsg = $"{strSysCode}-Agent:{mainAGname} 服务运行错误：{ex.Message} {ProcessName}";
                SQLHelper.ExecuteStoredProcedure(Constants.sp_AddSystemLog, new { mainAGid = mainAGid, result = logMsg });
                if (tracks.Count != 0)
                {
                    foreach (SFITrackNum track in tracks)
                    {
                        SQLHelper.ExecuteStoredProcedure(Constants.sp_Update_SFI_TrackNumByID, new { int_Mappingid = track.int_Mappingid });
                    }
                }
                return;
            }
            if (tracks.Count != 0)
            {
                foreach (SFITrackNum track in tracks)
                {
                    SQLHelper.ExecuteStoredProcedure(Constants.sp_Update_SFI_TrackNumByID, new { int_Mappingid = track.int_Mappingid });
                }
            }
            logMsg = $"{strSysCode}-Agent:{mainAGname} 服务运行完成 - {intCompleted}{ProcessName}";
            SQLHelper.ExecuteStoredProcedure(Constants.sp_AddSystemLog, new { mainAGid = mainAGid, result = logMsg });
        }
        /// <summary>
        /// 调用DPD接口获取最新运单状态信息
        /// </summary>
        /// <param name="dpdNum"></param>
        /// <returns></returns>
        private static IList<TrackRecord> GetDPDInfo(string dpdNum)
        {
            #region call dpd service to get the tracking states
            try
            {
                ParcelTracingService parcelTracingService = new ParcelTracingService();
                requestDpdOrder rstDpdOrder = new requestDpdOrder()
                {
                    auth = new auth() { clientKey = ConfigurationManager.AppSettings["clientKey"], clientNumber = Convert.ToInt64(ConfigurationManager.AppSettings["clientNumber"]) },
                    dpdOrderNr = dpdNum,
                    pickupYear = DateTime.Now.Year
                };
                stateParcels states = parcelTracingService.getStatesByDPDOrder(rstDpdOrder);
                IList<TrackRecord> records = new List<TrackRecord>();
                foreach (stateParcel state in states.states)
                {
                    records.Add(new TrackRecord()
                    {
                        dttm_Record_Dttm = state.transitionTime,
                        nvchar_Description = state.newState,
                        nvchar_Description_Local = state.newState,
                        nvchar_City = state.terminalCity
                    });
                }

                return records;
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
                return null;
            }
            #endregion
        }

        public static void AllClear()
        {
            string strSql = $"UPDATE  tb_SFI_TrackNum SET int_AG_Syn = 0 WHERE int_AGid={mainAGid}";
            SQLHelper.UpDateSQL(strSql);
        }
        /// <summary>
        /// 讲错误日志信息写入到文件当中
        /// </summary>
        /// <param name="str"></param>
        public static void WriteError(string str)
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AGinfoError.txt");
                if (!File.Exists(filePath))
                {
                    FileInfo myFile = new FileInfo(filePath);
                    myFile.Create();
                }
                StreamWriter sw = File.AppendText(filePath);
                sw.WriteLine($"{ DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}|{str}");
                sw.Flush();
                sw.Close();
            }
            catch
            {
            }
        }
        /// <summary>
        /// 获取服务器是否启用
        /// </summary>
        /// <returns></returns>
        public static string GetServerStatus()
        {
            try
            {
                string strSql = "SELECT TOP 1 ServerStatus FROM tb_ServerConfig";
                return SQLHelper.GetStringFromDB(strSql);
            }
            catch
            {
                return "O";
            }
        }

        /// <summary>
        /// 将xml文件反序列化为指定的类型
        /// </summary>
        /// <param name="filePath">xml文件路径</param>
        /// <param name="type">反序列化的类型</param>
        /// <returns></returns>
        public static object LoadFromXml(string filePath, Type type)
        {
            object result = null;

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(type);
                    result = xmlSerializer.Deserialize(reader);
                }
            }

            return result;
        }
        /// <summary>
        /// 在日志表中记录同步运单状态的结果信息
        /// </summary>
        /// <param name="track">运单信息表</param>
        /// <param name="result">同步结果信息</param>
        /// <param name="error">错误信息</param>
        /// <returns>操作数据库的结果</returns>
        private static string WriteServerLog(SFITrackNum track, string result, string error)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@type", "G", DbType.String);
            p.Add("@MappingID", track.int_Mappingid, DbType.Int32);
            p.Add("@result", result, DbType.String);
            if (!string.IsNullOrWhiteSpace(error))
                p.Add("@error", error, DbType.String);
            p.Add("@message", null, DbType.String, ParameterDirection.ReturnValue);
            return SQLHelper.ExecuteStoredProcedure<string>(Constants.sp_Logupdate, p, "@message");
        }
        /// <summary>
        /// 将获取的运单状态信息同步到Track_Record表中
        /// </summary>
        /// <param name="track">订单号和运单号的关系表</param>
        /// <param name="record">运单状态信息</param>
        /// <returns>更新数据库的结果</returns>
        private static string UpdateTrackRecord(SFITrackNum track, TrackRecord record)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("@MappingID", track.int_Mappingid, DbType.Int32);
            p.Add("@AGid", track.int_AGid, DbType.Int32);
            p.Add("@SFInumber", track.vchar_SFInum, DbType.String);
            p.Add("@AGnumber", track.vchar_AGnum, DbType.String);
            p.Add("@dttm", record.dttm_Record_Dttm, DbType.DateTime);
            p.Add("@Description", record.nvchar_Description, DbType.String);
            p.Add("@DescriptionLocal", record.nvchar_Description_Local, DbType.String);
            p.Add("@city", record.nvchar_City, DbType.String);
            p.Add("@code", $"{record.dttm_Record_Dttm.ToString("yyyyMMddhhmmss")}_{track.int_Mappingid}", DbType.String);
            p.Add("@message", null, DbType.String, ParameterDirection.ReturnValue);
            return SQLHelper.ExecuteStoredProcedure<string>(Constants.sp_RecordUpdate, p, "@message");
        }

    }
}
