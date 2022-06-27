using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Lottery365.Models
{
    public partial class User
    {
        public User()
        {
            UserLotteryDetails = new HashSet<UserLotteryDetail>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }
        [StringLength(20)]
        public string EmailId { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        public int RoleId { get; set; }

        [ForeignKey(nameof(RoleId))]
        [InverseProperty("Users")]
        public virtual Role Role { get; set; }
        [InverseProperty(nameof(UserLotteryDetail.User))]
        public virtual ICollection<UserLotteryDetail> UserLotteryDetails { get; set; }
    }
}
