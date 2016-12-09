using System;
using System.Linq;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;
using DevExpress.Utils.MVVM.Services;
using DevExpress.Utils.MVVM;

namespace MVVMDemo.Views.db_SFIEntitiesView
{
    public partial class db_SFIEntitiesView : XtraUserControl
    {
        public db_SFIEntitiesView()
        {
            InitializeComponent();
            if (!mvvmContext.IsDesignMode)
                InitializeNavigation();
            ribbonControl.Merge += ribbonControl_Merge;
            ribbonControl.UnMerge += ribbonControl_UnMerge;
        }

        private void ribbonControl_UnMerge(object sender, DevExpress.XtraBars.Ribbon.RibbonMergeEventArgs e)
        {
            ribbonControl.SelectedPage = e.MergeOwner.SelectedPage;
            ribbonControl.StatusBar.UnMergeStatusBar();
        }

        void ribbonControl_Merge(object sender, DevExpress.XtraBars.Ribbon.RibbonMergeEventArgs e)
        {
            ribbonControl.SelectedPage = e.MergedChild.SelectedPage;
            ribbonControl.StatusBar.MergeStatusBar(e.MergedChild.StatusBar);
        }
        void InitializeNavigation()
        {
            DevExpress.XtraEditors.WindowsFormsSettings.SetDPIAware();
            DevExpress.XtraEditors.WindowsFormsSettings.EnableFormSkins();
            DevExpress.XtraEditors.WindowsFormsSettings.AllowPixelScrolling = DevExpress.Utils.DefaultBoolean.True;
            DevExpress.XtraEditors.WindowsFormsSettings.ScrollUIMode = DevExpress.XtraEditors.ScrollUIMode.Touch;
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Office 2013 Light Gray");

            mvvmContext.RegisterService(DocumentManagerService.Create(navigationFrame));
            DevExpress.Utils.MVVM.MVVMContext.RegisterFlyoutDialogService();
            // We want to use buttons in Ribbon to show the specific modules
            var fluentAPI = mvvmContext.OfType<MVVMDemo.ViewModels.db_SFIEntitiesViewModel>();
            fluentAPI.BindCommand(navigationBarItemtb_AgentCollectionView, (x, m) => x.Show(m), x => x.Modules[0]);
            fluentAPI.BindCommand(barButtonItemtb_AgentCollectionView, (x, m) => x.Show(m), x => x.Modules[0]);
            fluentAPI.BindCommand(navigationBarItemtb_SFI_TrackNumCollectionView, (x, m) => x.Show(m), x => x.Modules[1]);
            fluentAPI.BindCommand(barButtonItemtb_SFI_TrackNumCollectionView, (x, m) => x.Show(m), x => x.Modules[1]);
            // We want show the default module when our UserControl is loaded
            fluentAPI.WithEvent<EventArgs>(this, "Load")
                .EventToCommand(x => x.OnLoaded(null), x => x.DefaultModule);
        }
    }
}
