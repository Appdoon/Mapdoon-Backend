﻿// <auto-generated />
using System;
using Appdoon.Presistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Appdoon.Presistence.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20231025153806_rate")]
    partial class rate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Appdoon.Domain.Entities.HomeWorks.Homework", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CreatorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<int>("MinScore")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Homeworks");
                });

            modelBuilder.Entity("Appdoon.Domain.Entities.Homeworks.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Answer")
                        .HasColumnType("int");

                    b.Property<int>("HomeworkId")
                        .HasColumnType("int");

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<string>("Option1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Option2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Option3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Option4")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuestionDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("HomeworkId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Appdoon.Domain.Entities.Progress.ChildStepProgress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChildStepId")
                        .HasColumnType("int");

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDone")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChildStepId");

                    b.HasIndex("UserId");

                    b.ToTable("ChildStepProgresses");
                });

            modelBuilder.Entity("Appdoon.Domain.Entities.Progress.HomeworkProgress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("HomeworkId")
                        .HasColumnType("int");

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDone")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HomeworkId");

                    b.HasIndex("UserId");

                    b.ToTable("HomeworkProgresses");
                });

            modelBuilder.Entity("Appdoon.Domain.Entities.Progress.StepProgress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDone")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("StepId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StepId");

                    b.HasIndex("UserId");

                    b.ToTable("StepProgresses");
                });

            modelBuilder.Entity("Appdoon.Domain.Entities.Rates.RateRoadMap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<float>("Question1")
                        .HasColumnType("real");

                    b.Property<float>("Question2")
                        .HasColumnType("real");

                    b.Property<float>("Question3")
                        .HasColumnType("real");

                    b.Property<float>("Question4")
                        .HasColumnType("real");

                    b.Property<float>("Question5")
                        .HasColumnType("real");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("RoadMapId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoadMapId")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Rates");
                });

            modelBuilder.Entity("Appdoon.Domain.Entities.RoadMaps.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Appdoon.Domain.Entities.RoadMaps.ChildStep", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("HomeworkId")
                        .HasColumnType("int");

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("bit");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("StepId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("HomeworkId");

                    b.HasIndex("StepId");

                    b.ToTable("ChildSteps");
                });

            modelBuilder.Entity("Appdoon.Domain.Entities.RoadMaps.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TopBannerSrc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("Appdoon.Domain.Entities.RoadMaps.Linker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Linkers");
                });

            modelBuilder.Entity("Appdoon.Domain.Entities.RoadMaps.RoadMap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatoreId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageSrc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Stars")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CreatoreId");

                    b.ToTable("RoadMaps");
                });

            modelBuilder.Entity("Appdoon.Domain.Entities.RoadMaps.Step", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRequired")
                        .HasColumnType("bit");

                    b.Property<string>("Link")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("RoadMapId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("RoadMapId");

                    b.ToTable("Steps");
                });

            modelBuilder.Entity("Appdoon.Domain.Entities.Users.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            InsertTime = new DateTime(2023, 10, 25, 19, 8, 6, 337, DateTimeKind.Local).AddTicks(7843),
                            IsRemoved = false,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            InsertTime = new DateTime(2023, 10, 25, 19, 8, 6, 340, DateTimeKind.Local).AddTicks(7415),
                            IsRemoved = false,
                            Name = "Teacher"
                        },
                        new
                        {
                            Id = 3,
                            InsertTime = new DateTime(2023, 10, 25, 19, 8, 6, 340, DateTimeKind.Local).AddTicks(7572),
                            IsRemoved = false,
                            Name = "User"
                        });
                });

            modelBuilder.Entity("Appdoon.Domain.Entities.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InsertTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRemoved")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RemoveTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CategoryRoadMap", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("int");

                    b.Property<int>("RoadMapsId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesId", "RoadMapsId");

                    b.HasIndex("RoadMapsId");

                    b.ToTable("CategoryRoadMap");
                });

            modelBuilder.Entity("ChildStepLinker", b =>
                {
                    b.Property<int>("ChildStepsId")
                        .HasColumnType("int");

                    b.Property<int>("LinkersId")
                        .HasColumnType("int");

                    b.HasKey("ChildStepsId", "LinkersId");

                    b.HasIndex("LinkersId");

                    b.ToTable("ChildStepLinker");
                });

            modelBuilder.Entity("RoadMapUser", b =>
                {
                    b.Property<int>("SignedRoadMapsId")
                        .HasColumnType("int");

                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.HasKey("SignedRoadMapsId", "StudentsId");

                    b.HasIndex("StudentsId");

                    b.ToTable("RoadMapUser");
                });

            modelBuilder.Entity("RoadMapUser1", b =>
                {
                    b.Property<int>("BookmarkedRoadMapsId")
                        .HasColumnType("int");

                    b.Property<int>("UsersBookmarkedId")
                        .HasColumnType("int");

                    b.HasKey("BookmarkedRoadMapsId", "UsersBookmarkedId");

                    b.HasIndex("UsersBookmarkedId");

                    b.ToTable("RoadMapUser1");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<int>("RolesId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RoleUser");
                });

            modelBuilder.Entity("Appdoon.Domain.Entities.HomeWorks.Homework", b =>
                {
                    b.HasOne("Appdoon.Domain.Entities.Users.User", "Creator")
                        .WithMany("CreatedHomeworks")
                        .HasForeignKey("CreatorId");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Appdoon.Domain.Entities.Homeworks.Question", b =>
                {
                    b.HasOne("Appdoon.Domain.Entities.HomeWorks.Homework", "Homework")
                        .WithMany("Questions")
                        .HasForeignKey("HomeworkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Homework");
                });

            modelBuilder.Entity("Appdoon.Domain.Entities.Progress.ChildStepProgress", b =>
                {
                    b.HasOne("Appdoon.Domain.Entities.RoadMaps.ChildStep", "ChildStep")
                        .WithMany("ChildStepProgresses")
                        .HasForeignKey("ChildStepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Appdoon.Domain.Entities.Users.User", "User")
                        .WithMany("ChildStepProgresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChildStep");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Appdoon.Domain.Entities.Progress.HomeworkProgress", b =>
                {
                    b.HasOne("Appdoon.Domain.Entities.HomeWorks.Homework", "Homework")
                        .WithMany("HomeworkProgresses")
                        .HasForeignKey("HomeworkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Appdoon.Domain.Entities.Users.User", "User")
                        .WithMany("HomeworkProgresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Homework");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Appdoon.Domain.Entities.Progress.StepProgress", b =>
                {
                    b.HasOne("Appdoon.Domain.Entities.RoadMaps.Step", "Step")
                        .WithMany("StepProgresses")
                        .HasForeignKey("StepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Appdoon.Domain.Entities.Users.User", "User")
                        .WithMany("StepProgresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Step");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Appdoon.Domain.Entities.Rates.RateRoadMap", b =>
                {
                    b.HasOne("Appdoon.Domain.Entities.RoadMaps.RoadMap", "RoadMap")
                        .WithOne()
                        .HasForeignKey("Appdoon.Domain.Entities.Rates.RateRoadMap", "RoadMapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Appdoon.Domain.Entities.Users.User", "User")
                        .WithOne()
                        .HasForeignKey("Appdoon.Domain.Entities.Rates.RateRoadMap", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoadMap");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Appdoon.Domain.Entities.RoadMaps.ChildStep", b =>
                {
                    b.HasOne("Appdoon.Domain.Entities.HomeWorks.Homework", "Homework")
                        .WithMany("ChildSteps")
                        .HasForeignKey("HomeworkId");

                    b.HasOne("Appdoon.Domain.Entities.RoadMaps.Step", "Step")
                        .WithMany("ChildSteps")
                        .HasForeignKey("StepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Homework");

                    b.Navigation("Step");
                });

            modelBuilder.Entity("Appdoon.Domain.Entities.RoadMaps.Lesson", b =>
                {
                    b.HasOne("Appdoon.Domain.Entities.Users.User", "Creator")
                        .WithMany("CreatedLessons")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Appdoon.Domain.Entities.RoadMaps.RoadMap", b =>
                {
                    b.HasOne("Appdoon.Domain.Entities.Users.User", "Creatore")
                        .WithMany("CreatedRoadMaps")
                        .HasForeignKey("CreatoreId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Creatore");
                });

            modelBuilder.Entity("Appdoon.Domain.Entities.RoadMaps.Step", b =>
                {
                    b.HasOne("Appdoon.Domain.Entities.RoadMaps.RoadMap", "RoadMap")
                        .WithMany("Steps")
                        .HasForeignKey("RoadMapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoadMap");
                });

            modelBuilder.Entity("CategoryRoadMap", b =>
                {
                    b.HasOne("Appdoon.Domain.Entities.RoadMaps.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Appdoon.Domain.Entities.RoadMaps.RoadMap", null)
                        .WithMany()
                        .HasForeignKey("RoadMapsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ChildStepLinker", b =>
                {
                    b.HasOne("Appdoon.Domain.Entities.RoadMaps.ChildStep", null)
                        .WithMany()
                        .HasForeignKey("ChildStepsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Appdoon.Domain.Entities.RoadMaps.Linker", null)
                        .WithMany()
                        .HasForeignKey("LinkersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RoadMapUser", b =>
                {
                    b.HasOne("Appdoon.Domain.Entities.RoadMaps.RoadMap", null)
                        .WithMany()
                        .HasForeignKey("SignedRoadMapsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Appdoon.Domain.Entities.Users.User", null)
                        .WithMany()
                        .HasForeignKey("StudentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RoadMapUser1", b =>
                {
                    b.HasOne("Appdoon.Domain.Entities.RoadMaps.RoadMap", null)
                        .WithMany()
                        .HasForeignKey("BookmarkedRoadMapsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Appdoon.Domain.Entities.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UsersBookmarkedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("Appdoon.Domain.Entities.Users.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Appdoon.Domain.Entities.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Appdoon.Domain.Entities.HomeWorks.Homework", b =>
                {
                    b.Navigation("ChildSteps");

                    b.Navigation("HomeworkProgresses");

                    b.Navigation("Questions");
                });

            modelBuilder.Entity("Appdoon.Domain.Entities.RoadMaps.ChildStep", b =>
                {
                    b.Navigation("ChildStepProgresses");
                });

            modelBuilder.Entity("Appdoon.Domain.Entities.RoadMaps.RoadMap", b =>
                {
                    b.Navigation("Steps");
                });

            modelBuilder.Entity("Appdoon.Domain.Entities.RoadMaps.Step", b =>
                {
                    b.Navigation("ChildSteps");

                    b.Navigation("StepProgresses");
                });

            modelBuilder.Entity("Appdoon.Domain.Entities.Users.User", b =>
                {
                    b.Navigation("ChildStepProgresses");

                    b.Navigation("CreatedHomeworks");

                    b.Navigation("CreatedLessons");

                    b.Navigation("CreatedRoadMaps");

                    b.Navigation("HomeworkProgresses");

                    b.Navigation("StepProgresses");
                });
#pragma warning restore 612, 618
        }
    }
}
