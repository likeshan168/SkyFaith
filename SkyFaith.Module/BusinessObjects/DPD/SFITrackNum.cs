using DevExpress.ExpressApp.Data;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.ComponentModel;

namespace SkyFaith.Module.BusinessObjects.DPD
{
    [DomainComponent, DefaultClassOptions,NavigationItem("运单管理"),XafDisplayName("运单管理")]
    public class SFITrackNum
    {
        private int _int_mappingid;
        private string _vchar_sfinum;
        private string _vchar_agnum;
        //private int _int_agid;
        //private string _vchar_updateuser;
        private DateTime? _dttm_updatedttm;
        private DateTime? _dttm_ag_lastsyn;
        private int _int_ag_syn = 0;
        private string _char_isstop = "N";
        private string _char_ag_syn_sign;
        private Agent _agent;
        /// <summary>
        /// 数据编号
        /// </summary>
        [XafDisplayName("数据编号")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Key]
        public int int_Mappingid
        {
            set { _int_mappingid = value; }
            get { return _int_mappingid; }
        }
        /// <summary>
        /// SFI单号
        /// </summary>
        [XafDisplayName("订单号")]
        [RuleRequiredField]
        public string vchar_SFInum
        {
            set { _vchar_sfinum = value; }
            get { return _vchar_sfinum; }
        }
        /// <summary>
        /// 代理单号
        /// </summary>
        [XafDisplayName("代理单号")]
        [RuleRequiredField]
        public string vchar_AGnum
        {
            set { _vchar_agnum = value; }
            get { return _vchar_agnum; }
        }
        ///// <summary>
        ///// 代理Id
        ///// </summary>
        //[Browsable(false)]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //public int int_AGid
        //{
        //    set { _int_agid = value; }
        //    get { return _int_agid; }
        //}
        ///// <summary>
        ///// 操作用户
        ///// </summary>
        //[Browsable(false)]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //public string vchar_updateUser
        //{
        //    set { _vchar_updateuser = value; }
        //    get { return _vchar_updateuser; }
        //}
        /// <summary>
        /// 数据导入时间
        /// </summary>
        [XafDisplayName("数据导入时间")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public DateTime? dttm_updateDttm
        {
            set { _dttm_updatedttm = value; }
            get { return _dttm_updatedttm; }
        }
        /// <summary>
        /// 最后同步时间
        /// </summary>
        [XafDisplayName("最后同步时间")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public DateTime? dttm_AG_LastSyn
        {
            set { _dttm_ag_lastsyn = value; }
            get { return _dttm_ag_lastsyn; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int int_AG_Syn
        {
            set { _int_ag_syn = value; }
            get { return _int_ag_syn; }
        }
        /// <summary>
        /// 运输服务完毕
        /// </summary>
        [XafDisplayName("运输服务完毕")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string char_IsStop
        {
            set { _char_isstop = value; }
            get { return _char_isstop; }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string char_AG_Syn_sign
        {
            set { _char_ag_syn_sign = value; }
            get { return _char_ag_syn_sign; }
        }
        [XafDisplayName("代理名称")]
        public Agent Agent
        {
            get { return _agent ; }
            set { _agent = value; }
        }
    }
}
