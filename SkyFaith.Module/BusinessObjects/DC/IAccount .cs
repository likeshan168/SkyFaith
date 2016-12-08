using DevExpress.ExpressApp.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyFaith.Module.BusinessObjects.DC
{
    [DomainComponent]
    public interface IAccount
    {
        [FieldSize(8)]
        string Login { get; set; }
        [FieldSize(8)]
        string Password { get; set; }
    }
}
