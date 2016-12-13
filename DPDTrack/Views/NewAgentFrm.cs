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
using DevExpress.Utils.MVVM.UI;

namespace DPDTrack.Views
{
    [ViewType("AddAgent")]
    public partial class NewAgentFrm : XtraForm
    {
        public NewAgentFrm()
        {
            InitializeComponent();
        }
    }
}