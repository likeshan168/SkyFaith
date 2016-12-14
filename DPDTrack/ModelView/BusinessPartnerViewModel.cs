using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using DPDModel.Models;
using DPDModelDPDModel.DAL;
using DevExpress.Mvvm.POCO;
using System.Windows.Forms;
using DPDTrack.Views;
using System.Collections.Generic;
using System.Linq;

namespace DPDTrack.ModelView
{
    [POCOViewModel]
    public class BusinessPartnerViewModel
    {
        public BusinessPartnerViewModel()
        {
            Agents = GetAgeents();
        }

        public IMessageBoxService MessageBoxService
        {
            get
            {
                return this.GetService<IMessageBoxService>();
            }
        }
        public ISplashScreenService SplashScreenService
        {
            get
            {
                return this.GetService<ISplashScreenService>();
            }
        }
        public IDialogService DialogService
        {
            get
            {
                return this.GetService<IDialogService>();
            }
        }
        /// <summary>
        /// 所有的代理商信息
        /// </summary>
        public virtual ObservableCollection<Agent> Agents { get; set; }
        /// <summary>
        /// datagrid中被选中的行
        /// </summary>
        public virtual Agent SelectedAgent { get; set; }
        
        public virtual bool IsGPCSync { get; set; }
        public virtual bool IsGPCQuery { get; set; }
        public virtual bool IsGPCPush { get; set; }
        /// <summary>
        /// 获取代理商信息
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<Agent> GetAgeents()
        {
            string sqlStr = $"SELECT [int_AGid],[vchar_AGcode],[vchar_AGname],[vchar_AGLinkMan] ,[vchar_AGcontect],CASE WHEN [int_AGtype]=0 THEN N'检查点提供方' WHEN [int_AGtype]=1 THEN N'检查点查询方' WHEN [int_AGtype]=2 THEN N'检查点推送接收方' END AS int_AGtype ,[bit_synOpen],[vchar_synVerify] as [检查点同步验证码] ,[vchar_synStopKeyWord] ,[int_synSpacing] ,[bit_synQuery] ,[vchar_QueryVerify],[bit_synPush],[vchar_PushUser],[vchar_PushVerify] FROM [db_SFI].[dbo].[tb_Agent] WHERE [IsOpen]=1";
            return new ObservableCollection<Agent>(SQLHelper.GetObject<Agent>(sqlStr));
        }
        /// <summary>
        /// 点击添加按钮的事件
        /// </summary>
        public virtual void AddAgentInfo()
        {

            ClearAgentInfo();
            //NewAgentFrm newAgentFrm = new NewAgentFrm();
            //DialogResult result = newAgentFrm.ShowDialog();
            //SplashScreenService.ShowSplashScreen();
            //if (result == DialogResult.OK)
            //{
            //    Agents.Clear();
            //    var agents = GetAgeents();
            //    foreach (var agent in agents)
            //    {
            //        Agents.Add(agent);
            //    }
            //}
            //SplashScreenService.HideSplashScreen();
        }

