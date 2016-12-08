using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SyncOrderTracking.Models
{
    [XmlRoot("Config",IsNullable =false)]
    public class Config
    {
        [XmlAttribute("TimeOut")]
        public int TimeOut { get; set; }

        [XmlAttribute("AGid")]
        public int AGid { get; set; }

        [XmlAttribute("AGname")]
        public string AGname { get; set; }
        [XmlAttribute("OverTime")]
        public int OverTime { get; set; }
        [XmlAttribute("SynSign")]
        public string SynSign { get; set; }
        [XmlAttribute("ProcessName")]
        public string ProcessName { get; set; }
    }
}
