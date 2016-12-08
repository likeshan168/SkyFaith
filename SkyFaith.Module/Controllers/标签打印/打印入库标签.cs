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
using SkyFaith.Module.BusinessObjects.鞋标签打印;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.Persistent.BaseImpl;

namespace SkyFaith.Module.Controllers.标签打印
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class 打印入库标签 : ViewController
    {
        public 打印入库标签()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.


            //parametrizedAction1.Category = "View";
            //parametrizedAction1.Id = "打印入库标签";
            //parametrizedAction1.ImageName = "Action_Printing_Print";
            //parametrizedAction1.Caption = "打印入库标签";
            parametrizedAction1.Execute += ParametrizedAction_Execute;


        }

        private void ParametrizedAction_Execute(object sender, ParametrizedActionExecuteEventArgs e)
        {
            //IObjectSpace objectSpace = Application.CreateObjectSpace();
            string parameter = e.ParameterCurrentValue as string;
            ListView view= (ListView)View;
            if (!string.IsNullOrWhiteSpace(parameter))
            {
                view.CollectionSource.Criteria["filter"] = CriteriaOperator.Parse("[条形码]=? OR [京东码]=? OR [旧京东码]=?", parameter, parameter, parameter);
                view.CollectionSource.ResetCollection();
                if(view.CollectionSource.GetCount()!=0)
                {
                    IObjectSpace rptObjectSpace = ReportDataProvider.ReportObjectSpaceProvider.CreateObjectSpace(typeof(ReportDataV2));
                    IReportDataV2 reportData = rptObjectSpace.FindObject<ReportDataV2>(
                        new BinaryOperator("DisplayName", "入库标签"));
                    if (reportData == null)
                    {
                        throw new UserFriendlyException("Cannot find the '入库标签' report.");
                    }
                    else
                    {
                        PrintReport(reportData);
                    }
                }
            }
            else
            {
                view.CollectionSource.Criteria.Remove("filter");
                view.CollectionSource.ResetCollection();
            }
            //object obj = objectSpace.FindObject(((ListView)View).ObjectTypeInfo.Type, CriteriaOperator.Parse("[条形码]=? OR [京东码]=? OR [旧京东码]=?", parameter, parameter, parameter));
            //if (obj != null)
            //{
            //DetailView detailView = Application.CreateDetailView(objectSpace, obj);
            //detailView.ViewEditMode = ViewEditMode.Edit;
            //e.ShowViewParameters.CreatedView = detailView;

            //}

        }

        protected virtual void PrintReport(IReportDataV2 reportData) { }


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
    }
}
