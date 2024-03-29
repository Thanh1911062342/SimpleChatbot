﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleChatBot.Databases;

#nullable disable

namespace SimpleChatBot.Migrations
{
    [DbContext(typeof(ChatbotContext))]
    partial class ChatbotContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SimpleChatBot.Databases.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Ip")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("KeyActive")
                        .IsUnicode(false)
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("KeyActive");

                    b.Property<DateTime?>("KeyDuration")
                        .HasColumnType("date");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("Account", (string)null);
                });

            modelBuilder.Entity("SimpleChatBot.Databases.Entities.SendMail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("Ip")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Ip");

                    b.Property<string>("MessageContent")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MessageContent");

                    b.Property<long?>("Timestamp")
                        .HasColumnType("bigint")
                        .HasColumnName("Timestamp");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("SendMail", (string)null);
                });

            modelBuilder.Entity("SimpleChatBot.Databases.Entities.SendMail", b =>
                {
                    b.HasOne("SimpleChatBot.Databases.Entities.Account", "Account")
                        .WithMany("SendMails")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("SimpleChatBot.Databases.Entities.Account", b =>
                {
                    b.Navigation("SendMails");
                });
#pragma warning restore 612, 618
        }
    }
}
