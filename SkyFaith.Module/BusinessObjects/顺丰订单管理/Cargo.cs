using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using Admiral.ImportData;

namespace SkyFaith.Module.BusinessObjects.顺丰订单管理
{
    [DefaultClassOptions]
    [NavigationItem("顺丰订单")]
    //[XafDisplayName("货物")]
    [Custom("Caption","货物")]
    public class Cargo : BaseObject,IImportData
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Cargo(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        // Fields...
        private Order _Order;
        private decimal _Amount;
        private decimal _Weight;
        private string _Unit;
        private int _Count;
        private string _Name;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField]
        [XafDisplayName("货物名称")]
        public string name
        {
            get
            {
                return _Name;
            }
            set
            {
                SetPropertyValue("name", ref _Name, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("货物数量")]

        public int count
        {
            get
            {
                return _Count;
            }
            set
            {
                SetPropertyValue("count", ref _Count, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("货物单位")]

        public string unit
        {
            get
            {
                return _Unit;
            }
            set
            {
                SetPropertyValue("unit", ref _Unit, value);
            }
        }

        [XafDisplayName("订单货物单位重量")]
        //[ModelDefault("","")]
        public decimal weight
        {
            get
            {
                return _Weight;
            }
            set
            {
                SetPropertyValue("weight", ref _Weight, value);
            }
        }

        [XafDisplayName("货物单价")]

        public decimal amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                SetPropertyValue("amount", ref _Amount, value);
            }
        }


        [Association("Order-Cargos")]
        public Order Order
        {
            get
            {
                return _Order;
            }
            set
            {
                SetPropertyValue("Order", ref _Order, value);
            }
        }
    }
}
