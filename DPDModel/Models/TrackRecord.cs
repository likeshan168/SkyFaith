using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPDModel.Models
{
    public class TrackRecord
    {
        private int _int_id;
        private int _int_mappingid;
        private string _vchar_sfinum;
        private string _vchar_agnum;
        private int _int_agid;
        private DateTime _dttm_record_dttm;
        private string _nvchar_description;
        private string _nvchar_description_local;
        private string _nvchar_city;
        private string _vchar_syncode;
        /// <summary>
        /// 
        /// </summary>
        public int int_id
        {
            set { _int_id = value; }
            get { return _int_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int int_MappingId
        {
            set { _int_mappingid = value; }
            get { return _int_mappingid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string vchar_SFInum
        {
            set { _vchar_sfinum = value; }
            get { return _vchar_sfinum; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string vchar_AGnum
        {
            set { _vchar_agnum = value; }
            get { return _vchar_agnum; }
        }
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
        public DateTime dttm_Record_Dttm
        {
            set { _dttm_record_dttm = value; }
            get { return _dttm_record_dttm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string nvchar_Description
        {
            set { _nvchar_description = value; }
            get { return _nvchar_description; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string nvchar_Description_Local
        {
            set { _nvchar_description_local = value; }
            get { return _nvchar_description_local; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string nvchar_City
        {
            set { _nvchar_city = value; }
            get { return _nvchar_city; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string vchar_synCode
        {
            set { _vchar_syncode = value; }
            get { return _vchar_syncode; }
        }
    }
}
