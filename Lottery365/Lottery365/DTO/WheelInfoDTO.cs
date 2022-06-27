using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lottery365.DTO
{
    public class WheelInfoDTO
    {
        public long WheelNumber { get; set; }
        public DateTime DrawDate { get; set; }
        public bool Active { get; set; }
    }
}
