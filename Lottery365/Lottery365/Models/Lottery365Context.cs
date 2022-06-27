using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Lottery365.Models
{
    public partial class Lottery365Context : DbContext
    {
        public Lottery365Context()
        {
        }

        public Lottery365Context(DbContextOptions<Lottery365Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserDrawNumberDetail> UserDrawNumberDetails { get; set; }
        public virtual DbSet<UserLotteryDetail> UserLotteryDetails { get; set; }
        public virtual DbSet<WheelDrawNumberDetail> WheelDrawNumberDetails { get; set; }
        public virtual DbSet<WheelInfo> WheelInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Database=Lottery365;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Roles");
            });

            modelBuilder.Entity<UserLotteryDetail>(entity =>
            {
                entity.Property(e => e.Status).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLotteryDetails)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserLotteryDetails_Users");

                entity.HasOne(d => d.WheelNumberNavigation)
                    .WithMany(p => p.UserLotteryDetails)
                    .HasPrincipalKey(p => p.WheelNumber)
                    .HasForeignKey(d => d.WheelNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserLotteryDetails_WheelInfo");
            });

            modelBuilder.Entity<WheelDrawNumberDetail>(entity =>
            {
                entity.HasOne(d => d.WheelNumberNavigation)
                    .WithMany(p => p.WheelDrawNumberDetails)
                    .HasPrincipalKey(p => p.WheelNumber)
                    .HasForeignKey(d => d.WheelNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WheelDrawNumberDetails_WheelInfo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
