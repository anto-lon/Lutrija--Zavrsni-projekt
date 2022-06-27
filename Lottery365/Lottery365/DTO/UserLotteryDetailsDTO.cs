using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lottery365.DTO
{
    public class UserLotteryDetailsDTO
    {
        public long TicketNumber { get; set; }
        public long WheelNumber { get; set; }
        public string DrawNumbers { get; set; }
        public string Status { get; set; }
        public int UserId { get; set; }
    }
}
