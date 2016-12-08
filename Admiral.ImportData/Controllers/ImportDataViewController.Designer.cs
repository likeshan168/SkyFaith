namespace Admiral.ImportData.Controllers
{
    partial class ImportDataViewController
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
            this.ImportData = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // ImportData
            // 
            this.ImportData.Caption = "导入数据";
            this.ImportData.ConfirmationMessage = null;
            this.ImportData.Id = "ImportData";
            this.ImportData.ToolTip = null;
            this.ImportData.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.ImportData_Execute);
            // 
            // ImportDataViewController
            // 
            this.Actions.Add(this.ImportData);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction ImportData;
    }
}
