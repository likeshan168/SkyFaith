using System;
using DevExpress.Mvvm.DataAnnotations;
using DPDModel.Models;
using DPDTrack.Models;
using System.Collections.ObjectModel;
using DPDModelDPDModel.DAL;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using Dapper;
using System.Data;
using DPDModel.BLL;
using DevExpress.XtraSplashScreen;

namespace DPDTrack.ModelView
{
    [POCOViewModel]
    public class CheckPointLogViewModel
    {
        public virtual SFITrackNum SFITrackNum { get; set; }

        public virtual CBXAgent CBXAgent { get; set; }

        public virtual string RecordStr { get; set; }

        public virtual TrackRecord SelectedTrackRecord { get; set; }

        private ObservableCollection<TrackRecord> trackRecords;
        public virtual ObservableCollection<TrackRecord> TrackRecords
        {
            get
            {
                if (trackRecords == null)
                {
                    trackRecords = GetTrackRecords();
                }

                return trackRecords;
            }
        }

        private ObservableCollection<ServerLog> trackRecordLogs;

        public virtual ObservableCollection<ServerLog> TrackRecordLogs
        {
            get
            {
                if (trackRecordLogs == null)
                    trackRecordLogs = GetTrackRecordLogs();
                return trackRecordLogs;
            }
        }

        private ObservableCollection<ServerLog> GetTrackRecordLogs()
        {
            string sqlStr = $"SELECT [log_time],[SFI_Number],[AG_Number],'{CBXAgent.Agname}' as [代理名称],[vchar_result],[nvchar_Error] FROM [db_SFI].[dbo].[tb_ServerLog] WHERE [SFI_Number]='{SFITrackNum.vchar_SFInum}' AND [AG_Number]='{SFITrackNum.vchar_AGnum}' AND [AG_id]={CBXAgent.AGid} AND [char_type]='G' ORDER BY log_id DESC ";
            IEnumerable<ServerLog> logs = SQLHelper.GetObject<ServerLog>(sqlStr);
            string[] results;
            foreach (ServerLog log in logs)
            {
                results = log.vchar_result.Split('|');
                log.vchar_result = $"共{results[0]}条检查点，成功导入{results[1]}条新数据，{results[2]}条错误";
            }
            return new ObservableCollection<ServerLog>(logs);
        }

        private ObservableCollection<TrackRecord> GetTrackRecords()
        {
            string sqlStr = $"SELECT int_id, dttm_Record_Dttm, nvchar_Description, nvchar_Description_Local, nvchar_City FROM tb_track_record WHERE (int_MappingId ={SFITrackNum.int_Mappingid}) ORDER BY int_id";
            return new ObservableCollection<TrackRecord>(SQLHelper.GetObject<TrackRecord>(sqlStr));
        }

        private List<string> GetDesc(string str)
        {
            List<string> rS = new List<string>();
            Regex re = default(Regex);
            MatchCollection Contents = default(MatchCollection);
            int intS = 0;
            int intE = 0;
            re = new Regex("[\\u4e00-\\u9fa5]");
            Contents = re.Matches(str);
            //re = New Regex("<br[^>]*>(.*?)</a>")
            if (Contents.Count == 0)
            {
                rS.Add("");
                rS.Add(str.Replace("(", " ").Replace("（", " ").Replace("）", " ").Replace(")", " "));
            }
            else
            {
                intS = Contents[0].Index + 1;
                intE = str.LastIndexOf(Contents[Contents.Count - 1].Value) + 1;
                rS.Add(str.Substring(intS, intE - intS));
                rS.Add(str.Replace(rS[0], "").Replace("(", " ").Replace("（", " ").Replace("）", " ").Replace(")", " "));
            }
            return rS;
        }

        public virtual void RefreshLogs(SplashScreenManager splash)
        {
            splash.ShowWaitForm();
            trackRecordLogs = GetTrackRecordLogs();
            splash.CloseWaitForm();
        }

        public virtual void AddTrackRecord()
        {
            DateTime strDate;
            string strDes = null;
            string strDesLoc = null;
            string strCity = null;
            TrackRecord record;
            string tmpStr;
            int intNew = 0;
            int intError = 0;
            string strRes;
            try
            {
                intNew = 0;
                intError = 0;
                foreach (string s in RecordStr.Split((char)(10)))
                {
                    tmpStr = s.Trim();
                    if (!string.IsNullOrWhiteSpace(tmpStr))
                    {
                        if (tmpStr.Substring(0, 2) == "20")
                        {
                            strDate = Convert.ToDateTime(tmpStr.Split(new char[] { ' ' })[0] + " " + tmpStr.Split(new char[] { ' ' })[1]);
                            strDes = GetDesc(tmpStr.Replace(strDate.ToString(), ""))[1];
                            strDesLoc = GetDesc(tmpStr.Replace(strDate.ToString(), ""))[0];
                            switch (tmpStr.IndexOf(" - "))
                            {
                                case 0:
                                    strCity = "China";
                                    break;
                                default:
                                    strCity = tmpStr.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries)[tmpStr.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries).Length - 1];
                                    break;
                            }
                            record = new TrackRecord();
                            record.dttm_Record_Dttm = strDate;
                            record.nvchar_Description = strDes;
                            record.nvchar_Description_Local = strDesLoc;
                            record.nvchar_City = strCity;
                            record.int_MappingId = SFITrackNum.int_Mappingid;
                            record.int_AGid = SFITrackNum.int_AGid;
                            record.vchar_SFInum = SFITrackNum.vchar_SFInum;
                            record.vchar_AGnum = SFITrackNum.vchar_AGnum;
                            strRes = UpdateTrackRecord(SFITrackNum, record);
                            if (strRes == "N")
                            {
                                intNew += 1;
                            }
                            else if (strRes == "F")
                            {
                                intError += 1;

                            }
                            trackRecords.Add(record);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteServerLog(SFITrackNum, "0|0|0|", ex.Message);
            }
            WriteServerLog(SFITrackNum, $"{intError + intNew}|{intNew}|{intError}", null);

            XtraMessageBox.Show("导入数据完成，导入结果请查看日志", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        public virtual void DeleteTrackRecord()
        {
            if (SelectedTrackRecord == null)
            {
                XtraMessageBox.Show("请选中要删除的行", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                string sqlStr = $"DELETE [db_SFI].[dbo].[tb_track_record] WHERE int_id={SelectedTrackRecord.int_id}";
                if (XtraMessageBox.Show("删除后不可恢复，是否继续？", "错误信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (SQLHelper.UpDateSQL(sqlStr))
                    {
                        TrackRecords.Remove(SelectedTrackRecord);
                    }
                }
            }
        }

        private string UpdateTrackRecord(SFITrackNum track, TrackRecord record)
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

        private string WriteServerLog(SFITrackNum track, string result, string error)
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

    }
}