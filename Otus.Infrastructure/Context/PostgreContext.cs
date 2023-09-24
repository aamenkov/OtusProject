using Microsoft.EntityFrameworkCore;
using DbConsole.Infrastructure.Entities;
using System.Data.SqlClient;

namespace DbConsole.Infrastructure.Context
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
            // Для отладки 
            //optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasComment("Пользователи");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Name).HasColumnType("character varying");

                entity.Property(x => x.IsLecturer)
                      .IsRequired()
                      .HasDefaultValue(false);

                entity.HasMany(e => e.UserGrades)
                       .WithOne(e => e.User)
                       .HasForeignKey(e => e.UserId);
            });

            modelBuilder.Entity<Homework>(entity =>
            {
                entity.HasComment("Домашние задания для пользователей");

                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.Title).HasColumnType("character varying");

                entity.Property(e => e.Description).HasColumnType("character varying");

                entity.HasMany(e => e.UserGrades)
                       .WithOne(e => e.Homework)
                       .HasForeignKey(e => e.HomeworkId);
            });

            modelBuilder.Entity<UserGrade>(entity =>
            {
                entity.HasComment("Оценки пользователей");

                entity.HasIndex(e => new { e.UserId, e.HomeworkId }, "Uniq_TypeGuid").IsUnique();

                entity.Property(e => e.UserGradeId).UseIdentityAlwaysColumn();

                entity.Property(x => x.IsPassed)
                      .IsRequired()
                      .HasDefaultValue(false);

                entity.Property(e => e.Quantity)
                      .HasAnnotation("Range", new[] { 0, 100 });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserGrades)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Grades");

                entity.HasOne(d => d.Homework)
                    .WithMany(p => p.UserGrades)
                    .HasForeignKey(d => d.HomeworkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Homeworks");

                entity.Property(p => p.DateTimeCreated)
                    .HasColumnType("timestamp with time zone")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
        }

    }
}
