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
using DPDTrack.ModelView;
using DPDModel.Models;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Utils.MVVM;
using DevExpress.Utils.MVVM.Services;

namespace DPDTrack.Views
{
    public partial class BusinessPartnerView : XtraUserControl
    {
        public BusinessPartnerView()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                splashScreenManager1.ShowWaitForm();
                InitBindings();
                splashScreenManager1.CloseWaitForm();
            }
        }

        void OnDisposing()
        {
            var context = MVVMContext.FromControl(this);
            if (context != null) context.Dispose();
        }

        private void InitBindings()
        {
            MVVMContext.RegisterXtraMessageBoxService();
            MVVMContext.RegisterXtraDialogService();

            mvvmContext1.ViewModelType = typeof(BusinessPartnerViewModel);
            var fluentAPI = mvvmContext1.OfType<BusinessPartnerViewModel>();
            //mvvmContext1.RegisterService(new NewAgentFrm());
            mvvmContext1.RegisterService(SplashScreenService.Create(splashScreenManager1));
            fluentAPI.SetBinding(gridControl1, gv => gv.DataSource, x => x.Agents);

            fluentAPI.WithEvent<ColumnView, FocusedRowObjectChangedEventArgs>(gridView1, "FocusedRowObjectChanged")
               .SetBinding(x => x.SelectedAgent,
                           args => args.Row as Agent,
                           (gView, entity) => 
                           {
                               gView.FocusedRowHandle = gView.FindRow(entity);
                               if (entity != null)
                               {
                                   gpcPush.Enabled = entity.bit_synPush;
                                   gpcQuery.Enabled = entity.bit_synQuery;
                                   gpcSync.Enabled = entity.bit_synOpen;
                               }
                           });

            fluentAPI.SetBinding(txtAgentName, x => x.EditValue, x => x.SelectedAgent.vchar_AGname);
            fluentAPI.SetBinding(txtContactor, x => x.EditValue, x => x.SelectedAgent.vchar_AGLinkMan);
            fluentAPI.SetBinding(txtContactNum, x => x.EditValue, x => x.SelectedAgent.vchar_AGcontect);
            fluentAPI.SetBinding(txtAgentCode, x => x.EditValue, x => x.SelectedAgent.vchar_AGcode);
            fluentAPI.SetBinding(txtAgentType, x => x.EditValue, x => x.SelectedAgent.int_AGtype);

            fluentAPI.SetBinding(cbxOpenSyncServer, x => x.EditValue, x => x.SelectedAgent.bit_synOpen);
            fluentAPI.SetBinding(cbxSearchService, x => x.EditValue, x => x.SelectedAgent.bit_synQuery);
            fluentAPI.SetBinding(cbxOpenPushServer, x => x.EditValue, x => x.SelectedAgent.bit_synPush);

            fluentAPI.SetBinding(txtVerifyCode, x => x.EditValue, x => x.SelectedAgent.vchar_synVerify);
            fluentAPI.SetBinding(txtSearchVerifyCode, x => x.EditValue, x => x.SelectedAgent.vchar_QueryVerify);
            fluentAPI.SetBinding(txtPushServerVerifyCode, x => x.EditValue, x => x.SelectedAgent.vchar_PushVerify);
            fluentAPI.SetBinding(txtPushUser, x => x.EditValue, x => x.SelectedAgent.vchar_PushUser);

            fluentAPI.SetBinding(txtKeyWords, x => x.EditValue, x => x.SelectedAgent.vchar_synStopKeyWord);
            fluentAPI.SetBinding(txtSyncTimSpan, x => x.EditValue, x => x.SelectedAgent.int_synSpacing);

            fluentAPI.SetBinding(btnSave, x => x.Enabled, x => x.IsSaveEnabled);
            fluentAPI.SetBinding(btnAdd, x => x.Enabled, x => x.IsAddEnabled);
            fluentAPI.SetBinding(btnModify, x => x.Enabled, x => x.IsModifyEnabled);

            fluentAPI.BindCommand(btnModify, x => x.ModifyAgentInfo());
            fluentAPI.BindCommand(btnAdd, x => x.AddAgentInfo());
            fluentAPI.BindCommand(btnDelete, x => x.DeleteSelectedAgent());
            fluentAPI.BindCommand(btnSave, x => x.SaveAgentInfo());

            fluentAPI.SetBinding(btnModify, x => x.Text, x => x.ModifyButtonText);
            fluentAPI.SetBinding(gpcSync, x => x.Enabled, x => x.IsGPCSync);
            fluentAPI.SetBinding(gpcQuery, x => x.Enabled, x => x.IsGPCQuery);
            fluentAPI.SetBinding(gpcPush, x => x.Enabled, x => x.IsGPCPush);

            fluentAPI.WithEvent<EventArgs>(txtAgentType, "SelectedIndexChanged").EventToCommand(x => x.AgentTypeChanged());
        }
    }
}
