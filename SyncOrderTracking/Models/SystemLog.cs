using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncOrderTracking.Models
{
    public class SystemLog
    {
        private int _int_id;
        private DateTime _dttm;
        private int? _agid;
        private string _result;
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
        public DateTime dttm
        {
            set { _dttm = value; }
            get { return _dttm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AGid
        {
            set { _agid = value; }
            get { return _agid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string result
        {
            set { _result = value; }
            get { return _result; }
        }
    }
}
