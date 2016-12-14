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
using DevExpress.Utils.MVVM.Services;

namespace DPDTrack.Views
{

    public partial class NewAgentView : XtraUserControl
    {
        public NewAgentView()
        {
            InitializeComponent();
            Load += NewAgentView_Load;
        }

        private void NewAgentView_Load(object sender, EventArgs e)
        {
            InitBindings();
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
            mvvmContext1.RegisterService(SplashScreenService.Create(splashScreenManager1));
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
            var viewModel = mvvmContext1.GetViewModel<NewAgentViewModel>();
            if (Parent is NewAgentFrm)
                viewModel.NewAgentFrm = Parent as NewAgentFrm;


            fluentAPI.SetBinding(gpcSync, x => x.Enabled, x => x.IsGPCSync);
            fluentAPI.SetBinding(gpcQuery, x => x.Enabled, x => x.IsGPCQuery);
            fluentAPI.SetBinding(gpcPush, x => x.Enabled, x => x.IsGPCPush);

            fluentAPI.BindCommand(btnSave, x => x.SaveAgentInfo());

            fluentAPI.WithEvent<EventArgs>(txtAgentType, "SelectedIndexChanged").EventToCommand(x => x.AgentTypeChanged());
        }

    }
}
