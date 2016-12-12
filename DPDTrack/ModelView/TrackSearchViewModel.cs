using System;
using DevExpress.Mvvm.DataAnnotations;
using System.Collections.Generic;
using DPDTrack.Models;
using DPDModel.Models;
using DPDModelDPDModel.DAL;
using System.Linq;
using System.Collections.ObjectModel;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using System.Data;
using DPDTrack.Views;

namespace DPDTrack.ModelView
{
    [POCOViewModel]
    public class TrackSearchViewModel
    {
        public TrackSearchViewModel()
        {
            CBXAgents = GetAgentInfo();
            ImportDate = DateTime.Now;
            SelectedAgent = CBXAgents.First();
            trackNumInfos = new ObservableCollection<SFITrackNum>();
        }
        public virtual IEnumerable<CBXAgent> CBXAgents { get; set; }

        public virtual DateTime ImportDate { get; set; }

        public virtual string OrderNo { get; set; }

        public virtual string AgentNo { get; set; }

        public virtual string OrderNo2 { get; set; }

        public virtual string AgentNo2 { get; set; }

        public virtual SFITrackNum SelectedTrackInfo { get; set; }
        //public virtual bool IsLoading { get; protected set; }

        public virtual CBXAgent SelectedAgent { get; set; }
        private ObservableCollection<SFITrackNum> trackNumInfos;
        public virtual ObservableCollection<SFITrackNum> TrackNumInfos
        {
            get
            {
                return trackNumInfos;
            }

        }
        /// <summary>
        /// 获取代理上信息
        /// </summary>
        /// <returns></returns>
        private IEnumerable<CBXAgent> GetAgentInfo()
        {
            //string sql = "select [int_AGid] as AGid,[vchar_AGname] as Agname from tb_Agent where IsOpen=1 and int_AGtype = 0";
            string sql = "select [int_AGid] as AGid,[vchar_AGname] as Agname from tb_Agent where IsOpen=1";
            return SQLHelper.GetObject<CBXAgent>(sql);
        }
        /// <summary>
        /// 查询运单信息
        /// </summary>
        /// <param name="para"></param>
        public virtual void Search(SplashScreenManager splash)
        {
            splash.ShowWaitForm();

            #region 拼接sql语句的where条件
            string sqlWhere = $" tb_SFI_TrackNum.int_AGid={SelectedAgent.AGid}";

            if (!string.IsNullOrWhiteSpace(OrderNo))
            {
                if (OrderNo.IndexOf(",") > 0)
                {
                    sqlWhere = $"{sqlWhere} AND tb_SFI_TrackNum.vchar_SFInum in (";
                    string[] strs = OrderNo.Split(',');
                    foreach (string str in strs)
                    {
                        sqlWhere = $"{sqlWhere}'{str}',";
                    }
                    sqlWhere = $"{sqlWhere.TrimEnd(',')})";
                }
                else
                {
                    sqlWhere = $"{sqlWhere} AND tb_SFI_TrackNum.vchar_SFInum='{OrderNo}'";
                }
            }

            if (!string.IsNullOrWhiteSpace(AgentNo))
            {
                if (AgentNo.IndexOf(",") > 0)
                {
                    sqlWhere = $"{sqlWhere} AND tb_SFI_TrackNum.vchar_AGnum in (";
                    string[] strs = AgentNo.Split(',');
                    foreach (string str in strs)
                    {
                        sqlWhere = $"{sqlWhere}'{str}',";
                    }
                    sqlWhere = $"{sqlWhere.TrimEnd(',')})";
                }
                else
                {
                    sqlWhere = $"{sqlWhere} AND tb_SFI_TrackNum.vchar_AGnum='{OrderNo}'";
                }
            }

            sqlWhere = $"{sqlWhere} AND CONVERT(varchar(10),tb_SFI_TrackNum.dttm_updateDttm,112) ='{ImportDate.ToString("yyyyMMdd")}'";
            //string sqlSelect = $"SELECT tb_SFI_TrackNum.int_Mappingid AS 数据编号, tb_SFI_TrackNum.vchar_SFInum AS SFI单号,tb_SFI_TrackNum.vchar_AGnum AS 代理单号, (CONVERT(varchar(10),tb_SFI_TrackNum.int_AGid) + N'-' + tb_Agent.vchar_AGname) AS 代理名称, tb_SFI_TrackNum.dttm_updateDttm AS 数据导入时间, tb_SFI_TrackNum.dttm_AG_LastSyn AS 最后同步时间, tb_SFI_TrackNum.char_IsStop AS 运输服务完毕 FROM tb_SFI_TrackNum INNER JOIN tb_Agent ON tb_SFI_TrackNum.int_AGid = tb_Agent.int_AGid WHERE {sqlWhere} ORDER BY tb_SFI_TrackNum.dttm_updateDttm DESC";
            string sqlSelect = $"SELECT tb_SFI_TrackNum.int_Mappingid, tb_SFI_TrackNum.vchar_SFInum,tb_SFI_TrackNum.vchar_AGnum , (CONVERT(varchar(10),tb_SFI_TrackNum.int_AGid) + N'-' + tb_Agent.vchar_AGname), tb_SFI_TrackNum.dttm_updateDttm, tb_SFI_TrackNum.dttm_AG_LastSyn, tb_SFI_TrackNum.char_IsStop FROM tb_SFI_TrackNum INNER JOIN tb_Agent ON tb_SFI_TrackNum.int_AGid = tb_Agent.int_AGid WHERE {sqlWhere} ORDER BY tb_SFI_TrackNum.dttm_updateDttm DESC";

            //DataSet ds = SQLHelper.GetDataSet(sqlSelect);
            //if (ds != null && ds.Tables.Count == 1)
            //{
            //    //para.Grid.BeginUpdate();

            //    //para.Grid.DataSource = ds.Tables[0].DefaultView;
            //    //para.Grid.EndUpdate();

            //}


            var tracks = (SQLHelper.GetObject<SFITrackNum>(sqlSelect)).ToList();
            trackNumInfos.Clear();
            if (tracks.Count > 0)
            {
                foreach (SFITrackNum track in tracks)
                {
                    trackNumInfos.Add(track);
                }
            }
            else
            {
                trackNumInfos.Clear();
            }
            splash.CloseWaitForm();

            #endregion

        }
        /// <summary>
        /// 清除表格中的获取到的运单信息
        /// </summary>
        /// <param name="para"></param>
        public virtual void ClearDataSource(SplashScreenManager splash)
        {
            splash.ShowWaitForm();
            TrackNumInfos.Clear();
            splash.CloseWaitForm();
        }
        /// <summary>
        /// Enter事件，默认为查询运单信息
        /// </summary>
        /// <param name="para"></param>
        public virtual void PressEnter(SplashScreenManager splash)
        {
            Search(splash);
        }

