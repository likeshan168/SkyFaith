using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Validation;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.ExpressApp.Web.Templates;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Spreadsheet;
using DevExpress.Web;
using DevExpress.Web.ASPxSpreadsheet;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admiral.ImportData.Module.Web.Editors
{
    [PropertyEditor(typeof(IImportOption), true)]
    public class ASPxSpreadsheetPropertyEditor : ASPxPropertyEditor, IComplexControl, IComplexViewItem, IXafCallbackHandler
    {
        public static bool IsNewStyle { get; set; }
        public static string SkinName { get; set; }
        // Fields
        private ASPxSpreadsheet _spreadsheet;

        // Methods
        public ASPxSpreadsheetPropertyEditor(Type objectType, IModelMemberViewItem model)
            : base(objectType, model)
        {
            importer = new ExcelImporter();
        }
        Guid documentVersion = Guid.Empty;
        protected override WebControl CreateEditModeControlCore()
        {
            this._spreadsheet = new ASPxSpreadsheet();

            _spreadsheet.SettingsDocumentSelector.UploadSettings.Enabled = true;
            _spreadsheet.SettingsDocumentSelector.UploadSettings.UseAdvancedUploadMode = true;
            _spreadsheet.SettingsDocumentSelector.UploadSettings.AdvancedModeSettings.EnableFileList = true;
            _spreadsheet.SettingsDocumentSelector.UploadSettings.AdvancedModeSettings.EnableMultiSelect = true;
            _spreadsheet.SettingsDocumentSelector.UploadSettings.AutoStartUpload = true;

            //初始目录：当前登陆用户+当天日期
            var dir = "~/ExcelFiles/" + SecuritySystem.CurrentUserName + "/" + DateTime.Now.ToString("yyyyMMdd");

            _spreadsheet.WorkDirectory = dir;

            _spreadsheet.ShowConfirmOnLosingChanges = false;
            _spreadsheet.ClientSideEvents.Init = "function(s, e){ s.SetFullscreenMode(true); }";
            _spreadsheet.Height = 800;
            _spreadsheet.CreateDefaultRibbonTabs(true);
            _spreadsheet.ActiveTabIndex = 0;//默认标签行这个可以打开的时候默认第一个导入的标签

            //_spreadsheet.ClientSideEvents.CustomCommandExecuted
            if (IsNewStyle)
            {
                _spreadsheet.ClientSideEvents.Init = "function(s, e){ s.SetFullscreenMode(false); }";
                _spreadsheet.Theme = "Aqua";
            }

            _spreadsheet.Load += (s, e) =>
            {
                var handlerid = _spreadsheet.UniqueID + "StartImport";

                var page = _spreadsheet.Page as ICallbackManagerHolder;

                page.CallbackManager.RegisterHandler(handlerid, this);


                var doAction = page.CallbackManager.GetScript(handlerid, "null");
                _spreadsheet.ClientSideEvents.CustomCommandExecuted = "function(s,e) { " + doAction + " }";
            };

            var t = _spreadsheet.RibbonTabs[0];
            var g = t.Groups.Add("导入");
            //_spreadsheet.Document
            var temp = new RibbonButtonItem("ImportData", "导入数据", RibbonItemSize.Large);
            temp.LargeImage.Url = "~/Images/ImportData_32x32.png";

            //new RibbonTemplateItem();
            g.Items.Add(temp);
            //StartImport = new StartImportTemplate(_spreadsheet, this._objectSpace, this._application);
            //temp.Template = StartImport;
            
            importer.InitializeExcelSheet(_spreadsheet.Document, this.option);

            //_spreadsheet.d
            return _spreadsheet;
        }
        ExcelImporter importer;

        IImportOption option
        {
            get
            {
                return this.PropertyValue as IImportOption;
            }
        }



        protected override WebControl CreateViewModeControlCore()
        {
            return CreateEditModeControlCore();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _spreadsheet = null;
            }
            base.Dispose(disposing);
        }



        protected override void ReadEditModeValueCore()
        {
            base.ReadEditModeValueCore();
        }

        protected override void SetImmediatePostDataCompanionScript(string script)
        {
        }

        IObjectSpace _objectSpace;
        XafApplication _application;

        public void Setup(IObjectSpace objectSpace, XafApplication application)
        {
            this._objectSpace = objectSpace;
            this._application = application;
            importer.Setup(application);
        }

        public void ProcessAction(string parameter)
        {
           importer.ProcessImportAction(_spreadsheet.Document);
        }

       

    }



    public class StartImportTemplate : ITemplate, IXafCallbackHandler
    {
        ASPxSpreadsheet _spreadSheet;
        XafApplication _application;
        public StartImportTemplate(ASPxSpreadsheet spreadSheet, IObjectSpace os, XafApplication xaf)
        {
            _spreadSheet = spreadSheet;
            this._application = xaf;
            objectSpace = os;
        }

        public string HandlerID
        {
            get
            {
                return _spreadSheet.UniqueID + "_startImport";
            }
        }

        public void InstantiateIn(Control container)
        {
            var btn = new ASPxButton();
            btn.UseSubmitBehavior = false;
            btn.AutoPostBack = false;
            var page = _spreadSheet.Page as ICallbackManagerHolder;
            var doAction = page.CallbackManager.GetScript(HandlerID, "null");

            btn.ClientSideEvents.Click = "function(s,e){ debugger;" + doAction + "}";

            btn.Text = "开始导入";
            container.Controls.Add(btn);
        }


        IObjectSpace objectSpace;
        public void ProcessAction(string parameter)
        {

        }
    }


    //public class SheetContext
    //{
    //    public SheetContext(Worksheet workbook)
    //    {
    //        this.workbook = workbook;
    //        var cells = workbook.Rows[1];
    //        for (int i = 1; i <= workbook.Columns.LastUsedIndex; i++)
    //        {
    //            CaptionIndex.Add(cells[i].DisplayText, i);
    //        }
    //    }

    //    Worksheet workbook;
    //    Dictionary<string, int> CaptionIndex = new Dictionary<string, int>();

    //    public Cell GetErrorCell(string caption, int rowIndex)
    //    {
    //        return this.workbook.Cells[rowIndex, CaptionIndex[caption]];
    //    }
    //}

    //public class SheetRowObject
    //{
    //    public SheetRowObject(SheetContext context)
    //    {
    //        this.context = context;
    //    }

    //    SheetContext context;

    //    public int Row { get; set; }

    //    public XPBaseObject Object { get; set; }

    //    public Row RowObject { get; set; }

    //    StringBuilder message = new StringBuilder();
    //    List<Cell> errorCells = new List<Cell>();
    //    public void AddErrorMessage(string msg, Cell errorCell)
    //    {
    //        HasError = true;
    //        message.Append(msg);
    //        errorCells.Add(errorCell);

    //        errorCell.FillColor = Color.Red;
    //        errorCell.Font.Color = Color.White;
    //        var s = errorCell.Worksheet;
    //        var tipCell = s.Cells[errorCell.RowIndex, 0];
    //        tipCell.Value = tipCell.DisplayText + "\n" + msg;
    //    }

    //    public void AddErrorMessage(string msg, IEnumerable<string> properties)
    //    {
    //        RowObject[0].Value = RowObject[0].DisplayText + "\n" + msg + "错误字段:" + string.Join(",", properties);
    //        foreach (var item in properties)
    //        {
    //            var cell = context.GetErrorCell(item, Row);
    //            cell.FillColor = Color.Red;
    //            cell.Font.Color = Color.White;
    //        }
    //    }

    //    public bool HasError { get;private set; }

    //    public string GetErrorMessage()
    //    {
    //        return message.ToString();
    //    }
        
    //}
}
