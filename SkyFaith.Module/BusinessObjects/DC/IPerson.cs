using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyFaith.Module.BusinessObjects.DC
{
    [DomainComponent]
    [NavigationItem("测试"),ImageName("BO_Person")]
    public interface IPerson
    {
        [RuleRequiredField]
        string LastName { get; set; }
        string FirstName { get; set; }
        [NonPersistentDc]
        string FullName { get; }
        DateTime Birthday { get; set; }
        bool Married { get; set; }

        string SpouseName { get; set; }

    }
}
