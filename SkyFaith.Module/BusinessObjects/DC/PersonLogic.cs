using DevExpress.ExpressApp.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyFaith.Module.BusinessObjects.DC
{
    [DomainLogic(typeof(IPerson))]
    public class PersonLogic
    {
        public static string Get_FullName(IPerson person)
        {
            return $"{person.FirstName} {person.LastName}";
        }

        public void AfterChange_Married(IPerson person)
        {
            if (!person.Married) person.SpouseName = string.Empty;
        }
    }
}
