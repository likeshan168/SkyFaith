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
using System.IO;

//namespace SkyFaith.Module.BusinessObjects.鞋标签打印
//{
//    [FileAttachment]
    
//    public class 鞋图 : BaseObject,FileData
//    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
//        public 鞋图(Session session)
//            : base(session)
//        {
//        }
//        public override void AfterConstruction()
//        {
//            base.AfterConstruction();
//            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
//        }
        

//    }

//    public class MyFileData : IFileData
//    {
//        public string FileName
//        {
//            get
//            {
//                throw new NotImplementedException();
//            }

//            set
//            {
//                throw new NotImplementedException();
//            }
//        }

//        public int Size
//        {
//            get
//            {
//                throw new NotImplementedException();
//            }
//        }

//        public void Clear()
//        {
//            throw new NotImplementedException();
//        }

//        public void LoadFromStream(string fileName, Stream stream)
//        {
//            throw new NotImplementedException();
//        }

//        public void SaveToStream(Stream stream)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
