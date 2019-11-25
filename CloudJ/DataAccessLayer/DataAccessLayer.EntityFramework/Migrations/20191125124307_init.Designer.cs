﻿// <auto-generated />
using System;
using DataAccessLayer.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccessLayer.EntityFramework.Migrations
{
    [DbContext(typeof(CloudjContext))]
    [Migration("20191125124307_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DataAccessLayer.Models.Billing.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CustomerId");

                    b.Property<long>("PlanId");

                    b.Property<long>("SolutionId");

                    b.HasKey("Id");

                    b.HasIndex("PlanId");

                    b.HasIndex("SolutionId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Solution.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<long?>("ParentCategoryId");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Solution.Cloud", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ContainerId");

                    b.Property<string>("Name");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("ContainerId");

                    b.ToTable("Clouds");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Solution.DockerImage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Image");

                    b.HasKey("Id");

                    b.ToTable("DockerImages");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Solution.Photo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<byte[]>("Data");

                    b.Property<long>("SolutionId");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("SolutionId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Solution.Plan", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<double>("Price");

                    b.Property<long>("SolutionId");

                    b.Property<string>("Time");

                    b.HasKey("Id");

                    b.HasIndex("SolutionId");

                    b.ToTable("Plans");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Solution.Review", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorId");

                    b.Property<string>("Header");

                    b.Property<DateTime>("PostedTime");

                    b.Property<byte>("Rate");

                    b.Property<long>("SolutionId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("SolutionId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Solution.Solution", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CategoryId");

                    b.Property<long?>("CloudId");

                    b.Property<DateTime>("CreatedTime");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<byte>("Rate");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CloudId");

                    b.ToTable("Solutions");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Solution.SolutionLink", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<long>("SolutionId");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("SolutionId");

                    b.ToTable("SolutionLinks");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Billing.Order", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Solution.Plan", "Plan")
                        .WithMany()
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataAccessLayer.Models.Solution.Solution", "Solution")
                        .WithMany()
                        .HasForeignKey("SolutionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccessLayer.Models.Solution.Category", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Solution.Category", "ParentCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentCategoryId");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Solution.Cloud", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Solution.DockerImage", "Container")
                        .WithMany()
                        .HasForeignKey("ContainerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccessLayer.Models.Solution.Photo", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Solution.Solution", "Solution")
                        .WithMany("Photos")
                        .HasForeignKey("SolutionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccessLayer.Models.Solution.Plan", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Solution.Solution", "Solution")
                        .WithMany("Plans")
                        .HasForeignKey("SolutionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccessLayer.Models.Solution.Review", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Solution.Solution", "Solution")
                        .WithMany("Reviews")
                        .HasForeignKey("SolutionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DataAccessLayer.Models.Solution.Solution", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Solution.Category", "Category")
                        .WithMany("Solutions")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DataAccessLayer.Models.Solution.Cloud", "Cloud")
                        .WithMany()
                        .HasForeignKey("CloudId");
                });

            modelBuilder.Entity("DataAccessLayer.Models.Solution.SolutionLink", b =>
                {
                    b.HasOne("DataAccessLayer.Models.Solution.Solution", "Solution")
                        .WithMany("SolutionLinks")
                        .HasForeignKey("SolutionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