        public virtual void SaveAgentInfo()
        {

            try
            {
                #region 验证重复
                if (string.IsNullOrWhiteSpace(SelectedAgent.vchar_AGname))
                {
                    MessageBoxService.ShowMessage("商业伙伴名称不能为空", "提示信息", MessageButton.OK, MessageIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(SelectedAgent.vchar_AGcontect))
                {
                    MessageBoxService.ShowMessage("联系方式不能为空", "提示信息", MessageButton.OK, MessageIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(SelectedAgent.vchar_AGLinkMan))
                {
                    MessageBoxService.ShowMessage("联系人不能为空", "提示信息", MessageButton.OK, MessageIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(SelectedAgent.vchar_AGcode))
                {
                    MessageBoxService.ShowMessage("商业伙伴代码不能为空", "提示信息", MessageButton.OK, MessageIcon.Error);
                    return;
                }
                string sqlStr = string.Empty;

                if (SelectedAgent.bit_synOpen)
                {
                    if (string.IsNullOrWhiteSpace(SelectedAgent.vchar_synVerify) || string.IsNullOrWhiteSpace(SelectedAgent.vchar_synStopKeyWord))
                    {
                        MessageBoxService.ShowMessage("检查点同步信息不可为空", "提示信息", MessageButton.OK, MessageIcon.Error);
                        return;
                    }
                    sqlStr = $"SELECT [vchar_AGcode],[vchar_synVerify] FROM [dbo].[tb_Agent] WHERE [vchar_AGcode]='{SelectedAgent.vchar_AGcode}' OR [vchar_synVerify]='{SelectedAgent.vchar_synVerify}'";
                    IList<Agent> agents = (SQLHelper.GetObject<Agent>(sqlStr)).ToList();
                    if (agents.Count != 0)
                    {
                        Agent agent = agents.FirstOrDefault();
                        if (SelectedAgent.vchar_AGcode == agent.vchar_AGcode)
                        {
                            MessageBoxService.ShowMessage("与其他商业伙伴代码重复，请更换", "提示信息", MessageButton.OK, MessageIcon.Error);
                            return;
                        }
                        if (SelectedAgent.vchar_synVerify == agent.vchar_synVerify)
                        {
                            MessageBoxService.ShowMessage("与其他商业伙伴同步接口校验码重复，请更换", "提示信息", MessageButton.OK, MessageIcon.Error);
                            return;
                        }
                    }
                }
                if (SelectedAgent.bit_synQuery)
                {
                    if (string.IsNullOrWhiteSpace(SelectedAgent.vchar_QueryVerify))
                    {
                        MessageBoxService.ShowMessage("检查点查询信息不可为空", "提示信息", MessageButton.OK, MessageIcon.Error);
                        return;
                    }
                    sqlStr = $"SELECT [vchar_QueryVerify] FROM [dbo].[tb_Agent] WHERE [vchar_QueryVerify]='{SelectedAgent.vchar_QueryVerify}'";
                    IList<Agent> agents = (SQLHelper.GetObject<Agent>(sqlStr)).ToList();
                    if (agents.Count != 0)
                    {
                        Agent agent = agents.FirstOrDefault();
                        if (SelectedAgent.vchar_QueryVerify == agent.vchar_QueryVerify)
                        {
                            MessageBoxService.ShowMessage("与其他商业伙伴查询接口校验码重复，请更换", "提示信息", MessageButton.OK, MessageIcon.Error);

                            return;
                        }
                    }
                }
                if (SelectedAgent.bit_synPush)
                {
                    if (string.IsNullOrWhiteSpace(SelectedAgent.vchar_PushUser) || string.IsNullOrWhiteSpace(SelectedAgent.vchar_PushVerify))
                    {
                        MessageBoxService.ShowMessage("检查点推送信息不可为空", "提示信息", MessageButton.OK, MessageIcon.Error);
                        return;
                    }

                    sqlStr = $"SELECT [vchar_PushUser],[vchar_PushVerify] FROM [dbo].[tb_Agent] WHERE [vchar_PushUser]='{SelectedAgent.vchar_PushUser}' OR [vchar_PushVerify]='{SelectedAgent.vchar_PushVerify}'";

                    IList<Agent> agents = (SQLHelper.GetObject<Agent>(sqlStr)).ToList();
                    if (agents.Count != 0)
                    {
                        Agent agent = agents.FirstOrDefault();

                        if (SelectedAgent.vchar_PushUser == agent.vchar_PushUser)
                        {
                            MessageBoxService.ShowMessage("与其他商业伙伴推送接口用户重复，请更换", "提示信息", MessageButton.OK, MessageIcon.Error);
                            return;
                        }
                        if (SelectedAgent.vchar_PushVerify == agent.vchar_PushVerify)
                        {
                            MessageBoxService.ShowMessage("与其他商业伙伴推送接口校验码重复，请更换", "提示信息", MessageButton.OK, MessageIcon.Error);
                            return;
                        }
                    }
                }

                #endregion
                #region 插入数据
                SplashScreenService.ShowSplashScreen();
                if (SelectedAgent.int_AGid == 0)
                {

                    if (SelectedAgent.int_AGtype == "检查点提供方")
                    {
                        sqlStr = $"INSERT INTO tb_Agent (vchar_AGcode, vchar_AGname, vchar_AGLinkMan, vchar_AGcontect, int_AGtype, bit_synOpen, vchar_synVerify,vchar_synStopKeyWord,int_synSpacing)VALUES ('{SelectedAgent.vchar_AGcode}', N'{SelectedAgent.vchar_AGname}', N'{SelectedAgent.vchar_AGLinkMan}', '{SelectedAgent.vchar_AGcontect}', 0, {(SelectedAgent.bit_synOpen ? 1 : 0)}, '{SelectedAgent.vchar_synVerify}', N'{SelectedAgent.vchar_synStopKeyWord}',{SelectedAgent.int_synSpacing}) ";
                    }
                    else if (SelectedAgent.int_AGtype == "检查点查询方")
                    {
                        sqlStr = $"INSERT INTO tb_Agent(vchar_AGcode, vchar_AGname, vchar_AGLinkMan, vchar_AGcontect, int_AGtype, bit_synQuery, vchar_QueryVerify)VALUES ('{SelectedAgent.vchar_AGcode}', N'{SelectedAgent.vchar_AGname}',N'{SelectedAgent.vchar_AGLinkMan}', '{SelectedAgent.vchar_AGcontect}', 1, {(SelectedAgent.bit_synQuery ? 1 : 0)}, '{SelectedAgent.vchar_QueryVerify}') ";

                    }
                    else if (SelectedAgent.int_AGtype == "检查点推送接收方")
                    {
                        sqlStr = $"INSERT INTO tb_Agent(vchar_AGcode, vchar_AGname, vchar_AGLinkMan, vchar_AGcontect, int_AGtype,bit_synPush, vchar_PushUser, vchar_PushVerify) VALUES ('{SelectedAgent.vchar_AGcode}', N'{SelectedAgent.vchar_AGname}', N'{SelectedAgent.vchar_AGLinkMan}', '{SelectedAgent.vchar_AGcontect}', 2, {(SelectedAgent.bit_synPush ? 1 : 0)}, '{SelectedAgent.vchar_PushUser}', '{SelectedAgent.vchar_PushVerify}') ";
                    }

                    if (SQLHelper.UpDateSQL(sqlStr))
                    {
                        MessageBoxService.ShowMessage("保存成功", "提示信息", MessageButton.OK, MessageIcon.Information);
                    }
                    else
                    {
                        MessageBoxService.ShowMessage("保存失败", "提示信息", MessageButton.OK, MessageIcon.Error);
                    }

                    Agents.Add(SelectedAgent);

                }
                else
                {
                    if (SelectedAgent.int_AGtype == "检查点提供方")
                    {
                        sqlStr = $"UPDATE tb_Agent SET vchar_AGcode = '{SelectedAgent.vchar_AGcode}', vchar_AGname = N'{SelectedAgent.vchar_AGname}', vchar_AGLinkMan = N'{SelectedAgent.vchar_AGLinkMan}', vchar_AGcontect = '{SelectedAgent.vchar_AGcontect}',int_AGtype = 0, bit_synOpen = {(SelectedAgent.bit_synOpen ? 1 : 0)}, vchar_synVerify = '{SelectedAgent.vchar_synVerify}', vchar_synStopKeyWord = N'{SelectedAgent.vchar_synStopKeyWord}', bit_synQuery = 0,vchar_QueryVerify = NULL, bit_synPush = 0, vchar_PushUser = NULL, vchar_PushVerify = NULL,int_synSpacing= {SelectedAgent.int_synSpacing}  WHERE [int_AGid]={SelectedAgent.int_AGid}";
                    }
                    else if (SelectedAgent.int_AGtype == "检查点查询方")
                    {
                        sqlStr = $"UPDATE tb_Agent SET vchar_AGcode = '{SelectedAgent.vchar_AGcode}', vchar_AGname = N'{SelectedAgent.vchar_AGname}', vchar_AGLinkMan = N'{SelectedAgent.vchar_AGLinkMan}', vchar_AGcontect = '{SelectedAgent.vchar_AGcontect}', int_AGtype = 1, bit_synOpen = 0, vchar_synVerify = NULL, vchar_synStopKeyWord = NULL, bit_synQuery = {(SelectedAgent.bit_synQuery ? 1 : 0)}, vchar_QueryVerify =  '{SelectedAgent.vchar_QueryVerify}', bit_synPush = 0, vchar_PushUser = NULL, vchar_PushVerify = NULL WHERE [int_AGid]={SelectedAgent.int_AGid}";

                    }
                    else if (SelectedAgent.int_AGtype == "检查点推送接收方")
                    {
                        sqlStr = $"UPDATE tb_Agent SET vchar_AGcode ='{SelectedAgent.vchar_AGcode}', vchar_AGname = N'{SelectedAgent.vchar_AGname}', vchar_AGLinkMan =  N'{SelectedAgent.vchar_AGLinkMan}', vchar_AGcontect ='{SelectedAgent.vchar_AGcontect}',int_AGtype =2, bit_synOpen = 0, vchar_synVerify = NULL, vchar_synStopKeyWord = NULL, bit_synQuery = 0, vchar_QueryVerify = NULL, bit_synPush = {(SelectedAgent.bit_synPush ? 1 : 0)}, vchar_PushUser = '{SelectedAgent.vchar_PushUser}', vchar_PushVerify = '{SelectedAgent.vchar_PushVerify}' WHERE [int_AGid]={SelectedAgent.int_AGid}";
                    }

                    if (SQLHelper.UpDateSQL(sqlStr))
                    {
                        MessageBoxService.ShowMessage("保存成功", "提示信息", MessageButton.OK, MessageIcon.Information);
                    }
                    else
                    {
                        MessageBoxService.ShowMessage("保存失败", "提示信息", MessageButton.OK, MessageIcon.Error);
                    }

                    var item = Agents.FirstOrDefault(a => a.int_AGid == SelectedAgent.int_AGid);
                    if (item != null)
                    {
                        item = SelectedAgent;
                    }
                }
                SplashScreenService.HideSplashScreen();
                #endregion
            }
            catch (Exception ex)
            {
                SplashScreenService.HideSplashScreen();
                MessageBoxService.ShowMessage($"保存失败:{ex.Message}", "提示信息", MessageButton.OK, MessageIcon.Error);
            }

        }

        public virtual void AgentTypeChanged()
        {
            if (SelectedAgent != null)
            {
                if (SelectedAgent.int_AGtype == "检查点提供方")
                {
                    IsGPCSync = true;
                    IsGPCQuery = false;
                    IsGPCPush = false;
                }
                else if (SelectedAgent.int_AGtype == "检查点查询方")
                {
                    IsGPCSync = false;
                    IsGPCQuery = true;
                    IsGPCPush = false;
                }
                else if (SelectedAgent.int_AGtype == "检查点推送接收方")
                {
                    IsGPCSync = false;
                    IsGPCQuery = false;
                    IsGPCPush = true;
                }
            }
        }

        public virtual void DeleteSelectedAgent()
        {
            if (SelectedAgent != null)
            {
                if (MessageBoxService.ShowMessage("您确定要删除选中的商业代理吗？", "提示信息", MessageButton.OKCancel, MessageIcon.Question) == MessageResult.OK)
                {
                    string sqlStr = $"UPDATE tb_Agent SET IsOpen=0 WHERE int_AGid={SelectedAgent.int_AGid}";
                    if (SQLHelper.UpDateSQL(sqlStr))
                    {
                        Agents.Remove(SelectedAgent);
                    }
                    else
                    {
                        MessageBoxService.ShowMessage("删除失败！", "提示信息", MessageButton.OK, MessageIcon.Information);
                    }
                }
            }
            else
            {
                MessageBoxService.ShowMessage("请选中要删除的行？", "提示信息", MessageButton.OK, MessageIcon.Information);
            }
        }

        private void ClearAgentInfo()
        {
            SelectedAgent = new Agent();
            SelectedAgent.int_AGtype = "检查点提供方";
            SelectedAgent.int_synSpacing = 30;
            SelectedAgent.bit_synOpen = false;
            SelectedAgent.bit_synPush = false;
            SelectedAgent.bit_synQuery = false;
            SelectedAgent.vchar_AGcode = string.Empty;
            SelectedAgent.vchar_AGcontect = string.Empty;
            SelectedAgent.vchar_AGLinkMan = string.Empty;
            SelectedAgent.vchar_AGname = string.Empty;
            SelectedAgent.vchar_PushUser = string.Empty;
            SelectedAgent.vchar_PushVerify = string.Empty;
            SelectedAgent.vchar_QueryVerify = string.Empty;
            SelectedAgent.vchar_synStopKeyWord = string.Empty;
            SelectedAgent.vchar_synVerify = string.Empty;

        }

    }
}