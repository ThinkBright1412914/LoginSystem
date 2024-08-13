﻿// <auto-generated />
using System;
using LoginSystem.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LoginSystem.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.30")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LoginSystem.Domain.Model.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("No_of_Tickets")
                        .HasColumnType("int");

                    b.Property<string>("SeatDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ShowId")
                        .HasColumnType("int");

                    b.Property<double>("TotalAmount")
                        .HasColumnType("float");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ShowId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("LoginSystem.Domain.Model.Carousel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("Carousels");
                });

            modelBuilder.Entity("LoginSystem.Domain.Model.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("LoginSystem.Domain.Model.Industry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Industries");
                });

            modelBuilder.Entity("LoginSystem.Domain.Model.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("LoginSystem.Domain.Model.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("IndustryId")
                        .HasColumnType("int");

                    b.Property<int>("LanguageId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.HasIndex("IndustryId");

                    b.HasIndex("LanguageId");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("LoginSystem.Domain.Model.RegisterUser", b =>
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

            modelBuilder.Entity("LoginSystem.Domain.Model.Role", b =>
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

            modelBuilder.Entity("LoginSystem.Domain.Model.Show", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReservedSeats")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SeatNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ShowDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ShowTimeId")
                        .HasColumnType("int");

                    b.Property<decimal>("TicketPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("ShowTimeId");

                    b.ToTable("Shows");
                });

            modelBuilder.Entity("LoginSystem.Domain.Model.ShowTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Time")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ShowTime");
                });

            modelBuilder.Entity("LoginSystem.Domain.Model.UserInfo", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ActivationCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("ImageFile")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsForcePasswordReset")
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
                            ExpirationDate = new DateTime(2024, 8, 14, 20, 10, 18, 34, DateTimeKind.Local).AddTicks(3711),
                            IsActive = true,
                            IsForcePasswordReset = false,
                            Password = "I0FkbWluMTIz",
                            UserName = "Admin"
                        });
                });

            modelBuilder.Entity("LoginSystem.Domain.Model.UserRole", b =>
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

            modelBuilder.Entity("LoginSystem.Domain.Model.Booking", b =>
                {
                    b.HasOne("LoginSystem.Domain.Model.Show", "ShowInfo")
                        .WithMany()
                        .HasForeignKey("ShowId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("LoginSystem.Domain.Model.UserInfo", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ShowInfo");

                    b.Navigation("User");
                });

            modelBuilder.Entity("LoginSystem.Domain.Model.Movie", b =>
                {
                    b.HasOne("LoginSystem.Domain.Model.Genre", "Genre")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LoginSystem.Domain.Model.Industry", "Industry")
                        .WithMany()
                        .HasForeignKey("IndustryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LoginSystem.Domain.Model.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Industry");

                    b.Navigation("Language");
                });

            modelBuilder.Entity("LoginSystem.Domain.Model.Show", b =>
                {
                    b.HasOne("LoginSystem.Domain.Model.Movie", "MovieInfo")
                        .WithMany()
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("LoginSystem.Domain.Model.ShowTime", "ShowTimeInfo")
                        .WithMany()
                        .HasForeignKey("ShowTimeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("MovieInfo");

                    b.Navigation("ShowTimeInfo");
                });

            modelBuilder.Entity("LoginSystem.Domain.Model.UserRole", b =>
                {
                    b.HasOne("LoginSystem.Domain.Model.Role", "Roles")
                        .WithMany("UsersRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LoginSystem.Domain.Model.UserInfo", "Users")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Roles");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("RoleUserInfo", b =>
                {
                    b.HasOne("LoginSystem.Domain.Model.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LoginSystem.Domain.Model.UserInfo", null)
                        .WithMany()
                        .HasForeignKey("UsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LoginSystem.Domain.Model.Role", b =>
                {
                    b.Navigation("UsersRoles");
                });

            modelBuilder.Entity("LoginSystem.Domain.Model.UserInfo", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
