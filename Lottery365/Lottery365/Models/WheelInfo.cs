using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Lottery365.Models
{
    [Table("WheelInfo")]
    [Index(nameof(WheelNumber), Name = "IX_WheelInfo", IsUnique = true)]
    public partial class WheelInfo
    {
        public WheelInfo()
        {
            UserLotteryDetails = new HashSet<UserLotteryDetail>();
            WheelDrawNumberDetails = new HashSet<WheelDrawNumberDetail>();
        }

        [Key]
        public long Id { get; set; }
        public long WheelNumber { get; set; }
        [Column(TypeName = "date")]
        public DateTime DrawDate { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<UserLotteryDetail> UserLotteryDetails { get; set; }
        public virtual ICollection<WheelDrawNumberDetail> WheelDrawNumberDetails { get; set; }
    }
}
