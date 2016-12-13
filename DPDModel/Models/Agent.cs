using System.ComponentModel;

namespace DPDModel.Models
{
    public class Agent
    {
        private int _int_agid;
        private string _vchar_agcode;
        private string _vchar_agname;
        private string _vchar_aglinkman;
        private string _vchar_agcontect;
        private string _int_agtype;
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
        [DisplayName("数据ID")]
        public virtual int int_AGid
        {
            set { _int_agid = value; }
            get { return _int_agid; }
        }
        [DisplayName("代码")]

        public virtual string vchar_AGcode
        {
            set { _vchar_agcode = value; }
            get { return _vchar_agcode; }
        }
        [DisplayName("名称")]

        public virtual string vchar_AGname
        {
            set { _vchar_agname = value; }
            get { return _vchar_agname; }
        }
        [DisplayName("联系人")]

        public virtual string vchar_AGLinkMan
        {
            set { _vchar_aglinkman = value; }
            get { return _vchar_aglinkman; }
        }
        [DisplayName("联系方式")]

        public virtual string vchar_AGcontect
        {
            set { _vchar_agcontect = value; }
            get { return _vchar_agcontect; }
        }
        [DisplayName("类型")]

        public virtual string int_AGtype
        {
            set { _int_agtype = value; }
            get { return _int_agtype; }
        }
        [DisplayName("检查点同步开启")]

        public virtual bool bit_synOpen
        {
            set { _bit_synopen = value; }
            get { return _bit_synopen; }
        }
        [DisplayName("检查点同步验证码")]

        public virtual string vchar_synVerify
        {
            set { _vchar_synverify = value; }
            get { return _vchar_synverify; }
        }
        [DisplayName("检查点同步终止关键字")]

        public virtual string vchar_synStopKeyWord
        {
            set { _vchar_synstopkeyword = value; }
            get { return _vchar_synstopkeyword; }
        }
        [DisplayName("检查点同步时间间隔")]

        public virtual int? int_synSpacing
        {
            set { _int_synspacing = value; }
            get { return _int_synspacing; }
        }
        [DisplayName("检查点查询服务开启")]

        public virtual bool bit_synQuery
        {
            set { _bit_synquery = value; }
            get { return _bit_synquery; }
        }
        [DisplayName("检查点查询服务校验码")]

        public virtual string vchar_QueryVerify
        {
            set { _vchar_queryverify = value; }
            get { return _vchar_queryverify; }
        }
        [DisplayName("检查点推送服务开启")]

        public virtual bool bit_synPush
        {
            set { _bit_synpush = value; }
            get { return _bit_synpush; }
        }
        [DisplayName("检查点推送服务用户名")]

        public virtual string vchar_PushUser
        {
            set { _vchar_pushuser = value; }
            get { return _vchar_pushuser; }
        }
        [DisplayName("检查点推送服务校验码")]

        public virtual string vchar_PushVerify
        {
            set { _vchar_pushverify = value; }
            get { return _vchar_pushverify; }
        }
        [Browsable(false)]
        public virtual bool IsOpen
        {
            set { _isopen = value; }
            get { return _isopen; }
        }
    }
}
