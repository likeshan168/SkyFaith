using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyFaith.Module.BusinessObjects.DC
{
    [DomainComponent, NavigationItem("测试"), ImageName("BO_Contact")]
    public interface ICustomer : IOrganization, IAccount
    {

    }
}
