using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Web.Templates;
using System.Web.UI;
using DevExpress.ExpressApp.Web.Templates.ActionContainers;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Web.Controls;

namespace SkyFaith.Web
{
    public partial class DefaultVerticalTemplateContent1 : TemplateContent, IHeaderImageControlContainer, IXafPopupWindowControlContainer
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            WebApplication.Instance.ClientInfo.SetInfo(ClientParams);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (WebWindow.CurrentRequestWindow != null)
            {
                WebWindow.CurrentRequestWindow.PagePreRender += new EventHandler(CurrentRequestWindow_PagePreRender);
            }
        }
        protected override void OnUnload(EventArgs e)
        {
            if (WebWindow.CurrentRequestWindow != null)
            {
                WebWindow.CurrentRequestWindow.PagePreRender -= new EventHandler(CurrentRequestWindow_PagePreRender);
            }
            base.OnUnload(e);
        }
        private void CurrentRequestWindow_PagePreRender(object sender, EventArgs e)
        {
            WebWindow.CurrentRequestWindow.RegisterStartupScript("OnLoadCore", string.Format(@"Init(""{0}"", ""VerticalCS"");OnLoadCore(""LPcell"", ""separatorCell"", ""separatorImage"", true, true);", BaseXafPage.CurrentTheme), true);
            UpdateTRPVisibility();
        }
        private void UpdateTRPVisibility()
        {
            bool isVisible = false;
            foreach (Control control in TRP.Controls)
            {
                if (control is ActionContainerHolder)
                {
                    if (((ActionContainerHolder)control).HasActiveActions())
                    {
                        isVisible = true;
                        break;
                    }
                }
            }
            TRP.Visible = isVisible;
        }
        public override IActionContainer DefaultContainer
        {
            get
            {
                if (TB != null)
                {
                    return TB.FindActionContainerById("View");
                }
                return null;
            }
        }
        public override void SetStatus(ICollection<string> statusMessages)
        {
            InfoMessagesPanel.Text = string.Join("<br>", new List<string>(statusMessages).ToArray());
        }
        public override object ViewSiteControl
        {
            get
            {
                return VSC;
            }
        }
        public ActionContainerHolder LeftToolsActionContainer { get { return VTC; } }
        public ActionContainerHolder DiagnosticActionContainer { get { return DAC; } }
        public ActionContainerHolder MainToolBarActionContainer { get { return TB; } }
        public ActionContainerHolder SecurityActionContainer { get { return SAC; } }
        public ActionContainerHolder TopToolsActionContainer { get { return SHC; } }

        public XafPopupWindowControl XafPopupWindowControl
        {
            get { return PopupWindowControl; }
        }
        public ThemedImageControl HeaderImageControl { get { return TIC; } }
    }
}
