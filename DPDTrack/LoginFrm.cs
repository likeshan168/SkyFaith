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
using DevExpress.Utils.MVVM;
using DPDTrack.ModelView;
using DevExpress.Utils.MVVM.Services;

namespace DPDTrack
{
    public partial class LoginFrm : XtraForm
    {
        public LoginFrm()
        {
            InitializeComponent();
            InitBindings();
        }

        private void InitBindings()
        {
            MVVMContext.RegisterMessageBoxService();

            mvvmContext1.ViewModelType = typeof(LoginViewModel);
            var fluentAPI = mvvmContext1.OfType<LoginViewModel>();
            mvvmContext1.RegisterService(SplashScreenService.Create(splashScreenManager1));
            fluentAPI.SetBinding(txtUserName, t => t.EditValue, x => x.CurrentUser);

            var loginFrm = this;
            fluentAPI.WithEvent<KeyEventArgs>(txtUserName, "KeyDown").EventToCommand(x => x.KeyDown(null), x => loginFrm, e => (e.KeyCode == Keys.Enter));
            fluentAPI.WithEvent<EventArgs>(btnLogin, "Click").EventToCommand(x => x.Login(null), x => loginFrm);
        }

        void OnDisposing()
        {
            var context = MVVMContext.FromControl(this);
            if (context != null) context.Dispose();
        }

    }
}