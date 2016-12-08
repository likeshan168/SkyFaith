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
using System.Collections;
using System.Windows.Forms;
using SkyFaith.Module.BusinessObjects.公共类;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.Web;

namespace SkyFaith.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class 顺丰订单操作 : ViewController
    {
        private ChoiceActionItem createMailNo;
        private ChoiceActionItem confirmOrder;
        private ChoiceActionItem cancelOrder;

        private ChoiceActionItem getMailNo;
        public 顺丰订单操作()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.

            #region 初始化choiceItems
            订单操作.ImageName = "BO_Order_Item";
            订单操作.Items.Clear();
            createMailNo = new ChoiceActionItem("创建运单号", null);
            createMailNo.ImageName = "createOrder";
            订单操作.Items.Add(createMailNo);
            confirmOrder = new ChoiceActionItem("确认订单", null);
            confirmOrder.ImageName = "Action_Grant";
            订单操作.Items.Add(confirmOrder);
            cancelOrder = new ChoiceActionItem("取消订单", null);
            cancelOrder.ImageName = "Action_Cancel";
            订单操作.Items.Add(cancelOrder);
            getMailNo = new ChoiceActionItem("获取运单号", null);
            getMailNo.ImageName = "GetOrder";
            订单操作.Items.Add(getMailNo);


            #endregion
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

        private void 订单操作_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            if (Application is WinApplication)
                ((WinApplication)Application).StartSplash();

            //IObjectSpace objectSpace = View is DevExpress.ExpressApp.ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            ArrayList objectsToProcess = new ArrayList(e.SelectedObjects);

            if (e.SelectedChoiceActionItem == createMailNo)
            {
                foreach (Order order in e.SelectedObjects)
                {
                    string xml = order.ExportToXml();
                    GetResponse(order.ExportToXml(), "OrderResponse", order);
                }
            }
            else if (e.SelectedChoiceActionItem == confirmOrder)
            {
                foreach (Order order in e.SelectedObjects)
                {
                    GetResponse(order.confirmOrder("1"), "OrderConfirmResponse", order);
                }
            }
            else if (e.SelectedChoiceActionItem == cancelOrder)
            {
                foreach (Order order in e.SelectedObjects)
                {
                    GetResponse(order.confirmOrder("2"), "OrderConfirmResponse", order);
                }
            }
            else if (e.SelectedChoiceActionItem == getMailNo)
            {
                foreach (Order order in e.SelectedObjects)
                {
                    GetResponse(order.SearchOrder(), "OrderSearchResponse", order);
                }
            }
            if (Application is WinApplication)
                ((WinApplication)Application).StopSplash();

        }

        private void GetResponse(string requestXml, string responseName, Order order)
        {
            string checkWorld = "j8DzkIFgmlomPt0aLuwU";
            string verifyCode = Encrypt.Instance.MD5Encrypt($"{requestXml}{checkWorld}");
            SFService.ExpressServiceClient client = new SFService.ExpressServiceClient();
            string result = client.sfexpressService(requestXml, verifyCode);
            XtraMessageBox.Show(order.GetResponseResult(result, responseName), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
