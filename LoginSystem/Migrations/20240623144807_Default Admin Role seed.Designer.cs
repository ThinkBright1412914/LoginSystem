﻿// <auto-generated />
using System;
using LoginSystem.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LoginSystem.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240623144807_Default Admin Role seed")]
    partial class DefaultAdminRoleseed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.30")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LoginSystem.Model.RegisterUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConfirmPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RegisterUsers");
                });

            modelBuilder.Entity("LoginSystem.Model.Role", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = new Guid("91fb10cd-b0ce-4e45-8763-6aaf1b8cb2f9"),
                            RoleName = "Admin"
                        },
                        new
                        {
                            RoleId = new Guid("115f896e-5a2a-4cf1-a90c-2bb3c01740dc"),
                            RoleName = "User"
                        });
                });

            modelBuilder.Entity("LoginSystem.Model.UserInfo", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ActivationCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("ImageFile")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("UserInfos");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("9d3a21ba-e76b-49e6-a24e-2cf9d1531994"),
                            ActivationCode = "678999",
                            Email = "nabinthekishor@gmail.com",
                            ExpirationDate = new DateTime(2024, 6, 24, 20, 33, 7, 420, DateTimeKind.Local).AddTicks(9401),
                            IsActive = true,
                            Password = "I0FkbWluMTIz",
                            UserName = "Admin"
                        });
                });

            modelBuilder.Entity("LoginSystem.Model.UserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("9d3a21ba-e76b-49e6-a24e-2cf9d1531994"),
                            RoleId = new Guid("91fb10cd-b0ce-4e45-8763-6aaf1b8cb2f9")
                        });
                });

            modelBuilder.Entity("RoleUserInfo", b =>
                {
                    b.Property<Guid>("RolesRoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsersUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RolesRoleId", "UsersUserId");

                    b.HasIndex("UsersUserId");

                    b.ToTable("RoleUserInfo");
                });

            modelBuilder.Entity("LoginSystem.Model.UserRole", b =>
                {
                    b.HasOne("LoginSystem.Model.Role", "Roles")
                        .WithMany("UsersRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LoginSystem.Model.UserInfo", "Users")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Roles");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("RoleUserInfo", b =>
                {
                    b.HasOne("LoginSystem.Model.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LoginSystem.Model.UserInfo", null)
                        .WithMany()
                        .HasForeignKey("UsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LoginSystem.Model.Role", b =>
                {
                    b.Navigation("UsersRoles");
                });

            modelBuilder.Entity("LoginSystem.Model.UserInfo", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
