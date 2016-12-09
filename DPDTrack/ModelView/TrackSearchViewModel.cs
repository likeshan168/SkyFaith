using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.Collections.Generic;
using DPDTrack.Models;
using DPDModel.Models;
using DPDModelDPDModel.DAL;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using DevExpress.Mvvm.POCO;
using DevExpress.XtraGrid;
using System.Data;

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

        public virtual CBXAgent SelectedAgent { get; set; }
        private ObservableCollection<SFITrackNum> trackNumInfos;
        public ObservableCollection<SFITrackNum> TrackNumInfos
        {
            get
            {
                return trackNumInfos;
            }

        }


        public IEnumerable<CBXAgent> GetAgentInfo()
        {
            //string sql = "select [int_AGid] as AGid,[vchar_AGname] as Agname from tb_Agent where IsOpen=1 and int_AGtype = 0";
            string sql = "select [int_AGid] as AGid,[vchar_AGname] as Agname from tb_Agent where IsOpen=1";
            return SQLHelper.GetObject<CBXAgent>(sql);
        }

        public void Search(SearchParameter para)
        {
            para.SplashScreenManager.ShowWaitForm();

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


            var tracks = (SQLHelper.GetObject<SFITrackNum>(sqlSelect));
            foreach (SFITrackNum track in tracks)
            {
                trackNumInfos.Add(track);
            }

            para.SplashScreenManager.CloseWaitForm();

            #endregion

        }

        public void ClearDataSource(SearchParameter para)
        {
            para.SplashScreenManager.ShowWaitForm();
            TrackNumInfos.Clear();
            para.SplashScreenManager.CloseWaitForm();
        }

    }
}