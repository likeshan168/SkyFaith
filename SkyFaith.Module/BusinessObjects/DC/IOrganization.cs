using DevExpress.ExpressApp.DC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyFaith.Module.BusinessObjects.DC
{
    [DomainComponent]
    public interface IOrganization
    {
        string Name { get; set; }
        IList<IPerson> Staff { get; }
    }
}
