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
using DevExpress.ExpressApp.Win.Templates.ActionContainers;
using DevExpress.XtraNavBar;

namespace SkyFaith.Module.Win.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppWindowControllertopic.aspx.
    public partial class NavBarViewChangingController : WindowController
    {
        private NavigationActionContainer navActionContainer;
        public NavBarViewChangingController()
        {
            InitializeComponent();
            // Target required Windows (via the TargetXXX properties) and create their Actions.
            //Window.ProcessActionContainer += Window_ProcessActionContainer;
        }

        private void Window_ProcessActionContainer(object sender, ProcessActionContainerEventArgs e)
        {
            UnsubscribeFromContainerEvents();
            if (e.ActionContainer is NavigationActionContainer)
            {
                navActionContainer = e.ActionContainer as NavigationActionContainer;
                SubscribeToContainerEvents();
                SetupNavBar();
            }

        }

        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target Window.
            Window.ProcessActionContainer += Window_ProcessActionContainer;

        }
        protected override void OnDeactivated()
        {
            Window.ProcessActionContainer -= Window_ProcessActionContainer;
            UnsubscribeFromContainerEvents();
            navActionContainer = null;
            base.OnDeactivated();

            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
        private void SubscribeToContainerEvents()
        {
            if (navActionContainer != null)
            {
                navActionContainer.NavigationControlCreated += navigationControlCreated;
            }
        }
        private void UnsubscribeFromContainerEvents()
        {
            if (navActionContainer != null)
            {
                navActionContainer.NavigationControlCreated -= navigationControlCreated;
            }
        }
        private void navigationControlCreated(object sender, EventArgs e)
        {
            SetupNavBar();
        }

        private void SetupNavBar()
        {
            if (navActionContainer != null && navActionContainer.NavigationControl is NavBarNavigationControl)
            {
                NavBarNavigationControl navBarNavigationControl = navActionContainer.NavigationControl as NavBarNavigationControl;
                navBarNavigationControl.SkinExplorerBarViewScrollStyle = SkinExplorerBarViewScrollStyle.ScrollBar;
                navBarNavigationControl.PaintStyleKind = NavBarViewKind.ExplorerBar;

            }
        }
    }
}
