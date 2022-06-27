using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lottery365.DTO
{
    public class WheelAndWinnerDetailsDTO
    {
        public long Id { get; set; }
        public long WheelNumber { get; set; }
        public string DrawNumbers { get; set; }
        public string Winners { get; set; }
        public DateTime DrawDate { get; set; }
        public bool Active { get; set; }
    }
}
