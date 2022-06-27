using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Lottery365.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        [InverseProperty(nameof(User.Role))]
        public virtual ICollection<User> Users { get; set; }
    }
}
