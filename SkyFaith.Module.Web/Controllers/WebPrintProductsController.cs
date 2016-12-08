
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.Xpo;
using SkyFaith.Module.Controllers.标签打印;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.SystemModule;
using DevExpress.ExpressApp;
using System.Web;

namespace SkyFaith.Module.Web.Controllers
{
    public class WebPrintProductsController : 打印入库标签
    {
        public WebPrintProductsController() : base()
        {

            //WebApplication.Instance.Modules
            //((WebWindow)WebApplication.Instance.MainWindow).
        }
        protected override void PrintReport(IReportDataV2 reportData)
        {
            string reportContainerHandle = ReportDataProvider.ReportsStorage.GetReportContainerHandle(reportData);
            ((WebWindow)WebApplication.Instance.MainWindow).RegisterStartupScript(
                "InstantPrintReport", GetPrintingScript(reportContainerHandle), overwrite: true);

        }

        private string GetPrintingScript(string reportContainerHandle)
        {
            string criteria = ((ListView)View).CollectionSource.Criteria["filter"].LegacyToString();
            string str = string.Format(@"
                        var iframe = document.getElementById('reportout');
                        if (iframe != null) {{
                            document.body.removeChild(iframe);
                        }}
                        iframe = document.createElement('iframe');
                        iframe.setAttribute('id', 'reportout');
                        iframe.style.width = 0 + 'px';
                        iframe.style.height = 0 + 'px';
                        iframe.style.display = 'none';
                        document.body.appendChild(iframe);
                        document.getElementById('reportout').contentWindow.location = 'InstantPrintReport.aspx?reportContainerHandle={0}&criteria={1}';
        ", reportContainerHandle, HttpUtility.JavaScriptStringEncode(criteria));
            return str;
        }

    }
}