        public virtual void Submit(SplashScreenManager splash)
        {
            splash.ShowWaitForm();
            ImportDate = DateTime.Now;
            AgentNo = string.Empty;
            OrderNo = string.Empty;

            if (string.IsNullOrWhiteSpace(OrderNo2))
            {
                splash.CloseWaitForm();
                XtraMessageBox.Show("订单号不能为空", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(AgentNo2))
            {
                splash.CloseWaitForm();
                XtraMessageBox.Show("DPD单号不能为空", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string str = "0123456789";
            string aGawbSign = "";

            for (int i = AgentNo2.Length - 1; i >= 0; i += -1)
            {
                if (str.LastIndexOf(AgentNo2.Substring(i, 1)) != 0)
                {
                    aGawbSign = AgentNo2.Substring(i, 1);
                    break;
                }
            }

            if (string.IsNullOrWhiteSpace(aGawbSign))
            {
                //获取最后一位的值
                aGawbSign = AgentNo2.Substring(AgentNo2.Length - 2, 1);
            }

            string sqlStr = $"INSERT INTO tb_SFI_TrackNum (vchar_SFInum, vchar_AGnum, int_AGid, vchar_updateUser, dttm_updateDttm,char_AG_Syn_sign) VALUES ('{OrderNo2}','{AgentNo2}','{SelectedAgent.AGid}','sfi',GETDATE(),'{aGawbSign}');";
            sqlStr = $"{sqlStr} select * from tb_SFI_TrackNum where int_AGid={SelectedAgent.AGid} AND CONVERT(varchar(10),tb_SFI_TrackNum.dttm_updateDttm,112) ='{ImportDate.ToString("yyyyMMdd")}';";
            IList<SFITrackNum> tracks = SQLHelper.GetObject<SFITrackNum>(sqlStr).ToList();

            LoadNewAddedTrackNos(sqlStr);

            splash.CloseWaitForm();
        }

        public virtual void BatchSubmit(SplashScreenManager splash)
        {
            string clipboard = Clipboard.GetText();
            DataTable dtInput = new DataTable();
            DataRow nR;
            if (!clipboard.Contains((char)(10)))
            {
                XtraMessageBox.Show("复制的内容格式不正确", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!clipboard.Contains((char)(9)))
            {
                XtraMessageBox.Show("复制的内容格式不正确", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            dtInput.Columns.Add("SFI单号");
            dtInput.Columns.Add("代理单号");
            string[] strs = clipboard.Split((char)(10));
            string enter = ((char)13).ToString();
            string tmpStr;
            foreach (string s in strs)
            {
                tmpStr = s.Replace(enter, "");
                if (!string.IsNullOrEmpty(tmpStr))
                {
                    nR = dtInput.NewRow();
                    nR[0] = tmpStr.Split((char)(9))[0];
                    nR[1] = tmpStr.Split((char)(9))[1];
                    dtInput.Rows.Add(nR);
                }
            }

            InputDataFrm inputDataFrm = new InputDataFrm(dtInput, SelectedAgent.AGid);
            DialogResult DResult = inputDataFrm.ShowDialog();
            if (DResult == DialogResult.OK)
            {
                splash.ShowWaitForm();
                string sqlStr = $"select * from tb_SFI_TrackNum where int_AGid={SelectedAgent.AGid} AND CONVERT(varchar(10),tb_SFI_TrackNum.dttm_updateDttm,112) ='{ImportDate.ToString("yyyyMMdd")}';";
                LoadNewAddedTrackNos(sqlStr);
                splash.CloseWaitForm();
            }
            else if (DResult == DialogResult.Abort)
            {
                XtraMessageBox.Show("添加数据失败", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (DResult == DialogResult.Cancel)
            {
                XtraMessageBox.Show("用户取消操作", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public virtual void DeleteTracks()
        {
            try
            {
                if (SelectedTrackInfo == null)
                {
                    XtraMessageBox.Show("请选中要删除的行", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string sqlStr;
                if (XtraMessageBox.Show("删除后不可恢复，是否继续？", "错误信息", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    sqlStr = $"DELETE tb_SFI_TrackNum WHERE int_AG_Syn=0 AND int_Mappingid={SelectedTrackInfo.int_Mappingid};DELETE [tb_track_record] WHERE [int_MappingId]={SelectedTrackInfo.int_Mappingid}";

                    if (SQLHelper.UpDateSQL(sqlStr))
                    {
                        XtraMessageBox.Show("删除成功", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //var itemToDel = trackNumInfos.FirstOrDefault(t => t.int_Mappingid == SelectedTrackInfo.int_Mappingid);
                        trackNumInfos.Remove(SelectedTrackInfo);
                    }
                    else
                    {
                        XtraMessageBox.Show("数据正在更新,删除失败", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public virtual void DoubleClickCell()
        {
            if (SelectedTrackInfo == null)
            {
                XtraMessageBox.Show("请选中要展示的行", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                CheckPointLog checkPointLog = new CheckPointLog(SelectedTrackInfo, SelectedAgent);
                checkPointLog.Show();
            }
        }

        private void LoadNewAddedTrackNos(string sqlStr)
        {
            IList<SFITrackNum> tracks = SQLHelper.GetObject<SFITrackNum>(sqlStr).ToList();

            if (tracks.Count > 0)
            {
                OrderNo = string.Empty;
                AgentNo = string.Empty;
                AgentNo2 = string.Empty;
                OrderNo2 = string.Empty;
                trackNumInfos.Clear();
                foreach (SFITrackNum track in tracks)
                {
                    trackNumInfos.Add(track);
                }
            }
            else
            {
                XtraMessageBox.Show("添加数据失败", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}