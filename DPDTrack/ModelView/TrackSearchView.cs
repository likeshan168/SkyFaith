using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DPDTrack.Models;
using System.Linq.Expressions;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;

namespace DPDTrack.ModelView
{
    public partial class TrackSearchView : XtraUserControl
    {
        public TrackSearchView()
        {

            InitializeComponent();
            splashScreenManager1.ShowWaitForm();
            InitBindings();
            splashScreenManager1.CloseWaitForm();
        }

        private void InitBindings()
        {
            mvvmContext1.ViewModelType = typeof(TrackSearchViewModel);
            var fluentAPI = mvvmContext1.OfType<TrackSearchViewModel>();

            cbxBusinessParter.Properties.Items.AddRange(fluentAPI.ViewModel.CBXAgents.ToList());
            cbxBusinessParter.SelectedIndex = 0;
            fluentAPI.SetBinding(cbxBusinessParter, cbx => cbx.EditValue, x => x.SelectedAgent);
            fluentAPI.SetBinding(dtImportData, dt => dt.EditValue, x => x.ImportDate);
            fluentAPI.SetBinding(txtOrderNum, orderNum => orderNum.EditValue, x => x.OrderNo);
            fluentAPI.SetBinding(txtAgentNum, agentNum => agentNum.EditValue, x => x.AgentNo);
            fluentAPI.SetBinding(trackNumInfoCollectionView, gc => gc.DataSource, x => x.TrackNumInfos);
            fluentAPI.BindCommand(btnSearch, (x, p1) => x.Search(p1), x => new SearchParameter() { Grid = trackNumInfoCollectionView, SplashScreenManager = splashScreenManager1 });

            fluentAPI.BindCommand(btnClear, (x, p1) => x.ClearDataSource(p1), x => new SearchParameter() { Grid = trackNumInfoCollectionView, SplashScreenManager = splashScreenManager1 });
        }


        private void gridView1_CustomDrawEmptyForground(object sender, CustomDrawEventArgs e)
        {
            ColumnView columnView = sender as ColumnView;
            BindingSource bindingSource = trackNumInfoCollectionView.DataSource as BindingSource;
            if (bindingSource?.Count == 0)
            {
                string str = "没有查询到你所需要的数据";
                Font f = new Font("宋体", 10, FontStyle.Bold);
                Rectangle r = new Rectangle(e.Bounds.Top + 200, e.Bounds.Left + 60, e.Bounds.Right - 5, e.Bounds.Height - 5);
                e.Graphics.DrawString(str, f, Brushes.Red, r);
            }
        }
        private void OnDisposing()
        {
            if (mvvmContext1 != null)
            {
                mvvmContext1.Dispose();
            }
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if(e.Info.IsRowIndicator&&e.RowHandle>=0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        private void gridView1_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(e.RowHandle);
            if (row == null) return;
            if(row["运输服务完毕"].ToString()=="N")
            {
                //e.Appearance.BackColor = Color.OrangeRed;
                e.Appearance.ForeColor = Color.OrangeRed;
            }
            else if(row["运输服务完毕"].ToString() == "Y")
            {
                //e.Appearance.BackColor = Color.Green;
                e.Appearance.ForeColor = Color.Green;
            }

            if(e.RowHandle== gridView1.FocusedRowHandle)
            {
                e.Appearance.ForeColor = Color.White;
                e.Appearance.BackColor = Color.RoyalBlue;
            }
        }
    }
}
