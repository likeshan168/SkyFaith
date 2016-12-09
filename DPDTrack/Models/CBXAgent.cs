using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPDTrack.Models
{
    public class CBXAgent
    {
        public int AGid { get; set; }
        public string Agname { get; set; }

        public override string ToString()
        {
            return Agname;
        }
    }
}
