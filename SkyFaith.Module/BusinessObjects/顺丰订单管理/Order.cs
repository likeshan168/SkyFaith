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
using System.Xml;
using Admiral.ImportData;

namespace SkyFaith.Module.BusinessObjects.顺丰订单管理
{
    [DefaultClassOptions]
    [NavigationItem("顺丰订单")]
    //[XafDisplayName("订单")]
    [Custom("Caption","订单")]
    [XafDefaultProperty("orderid")]
    
    public class Order : BaseObject,IImportData
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public Order(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
            pay_method = PayMethod.寄方付;
        }

        // Fields...
        private PayMethod _Pay_method;
        private string _D_post_code;
        private string _D_address;
        private string _D_county;
        private string _D_city;
        private string _D_province;
        private string _D_country;
        private string _D_deliverycode;
        private string _D_tel;
        private string _D_contact;
        private string _D_company;
        private string _J_post_code;
        private string _J_address;
        private string _J_county;
        private string _J_city;
        private string _J_province;
        private string _J_country;
        private string _J_shippercode;
        private string _J_tel;
        private string _J_contact;
        private string _J_company;
        private string _Mailno;
        private string _Orderid;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("订单号")]
        [RuleRequiredField]
        public string orderid
        {
            get
            {
                return _Orderid;
            }
            set
            {
                SetPropertyValue("orderid", ref _Orderid, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("顺丰运单号")]

        public string mailno
        {
            get
            {
                return _Mailno;
            }
            set
            {
                SetPropertyValue("mailno", ref _Mailno, value);
            }
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("寄件方公司名称")]

        public string j_company
        {
            get
            {
                return _J_company;
            }
            set
            {
                SetPropertyValue("j_company", ref _J_company, value);
            }
        }


        [XafDisplayName("寄件方联系人")]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string j_contact
        {
            get
            {
                return _J_contact;
            }
            set
            {
                SetPropertyValue("j_contact", ref _J_contact, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("寄件方联系电话")]
        [RuleRequiredField]

        public string j_tel
        {
            get
            {
                return _J_tel;
            }
            set
            {
                SetPropertyValue("j_tel", ref _J_tel, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("寄件方国家/城市代码")]

        public string j_shippercode
        {
            get
            {
                return _J_shippercode;
            }
            set
            {
                SetPropertyValue("j_shippercode", ref _J_shippercode, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("寄方国家")]

        public string j_country
        {
            get
            {
                return _J_country;
            }
            set
            {
                SetPropertyValue("j_country", ref _J_country, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("寄件方所在省份")]

        public string j_province
        {
            get
            {
                return _J_province;
            }
            set
            {
                SetPropertyValue("j_province", ref _J_province, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("寄件方所在城市名称")]

        public string j_city
        {
            get
            {
                return _J_city;
            }
            set
            {
                SetPropertyValue("j_city", ref _J_city, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("寄件人所在县/区")]

        public string j_county
        {
            get
            {
                return _J_county;
            }
            set
            {
                SetPropertyValue("j_county", ref _J_county, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("寄件方详细地址")]
        [RuleRequiredField]
        public string j_address
        {
            get
            {
                return _J_address;
            }
            set
            {
                SetPropertyValue("j_address", ref _J_address, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("寄方邮编")]

        public string j_post_code
        {
            get
            {
                return _J_post_code;
            }
            set
            {
                SetPropertyValue("j_post_code", ref _J_post_code, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("到件方公司名称")]
        [RuleRequiredField]

        public string d_company
        {
            get
            {
                return _D_company;
            }
            set
            {
                SetPropertyValue("d_company", ref _D_company, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("到件方联系人")]
        [RuleRequiredField]

        public string d_contact
        {
            get
            {
                return _D_contact;
            }
            set
            {
                SetPropertyValue("d_contact", ref _D_contact, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("到件方联系电话")]
        [RuleRequiredField]

        public string d_tel
        {
            get
            {
                return _D_tel;
            }
            set
            {
                SetPropertyValue("d_tel", ref _D_tel, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("到件方代码")]

        public string d_deliverycode
        {
            get
            {
                return _D_deliverycode;
            }
            set
            {
                SetPropertyValue("d_deliverycode", ref _D_deliverycode, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("到方国家")]

        public string d_country
        {
            get
            {
                return _D_country;
            }
            set
            {
                SetPropertyValue("d_country", ref _D_country, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("到件方所在省份")]

        public string d_province
        {
            get
            {
                return _D_province;
            }
            set
            {
                SetPropertyValue("d_province", ref _D_province, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("到件方所在城市名称")]

        public string d_city
        {
            get
            {
                return _D_city;
            }
            set
            {
                SetPropertyValue("d_city", ref _D_city, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("到件方所在县/区")]

        public string d_county
        {
            get
            {
                return _D_county;
            }
            set
            {
                SetPropertyValue("d_county", ref _D_county, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("到件方详细地址")]
        [RuleRequiredField]

        public string d_address
        {
            get
            {
                return _D_address;
            }
            set
            {
                SetPropertyValue("d_address", ref _D_address, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [XafDisplayName("到方邮编")]

        public string d_post_code
        {
            get
            {
                return _D_post_code;
            }
            set
            {
                SetPropertyValue("d_post_code", ref _D_post_code, value);
            }
        }

        [XafDisplayName("付款方式")]

        public PayMethod pay_method
        {
            get
            {
                return _Pay_method;
            }
            set
            {
                SetPropertyValue("pay_method", ref _Pay_method, value);
            }
        }

        [Association("Order-Cargos"), DevExpress.Xpo.Aggregated]
        [XafDisplayName("订单货物")]
        public XPCollection<Cargo> Cargos
        {
            get
            {
                return GetCollection<Cargo>("Cargos");
            }
        }

        //[Action(PredefinedCategory.Edit, Caption = "导出XML")]
        public string ExportToXml()
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(dec);
            XmlElement root = doc.CreateElement("Request");
            root.SetAttribute("service", "OrderService");
            root.SetAttribute("lang", "zh-CN");
            doc.AppendChild(root);

            XmlNode head = doc.CreateElement("Head");
            head.InnerText = "BSPdevelop";

            XmlNode body = doc.CreateElement("Body");

            XmlElement order = doc.CreateElement("Order");

            #region 创建订单信息
            order.SetAttribute("orderid", orderid);
            if (!string.IsNullOrWhiteSpace(j_company))
            {
                order.SetAttribute("j_company", j_company);
            }
            if (!string.IsNullOrWhiteSpace(j_contact))
            {
                order.SetAttribute("j_contact", j_contact);
            }
            if (!string.IsNullOrWhiteSpace(j_tel))
            {
                order.SetAttribute("j_tel", j_tel);
            }
            if (!string.IsNullOrWhiteSpace(j_shippercode))
            {
                order.SetAttribute("j_shippercode", j_shippercode);
            }
            if (!string.IsNullOrWhiteSpace(j_county))
            {
                order.SetAttribute("j_county", j_county);
            }
            if (!string.IsNullOrWhiteSpace(j_province))
            {
                order.SetAttribute("j_province", j_province);
            }
            if (!string.IsNullOrWhiteSpace(j_city))
            {
                order.SetAttribute("j_city", j_city);
            }
            if (!string.IsNullOrWhiteSpace(j_county))
            {
                order.SetAttribute("j_county", j_county);
            }
            if (!string.IsNullOrWhiteSpace(j_address))
            {
                order.SetAttribute("j_address", j_address);
            }
            if (!string.IsNullOrWhiteSpace(j_post_code))
            {
                order.SetAttribute("j_post_code", j_post_code);
            }
            if (!string.IsNullOrWhiteSpace(d_contact))
            {
                order.SetAttribute("d_contact", d_contact);
            }
            if (!string.IsNullOrWhiteSpace(d_tel))
            {
                order.SetAttribute("d_tel", d_tel);
            }
            if (!string.IsNullOrWhiteSpace(d_deliverycode))
            {
                order.SetAttribute("d_deliverycode", d_deliverycode);
            }
            if (!string.IsNullOrWhiteSpace(d_country))
            {
                order.SetAttribute("d_country", d_country);
            }
            if (!string.IsNullOrWhiteSpace(d_province))
            {
                order.SetAttribute("d_province", d_province);
            }
            if (!string.IsNullOrWhiteSpace(d_city))
            {
                order.SetAttribute("d_city", d_city);
            }
            if (!string.IsNullOrWhiteSpace(d_county))
            {
                order.SetAttribute("d_county", d_county);
            }
            if (!string.IsNullOrWhiteSpace(d_address))
            {
                order.SetAttribute("d_address", d_address);
            }

            if (!string.IsNullOrWhiteSpace(d_post_code))
            {
                order.SetAttribute("d_post_code", d_post_code);
            }
            if (pay_method != 0)
            {
                order.SetAttribute("pay_method", ((int)pay_method).ToString());
            }

            foreach (Cargo cargo in Cargos)
            {
                XmlElement c = doc.CreateElement("Cargo");
                if (!string.IsNullOrWhiteSpace(cargo.name))
                {
                    c.SetAttribute("name", cargo.name);
                }
                if (!string.IsNullOrWhiteSpace(cargo.unit))
                {
                    c.SetAttribute("unit", cargo.unit);
                }
                if (cargo.count != 0)
                {
                    c.SetAttribute("count", cargo.count.ToString());
                }
                if (cargo.weight != 0)
                {
                    c.SetAttribute("weight", string.Format("{0:C3}", cargo.weight));
                }
                if (cargo.amount != 0)
                {
                    c.SetAttribute("amount", string.Format("{0:C3}", cargo.amount));
                }
                order.AppendChild(c);
            }
            #endregion

            body.AppendChild(order);

            root.AppendChild(head);
            root.AppendChild(body);
            return doc.InnerXml;
        }

        public string SearchOrder()
        {
            #region 确认订单
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(dec);
            XmlElement root = doc.CreateElement("Request");
            root.SetAttribute("service", "OrderSearchService");
            root.SetAttribute("lang", "zh-CN");
            doc.AppendChild(root);

            XmlNode head = doc.CreateElement("Head");
            head.InnerText = "BSPdevelop";
            XmlNode body = doc.CreateElement("Body");

            XmlElement orderSearch = doc.CreateElement("Order");
            orderSearch.SetAttribute("orderid", orderid);
            body.AppendChild(orderSearch);

            root.AppendChild(head);
            root.AppendChild(body);
            return doc.InnerXml;
            #endregion
        }

        public string confirmOrder(string dealType)
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(dec);
            XmlElement root = doc.CreateElement("Request");
            root.SetAttribute("service", "OrderConfirmService");
            root.SetAttribute("lang", "zh-CN");
            doc.AppendChild(root);
            XmlNode head = doc.CreateElement("Head");
            head.InnerText = "BSPdevelop";
            XmlNode body = doc.CreateElement("Body");

            XmlElement orderConfirm = doc.CreateElement("OrderConfirm");
            orderConfirm.SetAttribute("orderid", orderid);
            if (!string.IsNullOrWhiteSpace(mailno))
            {
                orderConfirm.SetAttribute("mailno", mailno);
            }
            orderConfirm.SetAttribute("dealtype", dealType);

            body.AppendChild(orderConfirm);


            root.AppendChild(head);
            root.AppendChild(body);
            return doc.InnerXml;
        }

        public string GetResponseResult(string responseXML,string responseName)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(responseXML);

            string result = string.Empty;

            XmlNode head = doc.SelectSingleNode("Response/Head");
            if (head != null)
            {
                if (head.InnerText == "ERR")
                {
                    XmlNode error = doc.SelectSingleNode("Response/ERROR");
                    result = error.InnerText;
                }
                else if (head.InnerText == "OK")
                {
                    XmlNode body = doc.SelectSingleNode("Response/Body");
                    XmlElement response=(XmlElement) body.SelectSingleNode("responseName");
                    result = response?.GetAttribute("mailno");
                }
            }

            return result;
        }
    }

    public enum PayMethod
    {
        寄方付 = 1,
        收方付 = 2,
        第三方付 = 3
    }
}
