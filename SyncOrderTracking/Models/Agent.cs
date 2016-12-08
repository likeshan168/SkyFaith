using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncOrderTracking.Models
{
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
        /// <summary>
        /// 
        /// </summary>
        public int int_AGid
        {
            set { _int_agid = value; }
            get { return _int_agid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string vchar_AGcode
        {
            set { _vchar_agcode = value; }
            get { return _vchar_agcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string vchar_AGname
        {
            set { _vchar_agname = value; }
            get { return _vchar_agname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string vchar_AGLinkMan
        {
            set { _vchar_aglinkman = value; }
            get { return _vchar_aglinkman; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string vchar_AGcontect
        {
            set { _vchar_agcontect = value; }
            get { return _vchar_agcontect; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int int_AGtype
        {
            set { _int_agtype = value; }
            get { return _int_agtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool bit_synOpen
        {
            set { _bit_synopen = value; }
            get { return _bit_synopen; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string vchar_synVerify
        {
            set { _vchar_synverify = value; }
            get { return _vchar_synverify; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string vchar_synStopKeyWord
        {
            set { _vchar_synstopkeyword = value; }
            get { return _vchar_synstopkeyword; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? int_synSpacing
        {
            set { _int_synspacing = value; }
            get { return _int_synspacing; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool bit_synQuery
        {
            set { _bit_synquery = value; }
            get { return _bit_synquery; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string vchar_QueryVerify
        {
            set { _vchar_queryverify = value; }
            get { return _vchar_queryverify; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool bit_synPush
        {
            set { _bit_synpush = value; }
            get { return _bit_synpush; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string vchar_PushUser
        {
            set { _vchar_pushuser = value; }
            get { return _vchar_pushuser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string vchar_PushVerify
        {
            set { _vchar_pushverify = value; }
            get { return _vchar_pushverify; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsOpen
        {
            set { _isopen = value; }
            get { return _isopen; }
        }
    }
}
