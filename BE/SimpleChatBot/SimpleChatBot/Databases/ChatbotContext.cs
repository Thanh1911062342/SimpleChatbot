using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SimpleChatBot.Databases.Entities;

namespace SimpleChatBot.Databases
{
    public partial class ChatbotContext : DbContext
    {
        public ChatbotContext()
        {
        }

        public ChatbotContext(DbContextOptions<ChatbotContext> options) : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<SendMail> SendMails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.KeyActive)
                    .HasColumnName("KeyActive")
                    .HasColumnType("nvarchar(max)") // or the size you want, like nvarchar(200)
                    .IsRequired(false)
                    .IsUnicode(false);

                entity.Property(e => e.Ip).IsUnicode(false);

                entity.Property(e => e.KeyDuration).HasColumnType("date");

                entity.Property(e => e.ModifiedDate).HasColumnType("date");
            });

            modelBuilder.Entity<SendMail>(entity =>
            {
                entity.ToTable("SendMail");

                entity.HasKey(s => s.Id);

                entity.Property(s => s.Id)
                        .ValueGeneratedOnAdd();

                entity.Property(s => s.Timestamp)
                        .HasColumnName("Timestamp")
                        .HasColumnType("bigint")
                        .IsRequired(false);

                entity.Property(s => s.MessageContent)
                    .HasColumnName("MessageContent")
                    .HasColumnType("nvarchar(max)")
                    .IsRequired(false);

                entity.Property(s => s.Ip)
                    .HasColumnName("Ip")
                    .HasColumnType("nvarchar(max)")
                    .IsRequired(false);

                entity.HasOne(s => s.Account)
                    .WithMany(a => a.SendMails)
                    .HasForeignKey(s => s.AccountId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
