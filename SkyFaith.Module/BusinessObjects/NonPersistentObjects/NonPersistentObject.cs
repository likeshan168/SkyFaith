using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyFaith.Module.BusinessObjects.NonPersistentObjects
{
    [DomainComponent, DefaultClassOptions, VisibleInReports]
    public class NonPersistentObject
    {
        [Browsable(false)]
        [Key]
        public int Oid { get; set; }

        public string Name { get; set; }

    }
}
