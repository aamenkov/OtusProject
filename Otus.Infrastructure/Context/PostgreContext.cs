using Microsoft.EntityFrameworkCore;
using Otus.Infrastructure.Entities;
using System.Data;

namespace Otus.Infrastructure.Context
{
    public class PostgreContext : DbContext
    {
        public PostgreContext()
        {
                
        }

        public PostgreContext(DbContextOptions<PostgreContext> options) : base(options)
        {
        }

        public virtual DbSet<Homework> Homeworks { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserGrade> UserGrades { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=OtusProject;Username=postgres;Password=postgres");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasComment("Пользователи");

                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.Name).HasColumnType("character varying");
            });

            modelBuilder.Entity<Homework>(entity =>
            {
                entity.HasComment("Домашние задания для пользователей");

                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.Title).HasColumnType("character varying");

                entity.Property(e => e.Description).HasColumnType("character varying");
            });

            modelBuilder.Entity<UserGrade>(entity =>
            {
                entity.HasComment("Оценки пользователей");

                entity.Property(e => e.Id).UseIdentityAlwaysColumn();


                entity.HasOne(d => d.User)
                    .WithMany(p => p.)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User");
            });
        }

    }
}
