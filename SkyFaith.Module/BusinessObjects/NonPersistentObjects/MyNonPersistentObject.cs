using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyFaith.Module.BusinessObjects.NonPersistentObjects
{
    [DomainComponent, VisibleInReports,DefaultClassOptions,NavigationItem("测试")]
    public class MyNonPersistentObject
    {
        public string Name { get; set; }

        public Person Owner { get; set; }
    }
}
