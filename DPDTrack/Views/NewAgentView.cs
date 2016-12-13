using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils.MVVM.UI;
using DPDModel.Models;
using DevExpress.Utils.MVVM;
using DPDTrack.ModelView;
using DPDTrack.Models;

namespace DPDTrack.Views
{
    
    public partial class NewAgentView : XtraUserControl
    {
        public NewAgentView()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                InitBindings();
              
            }
        }

        void OnDisposing()
        {
            var context = MVVMContext.FromControl(this);
            if (context != null) context.Dispose();
        }

        private void InitBindings()
        {
            MVVMContext.RegisterMessageBoxService();

            mvvmContext1.ViewModelType = typeof(NewAgentViewModel);
            var fluentAPI = mvvmContext1.OfType<NewAgentViewModel>();
            fluentAPI.SetBinding(txtAgentName, x => x.EditValue, x => x.Agent.vchar_AGname);
            fluentAPI.SetBinding(txtContactor, x => x.EditValue, x => x.Agent.vchar_AGLinkMan);
            fluentAPI.SetBinding(txtContactNum, x => x.EditValue, x => x.Agent.vchar_AGcontect);
            fluentAPI.SetBinding(txtAgentCode, x => x.EditValue, x => x.Agent.vchar_AGcode);
            fluentAPI.SetBinding(txtAgentType, x => x.EditValue, x => x.Agent.int_AGtype);

            fluentAPI.SetBinding(cbxOpenSyncServer, x => x.EditValue, x => x.Agent.bit_synOpen);
            fluentAPI.SetBinding(cbxSearchService, x => x.EditValue, x => x.Agent.bit_synQuery);
            fluentAPI.SetBinding(cbxOpenPushServer, x => x.EditValue, x => x.Agent.bit_synPush);

            fluentAPI.SetBinding(txtVerifyCode, x => x.EditValue, x => x.Agent.vchar_synVerify);
            fluentAPI.SetBinding(txtSearchVerifyCode, x => x.EditValue, x => x.Agent.vchar_QueryVerify);
            fluentAPI.SetBinding(txtPushServerVerifyCode, x => x.EditValue, x => x.Agent.vchar_PushVerify);
            fluentAPI.SetBinding(txtPushUser, x => x.EditValue, x => x.Agent.vchar_PushUser);

            fluentAPI.SetBinding(txtKeyWords, x => x.EditValue, x => x.Agent.vchar_synStopKeyWord);
            fluentAPI.SetBinding(txtSyncTimSpan, x => x.EditValue, x => x.Agent.int_synSpacing);

            //fluentAPI.SetBinding(gpcSync, x => x.Enabled, x => x.Agent.int_AGtype, t =>
            //{
            //    if (t == "检查点提供方")
            //        return true;
            //    else
            //        return false;
            //});
            //fluentAPI.SetBinding(gpcQuery, x => x.Enabled, x => x.Agent.int_AGtype, t =>
            //{
            //    if (t == "检查点查询方")
            //        return true;
            //    else
            //        return false;
            //});
            //fluentAPI.SetBinding(gpcPush, x => x.Enabled, x => x.Agent.int_AGtype, t =>
            //{
            //    if (t == "检查点推送接收方")
            //        return true;
            //    else
            //        return false;
            //});

            fluentAPI.SetBinding(gpcSync, x => x.Enabled, x => x.IsGPCSync);
            fluentAPI.SetBinding(gpcQuery, x => x.Enabled, x => x.IsGPCQuery);
            fluentAPI.SetBinding(gpcPush, x => x.Enabled, x => x.IsGPCPush);

            fluentAPI.BindCommand(btnSave, x => x.SaveAgentInfo());

            fluentAPI.WithEvent<EventArgs>(txtAgentType, "SelectedIndexChanged").EventToCommand(x => x.AgentTypeChanged());
        }

        //protected override void WndProc(ref Message m)
        //{

        //    if (m.Msg == (int)WindowsMessages.WM_NCDESTROY ||m.Msg == (int)WindowsMessages.WM_DESTROY)
        //    {
        //        Show();
        //        return;
        //    }
        //    base.WndProc(ref m);
        //}

    }
}
