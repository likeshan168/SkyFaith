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
using DevExpress.XtraSpreadsheet;
using Admiral.ImportData.Win.Editors;
using System.Windows.Forms;

namespace Admiral.ImportData.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class WinImportViewController : ViewController
    {
        public WinImportViewController()
        {
            InitializeComponent();
            TargetObjectType = typeof(ImportData);
            TargetViewType = ViewType.DetailView;
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            if (this.Frame != null)
            {

            }
        }
        
        protected override void OnFrameAssigned()
        {
            base.OnFrameAssigned();
        }

        protected override void OnViewControlsCreated()
        {
            var form = Frame.Template as Form;
            var t = form as DevExpress.ExpressApp.Win.Templates.Ribbon.DetailRibbonFormV2;
            var sspe = ((this.View as DetailView).FindItem("Option") as SpreadSheetPropertyEditor);
            var ssc = sspe.Control as SpreadsheetControl;
            new SpreadSheetRibbonCreator(ssc, t?.Ribbon, sspe.Importer, form);
            
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void 打开文件_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var ssc = ((this.View as DetailView).FindItem("Option") as SpreadSheetPropertyEditor).Control as SpreadsheetControl;
            //ssc.Document.LoadDocument()
        }
    }
}
