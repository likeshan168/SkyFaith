using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using DPDModel.Models;
using DPDModelDPDModel.DAL;
using DevExpress.Mvvm.POCO;
using System.Windows.Forms;
using DPDTrack.Views;

namespace DPDTrack.ModelView
{
    [POCOViewModel]
    public class BusinessPartnerViewModel
    {
        public BusinessPartnerViewModel()
        {
            Agents = GetAgeents();
            IsSaveEnabled = false;
            ModifyButtonText = "修改";
        }

        public IMessageBoxService MessageBoxService
        {
            get
            {
                return this.GetService<IMessageBoxService>();
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
        /// <summary>
        /// 设置保存按钮是否可用
        /// </summary>
        public virtual bool IsSaveEnabled { get; set; }
        /// <summary>
        /// 设置修改按钮的显示名称
        /// </summary>
        public virtual string ModifyButtonText { get; set; }
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
        /// 点击修改按钮的事件
        /// </summary>
        public virtual void ModifyAgentInfo()
        {
            IsSaveEnabled = !IsSaveEnabled;
            if (IsSaveEnabled)
            {
                ModifyButtonText = "取消";
            }
            else
            {
                ModifyButtonText = "修改";
            }
        }
        /// <summary>
        /// 点击添加按钮的事件
        /// </summary>
        public virtual void AddAgentInfo()
        {
            //MessageResult result = DialogService.ShowDialog(MessageButton.OKCancel, "新增代理商信息", "AddAgent", null, this);
            //new NewAgentFrm
            NewAgentFrm newAgentFrm = new NewAgentFrm();
            DialogResult result = newAgentFrm.ShowDialog();
        }

        public virtual void SaveAgentInfo()
        {
            MessageBoxService.ShowMessage(SelectedAgent.vchar_AGname);

        }

        public virtual void AgentTypeChanged()
        {
            MessageBoxService.ShowMessage(SelectedAgent.vchar_AGname);
        }

        private void ClearAgentInfo()
        {
            //SelectedAgent = new Agent();
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