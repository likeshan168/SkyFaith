using SkyFaith.Module.Controllers.标签打印;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.XtraReports.UI;
using DevExpress.ExpressApp;

namespace SkyFaith.Module.Win.Controllers
{
    public class WinPrintProductsController : 打印入库标签
    {
        protected override void PrintReport(IReportDataV2 reportData)
        {
            XtraReport report = ReportDataProvider.ReportsStorage.LoadReport(reportData);

            ReportsModuleV2 reportsModule = ReportsModuleV2.FindReportsModule(Application.Modules);

            if (reportsModule != null && reportsModule.ReportsDataSourceHelper != null)
            {
                reportsModule.ReportsDataSourceHelper.SetupBeforePrint(report, null, ((ListView)View).CollectionSource.Criteria["filter"], true, null, false);
                report.Print();
            }

        }
    }
}
