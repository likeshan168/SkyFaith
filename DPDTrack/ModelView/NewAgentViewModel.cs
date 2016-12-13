using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DPDModel.Models;
using DevExpress.Mvvm.POCO;

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

        private IMessageBoxService MessageBoxService
        {
            get
            {
                return this.GetService<IMessageBoxService>();
            }
        }

        public virtual void SaveAgentInfo()
        {
            if (string.IsNullOrWhiteSpace(Agent.vchar_AGname))
            {
                MessageBoxService.ShowMessage("商业伙伴名称不能为空", "提示信息", MessageButton.OK, MessageIcon.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(Agent.vchar_AGcode))
            {
                MessageBoxService.ShowMessage("商业伙伴代码不能为空", "提示信息", MessageButton.OK, MessageIcon.Error);
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

            MessageBoxService.ShowMessage(Agent.bit_synOpen.ToString(), "提示信息", MessageButton.OK, MessageIcon.Error);

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