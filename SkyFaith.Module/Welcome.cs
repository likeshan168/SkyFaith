using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyFaith.Module
{
    [NonPersistent,Custom("Caption","Welcome")]
    [ImageName("")]
    public class Welcome
    {
        private static Welcome instance;
        protected Welcome() : base() { }

        public static Welcome Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Welcome();
                }

                return instance;

            }
        }
    }

    public class WelcomeDisableObjectSpaceActionController : ViewController<DetailView>
    {
        private const string InactiveForWelcome = "Inactive for Welcome";

        public WelcomeDisableObjectSpaceActionController()
        {

        }

        protected override void OnActivated()
        {
            base.OnActivated();
            Frame.GetController<RefreshController>().Active[InactiveForWelcome] = (View.ObjectTypeInfo.Type != typeof(Welcome));
            Frame.GetController<RecordsNavigationController>().Active[InactiveForWelcome] = (View.ObjectTypeInfo.Type != typeof(Welcome));
            Frame.GetController<ModificationsController>().Active[InactiveForWelcome] = (View.ObjectTypeInfo.Type != typeof(Welcome));
        }

        protected override void OnDeactivated()
        {
            Frame.GetController<RefreshController>().Active.RemoveItem(InactiveForWelcome);
            Frame.GetController<RecordsNavigationController>().Active.RemoveItem(InactiveForWelcome);
            Frame.GetController<ModificationsController>().Active.RemoveItem(InactiveForWelcome);
            base.OnDeactivated();

        }
    }
}
