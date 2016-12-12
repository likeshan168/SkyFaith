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
        }
    }
}