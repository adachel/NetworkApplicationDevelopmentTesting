using CommonChat.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ChatBD
{
    public class ChatContext : DbContext
    {
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public ChatContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // подключение к БД
        {
            optionsBuilder.LogTo(Console.WriteLine)
                          .UseLazyLoadingProxies()
<<<<<<< HEAD
                          .UseNpgsql("Host=localhost;Port=5432;Username=aaa;Password=1234;Database=BDAAA");
=======
                          .UseNpgsql("Host=localhost;Port=5432;Username=aaa;Password=1234;Database=fromWork");
>>>>>>> fromWork
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>(entity => 
            {
                entity.HasKey(m => m.Id).HasName("message_pkey");
                entity.ToTable("messages"); // можно указать свое наименование столбца
                entity.Property(m => m.Text).HasMaxLength(255).HasColumnName("text");
                entity.Property(m => m.FromUserId).HasColumnName("from_user_id");
                entity.Property(m => m.ToUserId).HasColumnName("to_user_id");

                entity.HasOne(m => m.FromUser)  // настройка связывания
                      .WithMany(u => u.FromMessages)
                      .HasForeignKey(m => m.FromUserId); // WithMany - связь от одного ко многим

                entity.HasOne(m => m.ToUser)
                      .WithMany(u => u.ToMessages)
                      .HasForeignKey(m => m.ToUserId);
            });
            base.OnModelCreating(modelBuilder);
        }


        // в терминале для ChatBD  -  dotnet tool install --global dotnet-ef --version 8.0.4
        // далее  -  dotnet ef migrations add InitialCreate. Получили два файла в каталоге Migrations.
        // для изменений в БД  -  dotnet ef database update
    }
}
