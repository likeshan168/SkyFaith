using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPDModel.Models
{
    public class GetWrodReplace
    {
        private int _int_id;
        private int _int_agid;
        private string _s_old;
        private string _s_new;
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
        public int int_agID
        {
            set { _int_agid = value; }
            get { return _int_agid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string s_Old
        {
            set { _s_old = value; }
            get { return _s_old; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string s_New
        {
            set { _s_new = value; }
            get { return _s_new; }
        }
    }
}
