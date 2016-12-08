using DevExpress.ExpressApp.DC;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyFaith.Module.BusinessObjects.NonPersistentObjects
{
    [DomainComponent]
    public class Duplicate
    {
        [Browsable(false),Key]
        public int Id;
        public string Name { get; set; }
        public int Count { get; set; }
    }

    [DomainComponent]
    public class DuplicateList
    {
        private BindingList<Duplicate> duplicates;
        public DuplicateList()
        {
            duplicates = new BindingList<Duplicate>();
        }

        public BindingList<Duplicate> Duplicates { get { return duplicates; } }
    }
}
