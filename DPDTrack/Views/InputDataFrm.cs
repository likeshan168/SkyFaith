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
using DPDTrack.Models;

namespace DPDTrack.ModelView
{
    public partial class InputDataFrm : XtraForm
    {
        private int agentId;
        public InputDataFrm(DataTable dataSource, int agentId)
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.agentId = agentId;
                InitBinding(dataSource);
            }
        }

        private void InitBinding(DataTable dt)
        {
            mvvmContext1.ViewModelType = typeof(InputDataFrmViewModel);
            var fluentAPI = mvvmContext1.OfType<InputDataFrmViewModel>();
            fluentAPI.ViewModel.DataSource = dt;
            fluentAPI.SetBinding(gridControl1, gv => gv.DataSource, x => x.DataSource);


            //fluentAPI.SetBinding(this, frm => frm., x => x.isClose);
            var frm = this;
            fluentAPI.WithEvent<EventArgs>(btnCancel, "Click").EventToCommand(x => x.CloseForm());
            var para = new CustomParameter() { AgentID = agentId, SplashScreenManager = splashScreenManager1 };
            fluentAPI.WithEvent(btnOk, "Click").EventToCommand(x => x.AddTraks(null), x => para);
            fluentAPI.SetBinding(frm, f => f.DialogResult, x => x.DResult);

        }

    }
}