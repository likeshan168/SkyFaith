namespace SkyFaith.Module.Controllers
{
    partial class 顺丰订单操作
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.订单操作 = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // 订单操作
            // 
            this.订单操作.Caption = "订单操作";
            this.订单操作.Category = "Edit";
            this.订单操作.ConfirmationMessage = null;
            this.订单操作.Id = "订单操作";
            this.订单操作.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.订单操作.TargetObjectType = typeof(SkyFaith.Module.BusinessObjects.顺丰订单管理.Order);
            this.订单操作.ToolTip = null;
            this.订单操作.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.订单操作_Execute);
            // 
            // 顺丰订单操作
            // 
            this.Actions.Add(this.订单操作);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction 订单操作;
    }
}
