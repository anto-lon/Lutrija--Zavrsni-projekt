using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lottery365.Models
{
    public partial class Lottery365Context : DbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder builder)
        {
            builder.Entity<User>(entity =>
            {
                entity.Property(x => x.Id).ValueGeneratedOnAdd();

            });
            builder.Entity<UserDrawNumberDetail>(entity =>
            {
                entity.Property(x => x.Id).ValueGeneratedOnAdd();

            });
            builder.Entity<UserLotteryDetail>(entity =>
            {
                entity.Property(x => x.Id).ValueGeneratedOnAdd();

            });
            builder.Entity<WheelInfo>(entity =>
            {
                entity.Property(x => x.Id).ValueGeneratedOnAdd();

            });
            builder.Entity<WheelDrawNumberDetail>(entity =>
            {
                entity.Property(x => x.Id).ValueGeneratedOnAdd();

            });
            builder.Entity<Role>(entity =>
            {
                entity.Property(x => x.Id).ValueGeneratedOnAdd();

            });
        }
    }
}
