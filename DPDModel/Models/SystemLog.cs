using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPDModel.Models
{
    public class SystemLog
    {
        private int _int_id;
        private DateTime _dttm;
        private int? _agid;
        private string _result;
        [Browsable(false)]
        public int int_id
        {
            set { _int_id = value; }
            get { return _int_id; }
        }
        [DisplayName("服务器时间")]

        public DateTime dttm
        {
            set { _dttm = value; }
            get { return _dttm; }
        }
        [Browsable(false)]

        public int? AGid
        {
            set { _agid = value; }
            get { return _agid; }
        }
        [DisplayName("日志内容")]

        public string result
        {
            set { _result = value; }
            get { return _result; }
        }
    }
}
