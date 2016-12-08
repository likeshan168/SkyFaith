using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;

namespace Admiral.ImportData
{
    [XafDisplayName("数据导入")]
    [NonPersistent]
    [ImageName("ImportData")]
    public class ImportData : BaseObject
    {
        public ImportData(Session s) : base(s)
        {

        }

        public IImportOption Option { get; set; }

        private decimal _Progress;
        [XafDisplayName("进度")][ModelDefault("AllowEdit","False")]
        public decimal Progress
        {
            get { return _Progress; }
            set { SetPropertyValue("Progress", ref _Progress, value); }
        }

    }
}