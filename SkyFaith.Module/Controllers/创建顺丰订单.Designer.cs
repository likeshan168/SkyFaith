namespace SkyFaith.Module.Controllers
{
    partial class 创建顺丰订单
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
            this.获取顺丰运单号 = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // 获取顺丰运单号
            // 
            this.获取顺丰运单号.Caption = "获取顺丰运单号";
            this.获取顺丰运单号.Category = "Edit";
            this.获取顺丰运单号.ConfirmationMessage = null;
            this.获取顺丰运单号.Id = "获取顺丰运单号";
            this.获取顺丰运单号.TargetObjectType = typeof(SkyFaith.Module.BusinessObjects.顺丰订单管理.Order);
            this.获取顺丰运单号.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.获取顺丰运单号.ToolTip = null;
            this.获取顺丰运单号.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.获取顺丰运单号_Execute);
            // 
            // 创建顺丰订单
            // 
            this.Actions.Add(this.获取顺丰运单号);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction 获取顺丰运单号;
    }
}
