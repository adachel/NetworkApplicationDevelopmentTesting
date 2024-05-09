﻿// <auto-generated />
using ChatBD;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ChatBD.Migrations
{
    [DbContext(typeof(ChatContext))]
<<<<<<<< HEAD:ChatBD/Migrations/20240501113020_InitialCreate.Designer.cs
    [Migration("20240501113020_InitialCreate")]
========
    [Migration("20240502102536_InitialCreate")]
>>>>>>>> fromWork:ChatBD/Migrations/20240502102536_InitialCreate.Designer.cs
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CommonChat.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("FromUserId")
                        .HasColumnType("integer")
                        .HasColumnName("from_user_id");

                    b.Property<bool>("IsReceived")
                        .HasColumnType("boolean");

                    b.Property<string>("Text")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("text");

                    b.Property<int>("ToUserId")
                        .HasColumnType("integer")
                        .HasColumnName("to_user_id");

                    b.HasKey("Id")
                        .HasName("message_pkey");

                    b.HasIndex("FromUserId");

                    b.HasIndex("ToUserId");

                    b.ToTable("messages", (string)null);
                });

            modelBuilder.Entity("CommonChat.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CommonChat.Models.Message", b =>
                {
                    b.HasOne("CommonChat.Models.User", "FromUser")
                        .WithMany("FromMessages")
                        .HasForeignKey("FromUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CommonChat.Models.User", "ToUser")
                        .WithMany("ToMessages")
                        .HasForeignKey("ToUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FromUser");

                    b.Navigation("ToUser");
                });

            modelBuilder.Entity("CommonChat.Models.User", b =>
                {
                    b.Navigation("FromMessages");

                    b.Navigation("ToMessages");
                });
#pragma warning restore 612, 618
        }
    }
}
