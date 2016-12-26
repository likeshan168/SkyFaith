using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DPDModelDPDModel.DAL;

namespace DPDTrack.ModelView
{
    [POCOViewModel]
    public class LoginViewModel
    {
        public virtual string CurrentUser { get; set; }

        public virtual void KeyDown(LoginFrm loginFrm)
        {
            if (string.IsNullOrWhiteSpace(CurrentUser))
            {
                MessageBoxService.ShowMessage($"用户名不能为空", "提示信息", MessageButton.OK, MessageIcon.Error);
                return;
            }

            LogOn(loginFrm);
        }

        private IMessageBoxService MessageBoxService
        {
            get
            {
                return this.GetService<IMessageBoxService>();
            }
        }

        protected ISplashScreenService SplashScreenService
        {
            get { return this.GetService<ISplashScreenService>(); }
        }

        public virtual void Login(LoginFrm loginFrm)
        {
            LogOn(loginFrm);
        }

        private void LogOn(LoginFrm loginFrm)
        {
            try
            {
                string sqlStr = $"SELECT vchar_user FROM tb_SFIuser WHERE vchar_user='{CurrentUser}'";
                string user = SQLHelper.GetStringFromDB<string>(sqlStr);
                if(string.IsNullOrWhiteSpace(user))
                {
                    MessageBoxService.ShowMessage($"用户名错误", "提示信息", MessageButton.OK, MessageIcon.Information);
                }
                else
                {
                    MainFrm.CurrentUser = user;

                    loginFrm.DialogResult = System.Windows.Forms.DialogResult.OK;
                    
                }
            }
            catch (Exception ex)
            {
                MessageBoxService.ShowMessage($"登录失败：{ex.Message}", "提示信息", MessageButton.OK, MessageIcon.Error);

            }
        }
    }
}