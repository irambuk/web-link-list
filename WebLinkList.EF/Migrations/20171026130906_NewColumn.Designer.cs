﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using WebLinkList.EF;

namespace WebLinkList.EF.Migrations
{
    [DbContext(typeof(WebLinkContext))]
    [Migration("20171026130906_NewColumn")]
    partial class NewColumn
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebLinkList.EF.Model.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("WebLinkList.EF.Model.Usage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<Guid>("WebLinkId");

                    b.HasKey("Id");

                    b.HasIndex("WebLinkId");

                    b.ToTable("Usages");
                });

            modelBuilder.Entity("WebLinkList.EF.Model.WebLink", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<bool>("IsFaviourite");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.ToTable("WebLinks");
                });

            modelBuilder.Entity("WebLinkList.EF.Model.WebLinkCategory", b =>
                {
                    b.Property<Guid>("WebLinkId");

                    b.Property<Guid>("CategoryId");

                    b.HasKey("WebLinkId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("WebLinkCategories");
                });

            modelBuilder.Entity("WebLinkList.EF.Model.Usage", b =>
                {
                    b.HasOne("WebLinkList.EF.Model.WebLink", "WebLink")
                        .WithMany("Usages")
                        .HasForeignKey("WebLinkId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebLinkList.EF.Model.WebLinkCategory", b =>
                {
                    b.HasOne("WebLinkList.EF.Model.Category", "Category")
                        .WithMany("WebLinkCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebLinkList.EF.Model.WebLink", "WebLink")
                        .WithMany("WebLinkCategories")
                        .HasForeignKey("WebLinkId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
