using DevExpress.ExpressApp.Data;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using System.ComponentModel;

namespace SkyFaith.Module.BusinessObjects.DPD
{
    [DomainComponent, DefaultClassOptions, NavigationItem("运单管理"), XafDisplayName("商业伙伴管理"),DefaultProperty("vchar_AGname")]
    public class Agent
    {
        private int _int_agid;
        private string _vchar_agcode;
        private string _vchar_agname;
        private string _vchar_aglinkman;
        private string _vchar_agcontect;
        private int _int_agtype;
        private bool _bit_synopen = false;
        private string _vchar_synverify;
        private string _vchar_synstopkeyword;
        private int? _int_synspacing = 30;
        private bool _bit_synquery = false;
        private string _vchar_queryverify;
        private bool _bit_synpush = false;
        private string _vchar_pushuser;
        private string _vchar_pushverify;
        private bool _isopen = true;
        [XafDisplayName("数据ID")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Key]
        public int int_AGid
        {
            set { _int_agid = value; }
            get { return _int_agid; }
        }
        [XafDisplayName("商业伙伴代码")]
        public string vchar_AGcode
        {
            set { _vchar_agcode = value; }
            get { return _vchar_agcode; }
        }
        [XafDisplayName("商业伙伴名称")]
        public string vchar_AGname
        {
            set { _vchar_agname = value; }
            get { return _vchar_agname; }
        }
        [XafDisplayName("联系人")]
        public string vchar_AGLinkMan
        {
            set { _vchar_aglinkman = value; }
            get { return _vchar_aglinkman; }
        }
        [XafDisplayName("联系方式")]
        public string vchar_AGcontect
        {
            set { _vchar_agcontect = value; }
            get { return _vchar_agcontect; }
        }
        [XafDisplayName("商业伙伴类型")]
        public int int_AGtype
        {
            set { _int_agtype = value; }
            get { return _int_agtype; }
        }
        [XafDisplayName("检查点同步开启")]
        public bool bit_synOpen
        {
            set { _bit_synopen = value; }
            get { return _bit_synopen; }
        }
        [XafDisplayName("检查点同步验证码")]
        public string vchar_synVerify
        {
            set { _vchar_synverify = value; }
            get { return _vchar_synverify; }
        }
        [XafDisplayName("检查点同步终止关键词")]
        public string vchar_synStopKeyWord
        {
            set { _vchar_synstopkeyword = value; }
            get { return _vchar_synstopkeyword; }
        }
        [XafDisplayName("检查点同步时间间隔")]
        public int? int_synSpacing
        {
            set { _int_synspacing = value; }
            get { return _int_synspacing; }
        }
        [XafDisplayName("检查点查询服务开启")]
        public bool bit_synQuery
        {
            set { _bit_synquery = value; }
            get { return _bit_synquery; }
        }
        [XafDisplayName("检查点查询服务校验码")]
        public string vchar_QueryVerify
        {
            set { _vchar_queryverify = value; }
            get { return _vchar_queryverify; }
        }
        [XafDisplayName("检查点推送服务开启")]
        public bool bit_synPush
        {
            set { _bit_synpush = value; }
            get { return _bit_synpush; }
        }
        [XafDisplayName("检查点推送服务用户名")]
        public string vchar_PushUser
        {
            set { _vchar_pushuser = value; }
            get { return _vchar_pushuser; }
        }
        [XafDisplayName("检查点推送服务校验码")]
        public string vchar_PushVerify
        {
            set { _vchar_pushverify = value; }
            get { return _vchar_pushverify; }
        }
        /// <summary>
        /// 逻辑删除标志
        /// </summary>
        public bool IsOpen
        {
            set { _isopen = value; }
            get { return _isopen; }
        }
    }
}
