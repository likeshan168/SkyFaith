using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.Data;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using DPDModelDPDModel.DAL;
using DPDTrack.Models;

namespace DPDTrack.ModelView
{
    [POCOViewModel]
    public class InputDataFrmViewModel
    {

        public virtual DataTable DataSource { get; set; }

        public virtual DialogResult DResult { get; set; }

        public virtual void AddTraks(CustomParameter para)
        {
            para.SplashScreenManager.ShowWaitForm();
            string sqlStr = string.Empty;
            if (DataSource != null)
            {
                foreach (DataRow row in DataSource.Rows)
                {
                    string orderNo = row[0].ToString();
                    string agentNo = row[1].ToString();

                    if (string.IsNullOrWhiteSpace(orderNo))
                    {
                        para.SplashScreenManager.CloseWaitForm();
                        XtraMessageBox.Show("订单号不能为空", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(agentNo))
                    {
                        para.SplashScreenManager.CloseWaitForm();
                        XtraMessageBox.Show("DPD单号不能为空", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    string str = "0123456789";
                    string aGawbSign = "";

                    for (int i = agentNo.Length - 1; i >= 0; i += -1)
                    {
                        if (str.LastIndexOf(agentNo.Substring(i, 1)) != 0)
                        {
                            aGawbSign = agentNo.Substring(i, 1);
                            break;
                        }
                    }

                    if (string.IsNullOrWhiteSpace(aGawbSign))
                    {
                        //获取最后一位的值
                        aGawbSign = agentNo.Substring(agentNo.Length - 2, 1);
                    }
                    sqlStr = $"{sqlStr} INSERT INTO tb_SFI_TrackNum (vchar_SFInum, vchar_AGnum, int_AGid, vchar_updateUser, dttm_updateDttm,char_AG_Syn_sign) VALUES ('{orderNo}','{agentNo}',{para.AgentID},'sfi',GETDATE(),'{aGawbSign}');";

                }

                if (!string.IsNullOrWhiteSpace(sqlStr))
                {
                    if (SQLHelper.UpDateSQL(sqlStr))
                    {
                        DResult = DialogResult.OK;
                    }
                    else
                    {
                        DResult = DialogResult.Abort;
                    }
                }
            }

            para.SplashScreenManager.CloseWaitForm();
        }

        public virtual void CloseForm()
        {
            DResult = DialogResult.Cancel;
        }
    }
}