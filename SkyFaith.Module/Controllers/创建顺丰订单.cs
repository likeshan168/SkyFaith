using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using SkyFaith.Module.BusinessObjects.顺丰订单管理;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using SkyFaith.Module.BusinessObjects.公共类;

namespace SkyFaith.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class 创建顺丰订单 : ViewController
    {
        public 创建顺丰订单()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            获取顺丰运单号.ImageName = "Action_Grant";
            获取顺丰运单号.TargetObjectsCriteria = (CriteriaOperator.Parse("[mailno] is null")).ToString();
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void 获取顺丰运单号_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            foreach (Order order in e.SelectedObjects)
            {
                string xml = order.ExportToXml();
                string checkWorld = "j8DzkIFgmlomPt0aLuwU";
                string verifyCode = Encrypt.Instance.MD5Encrypt($"{xml}{checkWorld}");
                SFService.ExpressServiceClient client = new SFService.ExpressServiceClient();
                string result = client.sfexpressService(xml, verifyCode);
                XtraMessageBox.Show(result, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //View.ObjectSpace.CommitChanges();
            //View.ObjectSpace.Refresh();

        }
    }
}
