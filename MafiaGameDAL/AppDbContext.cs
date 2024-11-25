using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MafiaGameDAL.Models;

namespace MafiaGameDAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<SessionPlayer> SessionPlayers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=MafiaData;Username=postgres;Password=1111;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Session>()
                .HasOne(s => s.Creator)
                .WithMany()  // Можливо, буде багато користувачів, які створюють сесії
                .HasForeignKey(s => s.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Session)
                .WithMany()  // Якщо користувач може бути лише в одній сесії
                .HasForeignKey(u => u.SessionId)
                .OnDelete(DeleteBehavior.SetNull); // Можна змінити на будь-яку поведінку видалення
        }
    }
}
