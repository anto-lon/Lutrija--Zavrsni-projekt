using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Lottery365.Models
{
    [Index(nameof(TicketNumber), Name = "IX_UserLotteryDetails", IsUnique = true)]
    public partial class UserLotteryDetail
    {
        [Key]
        public int Id { get; set; }
        public long TicketNumber { get; set; }
        public int UserId { get; set; }
        public long WheelNumber { get; set; }
        [StringLength(50)]
        public string Status { get; set; }

        [ForeignKey(nameof(UserId))]
        [InverseProperty("UserLotteryDetails")]
        public virtual User User { get; set; }
        public virtual WheelInfo WheelNumberNavigation { get; set; }
    }
}
