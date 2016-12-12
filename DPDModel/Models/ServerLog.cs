using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPDModel.Models
{
    public class ServerLog
    {
        private int _log_id;
        private string _char_type;
        private DateTime _log_time;
        private string _sfi_number;
        private string _ag_number;
        private int? _ag_id;
        private string _vchar_result;
        private string _nvchar_error;
        [Browsable(false)]
        public int log_id
        {
            set { _log_id = value; }
            get { return _log_id; }
        }
        [Browsable(false)]
        public string char_type
        {
            set { _char_type = value; }
            get { return _char_type; }
        }
        [DisplayName("时间")]
        public DateTime log_time
        {
            set { _log_time = value; }
            get { return _log_time; }
        }
        [DisplayName("SFI单号")]
        public string SFI_Number
        {
            set { _sfi_number = value; }
            get { return _sfi_number; }
        }
        [DisplayName("代理单号")]

        public string AG_Number
        {
            set { _ag_number = value; }
            get { return _ag_number; }
        }
        [Browsable(false)]

        public int? AG_id
        {
            set { _ag_id = value; }
            get { return _ag_id; }
        }
        [DisplayName("日志描述")]
        public string vchar_result
        {
            set { _vchar_result = value; }
            get { return _vchar_result; }
        }
        [DisplayName("错误信息")]

        public string nvchar_Error
        {
            set { _nvchar_error = value; }
            get { return _nvchar_error; }
        }
    }
}
