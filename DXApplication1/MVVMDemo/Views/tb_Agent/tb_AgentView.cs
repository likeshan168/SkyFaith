using System;
using System.Linq;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using DevExpress.Utils.MVVM;
using DevExpress.Utils.MVVM.Services;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;

namespace MVVMDemo.Views.tb_AgentView
{
    public partial class tb_AgentView : XtraUserControl
    {
        public tb_AgentView()
        {
            InitializeComponent();
            if (!mvvmContext.IsDesignMode)
                InitBindings();
        }
        void InitBindings()
        {
            var fluentAPI = mvvmContext.OfType<MVVMDemo.ViewModels.tb_AgentViewModel>();
            fluentAPI.WithEvent(this, "Load").EventToCommand(x => x.OnLoaded());
            fluentAPI.SetObjectDataSourceBinding(
                tb_AgentViewBindingSource, x => x.Entity, x => x.Update());

            bbiCustomize.ItemClick += (s, e) => { dataLayoutControl1.ShowCustomizationForm(); };
        }
    }
}
