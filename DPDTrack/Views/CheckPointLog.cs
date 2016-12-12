using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DPDTrack.ModelView;
using DPDModel.Models;
using DPDTrack.Models;
using DevExpress.XtraGrid.Views.Base;

namespace DPDTrack.Views
{
    public partial class CheckPointLog : XtraForm
    {
        public CheckPointLog(SFITrackNum track, CBXAgent agent)
        {
            InitializeComponent();
            if (!DesignMode)
            {
                splashScreenManager1.ShowWaitForm();
                InitBindings(track, agent);
                splashScreenManager1.CloseWaitForm();
            }
        }

        private void InitBindings(SFITrackNum track, CBXAgent agent)
        {
            mvvmContext1.ViewModelType = typeof(CheckPointLogViewModel);
            var fluentAPI = mvvmContext1.OfType<CheckPointLogViewModel>();
            fluentAPI.ViewModel.SFITrackNum = track;
            fluentAPI.ViewModel.CBXAgent = agent;

            fluentAPI.SetBinding(lblMapId, lbl => lbl.Text, x => x.SFITrackNum.int_Mappingid, x => { return $"数据编号：{x}"; });
            fluentAPI.SetBinding(lblOrderNo, lbl => lbl.Text, x => x.SFITrackNum.vchar_SFInum, x => { return $"SFI单号：{x}"; });
            fluentAPI.SetBinding(lblAgentNo, lbl => lbl.Text, x => x.SFITrackNum.vchar_AGnum, x => { return $"供应商单号：{x}"; });
            fluentAPI.SetBinding(lblAgentName, lbl => lbl.Text, x => x.CBXAgent.Agname, x => { return $"供应商：{x}"; });
            fluentAPI.SetBinding(txtRecordStr, x => x.EditValue, x => x.RecordStr);

            fluentAPI.SetBinding(gridControl1, gv => gv.DataSource, x => x.TrackRecords);
            fluentAPI.SetBinding(gridControl2, gv => gv.DataSource, x => x.TrackRecordLogs);

            fluentAPI.WithEvent<ColumnView, FocusedRowObjectChangedEventArgs>(gridView1, "FocusedRowObjectChanged")
                .SetBinding(x => x.SelectedTrackRecord,
                            args => args.Row as TrackRecord,
                            (gView, entity) => gView.FocusedRowHandle = gView.FindRow(entity));

            fluentAPI.BindCommand(btnDelete, x => x.DeleteTrackRecord());
            fluentAPI.BindCommand(btnSubmit, x => x.AddTrackRecord());
            fluentAPI.BindCommand(btnRefresh, x => x.RefreshLogs(null), x => splashScreenManager1);
        }

        private void gridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
    }
}