using System;
using Livestream_Backend_application.DataTransfer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Livestream_Backend_application.Models
{
    public partial class LivestreamDBContext : IdentityDbContext<AppUser>
    {


        public LivestreamDBContext(DbContextOptions<LivestreamDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Followers> Followers { get; set; }
        public virtual DbSet<Streams> Streams { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<AppUser> appUsers {get; set;}
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Followers>(entity =>
            {
                entity.Property(e => e.FollowersId).HasColumnName("followers_id");

                entity.Property(e => e.FollowerId)
                    .HasColumnName("follower_id")
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UserdId).HasColumnName("userd_id");
            });

            modelBuilder.Entity<Streams>(entity =>
            {
                entity.HasKey(e => e.StreamId);

                entity.Property(e => e.StreamId).HasColumnName("stream_id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.UsersId).HasColumnName("users_id");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("firstname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasColumnName("lastname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasColumnName("role")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Streamkey)
                    .IsRequired()
                    .HasColumnName("streamkey")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

           // OnModelCreatingPartial(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
