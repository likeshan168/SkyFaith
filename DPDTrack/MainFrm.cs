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
using DPDTrack.Views;

namespace DPDTrack
{
    public partial class MainFrm : DevExpress.XtraEditors.XtraForm
    {
        public MainFrm()
        {
            InitializeComponent();
            var view = new TrackSearchView();
            view.Dock = DockStyle.Fill;
            xtraTabPage1.Controls.Add(view);

            var bpv = new BusinessPartnerView();
            bpv.Dock = DockStyle.Fill;
            xtraTabPage2.Controls.Add(bpv);

            var checkPointServerView = new CheckPointServerMgrView();
            checkPointServerView.Dock = DockStyle.Fill;
            xtraTabPage3.Controls.Add(checkPointServerView);
        }
    }
}