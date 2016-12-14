using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DPDModel.Models;
using DevExpress.Mvvm.POCO;
using DPDModelDPDModel.DAL;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DPDTrack.Views;
using DPDTrack.Models;

namespace DPDTrack.ModelView
{
    [POCOViewModel]
    public class NewAgentViewModel
    {
        public NewAgentViewModel()
        {
            Agent = ViewModelSource.Create<Agent>();
            Agent.int_AGtype = "检查点提供方";
            IsGPCSync = true;
            IsGPCQuery = false;
            IsGPCPush = false;
        }
        public virtual Agent Agent { get; set; }

        public virtual bool IsGPCSync { get; set; }
        public virtual bool IsGPCQuery { get; set; }
        public virtual bool IsGPCPush { get; set; }

        public virtual NewAgentFrm NewAgentFrm
        {
            get;
            set;
        }

        public virtual DialogResult DResult { get; set; }

        private IMessageBoxService MessageBoxService
        {
            get
            {
                return this.GetService<IMessageBoxService>();
            }
        }

        protected ISplashScreenService SplashScreenService
        {
            get { return this.GetService<ISplashScreenService>(); }
        }

        /// <summary>
        /// 新增商业代理信息
        /// </summary>
        public virtual void SaveAgentInfo()
        {
            try
            {
                #region 验证重复
                if (string.IsNullOrWhiteSpace(Agent.vchar_AGname))
                {
                    MessageBoxService.ShowMessage("商业伙伴名称不能为空", "提示信息", MessageButton.OK, MessageIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(Agent.vchar_AGcontect))
                {
                    MessageBoxService.ShowMessage("联系方式不能为空", "提示信息", MessageButton.OK, MessageIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(Agent.vchar_AGLinkMan))
                {
                    MessageBoxService.ShowMessage("联系人不能为空", "提示信息", MessageButton.OK, MessageIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(Agent.vchar_AGcode))
                {
                    MessageBoxService.ShowMessage("商业伙伴代码不能为空", "提示信息", MessageButton.OK, MessageIcon.Error);
                    return;
                }
                string sqlStr = string.Empty;

                if (Agent.bit_synOpen)
                {
                    if (string.IsNullOrWhiteSpace(Agent.vchar_synVerify) || string.IsNullOrWhiteSpace(Agent.vchar_synStopKeyWord))
                    {
                        MessageBoxService.ShowMessage("检查点同步信息不可为空", "提示信息", MessageButton.OK, MessageIcon.Error);
                        return;
                    }
                    sqlStr = $"SELECT [vchar_AGcode],[vchar_synVerify] FROM [dbo].[tb_Agent] WHERE [vchar_AGcode]='{Agent.vchar_AGcode}' OR [vchar_synVerify]='{Agent.vchar_synVerify}'";
                    IList<Agent> agents = (SQLHelper.GetObject<Agent>(sqlStr)).ToList();
                    if (agents.Count != 0)
                    {
                        Agent agent = agents.FirstOrDefault();
                        if (Agent.vchar_AGcode == agent.vchar_AGcode)
                        {
                            MessageBoxService.ShowMessage("与其他商业伙伴代码重复，请更换", "提示信息", MessageButton.OK, MessageIcon.Error);
                            return;
                        }
                        if (Agent.vchar_synVerify == agent.vchar_synVerify)
                        {
                            MessageBoxService.ShowMessage("与其他商业伙伴同步接口校验码重复，请更换", "提示信息", MessageButton.OK, MessageIcon.Error);
                            return;
                        }
                    }
                }
                if (Agent.bit_synQuery)
                {
                    if (string.IsNullOrWhiteSpace(Agent.vchar_QueryVerify))
                    {
                        MessageBoxService.ShowMessage("检查点查询信息不可为空", "提示信息", MessageButton.OK, MessageIcon.Error);
                        return;
                    }
                    sqlStr = $"SELECT [vchar_QueryVerify] FROM [dbo].[tb_Agent] WHERE [vchar_QueryVerify]='{Agent.vchar_QueryVerify}'";
                    IList<Agent> agents = (SQLHelper.GetObject<Agent>(sqlStr)).ToList();
                    if (agents.Count != 0)
                    {
                        Agent agent = agents.FirstOrDefault();
                        if (Agent.vchar_QueryVerify == agent.vchar_QueryVerify)
                        {
                            MessageBoxService.ShowMessage("与其他商业伙伴查询接口校验码重复，请更换", "提示信息", MessageButton.OK, MessageIcon.Error);

                            return;
                        }
                    }
                }
                if (Agent.bit_synPush)
                {
                    if (string.IsNullOrWhiteSpace(Agent.vchar_PushUser) || string.IsNullOrWhiteSpace(Agent.vchar_PushVerify))
                    {
                        MessageBoxService.ShowMessage("检查点推送信息不可为空", "提示信息", MessageButton.OK, MessageIcon.Error);
                        return;
                    }

                    sqlStr = $"SELECT [vchar_PushUser],[vchar_PushVerify] FROM [dbo].[tb_Agent] WHERE [vchar_PushUser]='{Agent.vchar_PushUser}' OR [vchar_PushVerify]='{Agent.vchar_PushVerify}'";

                    IList<Agent> agents = (SQLHelper.GetObject<Agent>(sqlStr)).ToList();
                    if (agents.Count != 0)
                    {
                        Agent agent = agents.FirstOrDefault();

                        if (Agent.vchar_PushUser == agent.vchar_PushUser)
                        {
                            MessageBoxService.ShowMessage("与其他商业伙伴推送接口用户重复，请更换", "提示信息", MessageButton.OK, MessageIcon.Error);
                            return;
                        }
                        if (Agent.vchar_PushVerify == agent.vchar_PushVerify)
                        {
                            MessageBoxService.ShowMessage("与其他商业伙伴推送接口校验码重复，请更换", "提示信息", MessageButton.OK, MessageIcon.Error);
                            return;
                        }
                    }
                }

                #endregion
                #region 插入数据
                SplashScreenService.ShowSplashScreen();
                if (Agent.int_AGtype == "检查点提供方")
                {
                    sqlStr = $"INSERT INTO tb_Agent (vchar_AGcode, vchar_AGname, vchar_AGLinkMan, vchar_AGcontect, int_AGtype, bit_synOpen, vchar_synVerify,vchar_synStopKeyWord,int_synSpacing)VALUES ('{Agent.vchar_AGcode}', N'{Agent.vchar_AGname}', N'{Agent.vchar_AGLinkMan}', '{Agent.vchar_AGcontect}', 0, {(Agent.bit_synOpen ? 1 : 0)}, '{Agent.vchar_synVerify}', N'{Agent.vchar_synStopKeyWord}',{Agent.int_synSpacing}) ";
                }
                else if (Agent.int_AGtype == "检查点查询方")
                {
                    sqlStr = $"INSERT INTO tb_Agent(vchar_AGcode, vchar_AGname, vchar_AGLinkMan, vchar_AGcontect, int_AGtype, bit_synQuery, vchar_QueryVerify)VALUES ('{Agent.vchar_AGcode}', N'{Agent.vchar_AGname}',N'{Agent.vchar_AGLinkMan}', '{Agent.vchar_AGcontect}', 1, {(Agent.bit_synQuery ? 1 : 0)}, '{Agent.vchar_QueryVerify}') ";

                }
                else if (Agent.int_AGtype == "检查点推送接收方")
                {
                    sqlStr = $"INSERT INTO tb_Agent(vchar_AGcode, vchar_AGname, vchar_AGLinkMan, vchar_AGcontect, int_AGtype,bit_synPush, vchar_PushUser, vchar_PushVerify) VALUES ('{Agent.vchar_AGcode}', N'{Agent.vchar_AGname}', N'{Agent.vchar_AGLinkMan}', '{Agent.vchar_AGcontect}', 2, {(Agent.bit_synPush ? 1 : 0)}, '{Agent.vchar_PushUser}', '{Agent.vchar_PushVerify}') ";
                }

                if (SQLHelper.UpDateSQL(sqlStr))
                {
                    MessageBoxService.ShowMessage("保存成功", "提示信息", MessageButton.OK, MessageIcon.Information);
                    NewAgentFrm.Close();
                    NewAgentFrm.DialogResult = DialogResult.OK;

                }
                else
                {
                    MessageBoxService.ShowMessage("保存失败", "提示信息", MessageButton.OK, MessageIcon.Error);
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
            if (Agent.int_AGtype == "检查点提供方")
            {
                IsGPCSync = true;
                IsGPCQuery = false;
                IsGPCPush = false;
            }
            else if (Agent.int_AGtype == "检查点查询方")
            {
                IsGPCSync = false;
                IsGPCQuery = true;
                IsGPCPush = false;
            }
            else if (Agent.int_AGtype == "检查点推送接收方")
            {
                IsGPCSync = false;
                IsGPCQuery = false;
                IsGPCPush = true;
            }
        }
    }
}