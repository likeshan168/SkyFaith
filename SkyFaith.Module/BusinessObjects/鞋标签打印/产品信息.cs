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
using System.IO;

namespace SkyFaith.Module.BusinessObjects.鞋标签打印
{
    //[UpdateImport("Code")]
    [DefaultClassOptions]
    [DefaultProperty("中文名称")]
    [NavigationItem("鞋标签打印")]
    public class 产品信息 : BaseObject, IImportData
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public 产品信息(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        // Fields...
        private string _旧京东码;
        private string _京东码;
        private string _条形码;
        private string _Inches;
        private string _CM;
        private string _USA;
        private string _EU;
        private string _AUS;
        private XPDelayedProperty _图片 = new XPDelayedProperty();
        private string _中文名称;
        private string _英文名称;
        private string _英文颜色;
        private string _鞋图名称;
        private string _颜色;
        private string _货号;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string 货号
        {
            get
            {
                return _货号;
            }
            set
            {
                SetPropertyValue("货号", ref _货号, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string 颜色
        {
            get
            {
                return _颜色;
            }
            set
            {
                SetPropertyValue("颜色", ref _颜色, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string 鞋图名称
        {
            get
            {
                return _鞋图名称;
            }
            set
            {
                SetPropertyValue("鞋图名称", ref _鞋图名称, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string 英文颜色
        {
            get
            {
                return _英文颜色;
            }
            set
            {
                SetPropertyValue("英文颜色", ref _英文颜色, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string 英文名称
        {
            get
            {
                return _英文名称;
            }
            set
            {
                SetPropertyValue("英文名称", ref _英文名称, value);
            }
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]

        public string 中文名称
        {
            get
            {
                return _中文名称;
            }
            set
            {
                SetPropertyValue("中文名称", ref _中文名称, value);
            }
        }
        //private XPDelayedProperty bigPicture = new XPDelayedProperty();
        [Size(SizeAttribute.Unlimited), VisibleInListView(true)]
        [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit,
             DetailViewImageEditorMode = ImageEditorMode.PictureEdit,
            ListViewImageEditorCustomHeight = 40, DetailViewImageEditorFixedHeight = 40)]
        [Delayed("_图片", true)]
        public byte[] 图片
        {
            get
            {
                if (_图片 == null)
                {
                    _图片.Value = GetImageBytes(鞋图名称);
                }
                return (byte[])_图片.Value;
            }
            set
            {
                //SetPropertyValue("图片", ref _图片, value);
                _图片.Value = value;
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string AUS
        {
            get
            {
                return _AUS;
            }
            set
            {
                SetPropertyValue("AUS", ref _AUS, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string EU
        {
            get
            {
                return _EU;
            }
            set
            {
                SetPropertyValue("EU", ref _EU, value);
            }
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string USA
        {
            get
            {
                return _USA;
            }
            set
            {
                SetPropertyValue("USA", ref _USA, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string CM
        {
            get
            {
                return _CM;
            }
            set
            {
                SetPropertyValue("CM", ref _CM, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Inches
        {
            get
            {
                return _Inches;
            }
            set
            {
                SetPropertyValue("Inches", ref _Inches, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        [RuleRequiredField]
        public string 条形码
        {
            get
            {
                return _条形码;
            }
            set
            {
                SetPropertyValue("条形码", ref _条形码, value);
            }
        }


        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string 京东码
        {
            get
            {
                return _京东码;
            }
            set
            {
                SetPropertyValue("京东码", ref _京东码, value);
            }
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string 旧京东码
        {
            get
            {
                return _旧京东码;
            }
            set
            {
                SetPropertyValue("旧京东码", ref _旧京东码, value);
            }
        }


        //[Association("产品信息-图片")]
        //public XPCollection<鞋图> 图片
        //{
        //    get
        //    {
        //        return GetCollection<鞋图>("图片");
        //    }
        //}

        protected override void OnSaving()
        {
            //这里可以做自己的验证
            /*
             * 1. 如果是新增的操作那么就验证数据库是否存在该项
             *    a.存在，则更新
             *    b.不存在则插入
             */
            base.OnSaving();
            //看是否为新增
            if (Session.IsNewObject(this))
            {
                产品信息 obj = null;
                if (!string.IsNullOrWhiteSpace(京东码))
                {
                    obj = Session.FindObject<产品信息>(CriteriaOperator.Parse("[条形码]=? and [京东码]=?", 条形码, 京东码));
                }
                else if (!string.IsNullOrWhiteSpace(旧京东码))
                {
                    obj = Session.FindObject<产品信息>(CriteriaOperator.Parse("[条形码]=? and 旧京东码=?", 条形码, 旧京东码));
                }
                else if (!string.IsNullOrWhiteSpace(条形码))
                {
                    obj = Session.FindObject<产品信息>(CriteriaOperator.Parse("[条形码]=?", 条形码));
                }

                if (obj != null)
                {
                    obj.Delete();
                }
            }
        }

        private byte[] GetImageBytes(string imageName)
        {
            string path = @"C:\Images\Product\" + imageName + ".jpg";
            if (File.Exists(path))
            {
                using (FileStream fs = File.OpenRead(path))
                {//OpenRead
                    int filelength = 0;
                    filelength = (int)fs.Length; //获得文件长度 
                    byte[] image = new byte[filelength]; //建立一个字节数组 
                    fs.Read(image, 0, filelength); //按字节流读取 
                    return image;
                }
            }

            return null;
        }
    }
}
