using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Lottery365.Models
{
    public partial class UserDrawNumberDetail
    {
        [Key]
        public long Id { get; set; }
        public long TicketNumber { get; set; }
        public int Number { get; set; }
    }
}
