using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Lottery365.Models
{
    public partial class WheelDrawNumberDetail
    {
        [Key]
        public int Id { get; set; }
        public long WheelNumber { get; set; }
        public int Number { get; set; }

        public virtual WheelInfo WheelNumberNavigation { get; set; }
    }
}
