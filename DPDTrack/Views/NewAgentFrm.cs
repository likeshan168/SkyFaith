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
using DevExpress.Utils.MVVM;
using DPDTrack.ModelView;
using DPDTrack.Models;

namespace DPDTrack.Views
{
    public partial class NewAgentFrm : XtraForm
    {
        public NewAgentFrm()
        {
            InitializeComponent();
            //MVVMContext mvvmContext = new MVVMContext();
            //mvvmContext.ContainerControl = this;
            ////mvvmContext.RegisterService(new NewAgentFrm());

            //mvvmContext.ViewModelType = typeof(NewAgentViewModel);

            //var fluentAPI = mvvmContext.OfType<NewAgentViewModel>();
            //var viewModel = mvvmContext.GetViewModel<NewAgentViewModel>();
            //viewModel.NewAgentFrm = this;
            //fluentAPI.SetBinding(this, f => f.Text, x => x.Agent.vchar_AGname);
            //Close();
            //fluentAPI.WithEvent<KeyEventArgs>(this, ).EventToCommand(x => x.PressEnter(null), x => splashScreenManager1, e => (e.KeyCode == Keys.Enter));
        }

        void OnDisposing()
        {
            var context = MVVMContext.FromControl(this);
            if (context != null) context.Dispose();
        }

    }
}