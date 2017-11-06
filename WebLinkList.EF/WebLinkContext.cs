using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebLinkList.EF.Model;

namespace WebLinkList.EF
{
    public class WebLinkContext : DbContext
    {
        public DbSet<WebLink> WebLinks { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<WebLinkCategory> WebLinkCategories { get; set; }

        public DbSet<Usage> Usages { get; set; }

        public WebLinkContext(DbContextOptions options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Web Link
            modelBuilder.Entity<WebLink>().Property(t => t.Id).IsRequired();
            modelBuilder.Entity<WebLink>().Property(t => t.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<WebLink>().Property(t => t.Url).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<WebLink>().Property(t => t.CreatedDateTime).IsRequired();
            modelBuilder.Entity<WebLink>().HasMany(bc => bc.Usages).WithOne(c => c.WebLink).HasForeignKey(bc => bc.WebLinkId);

            // Category
            modelBuilder.Entity<Category>().Property(t => t.Id).IsRequired();
            modelBuilder.Entity<Category>().Property(t => t.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Category>().Property(t => t.CreatedDateTime).IsRequired();

            // Web Link Category
            modelBuilder.Entity<WebLinkCategory>().HasKey(bc => new { bc.WebLinkId, bc.CategoryId });
            modelBuilder.Entity<WebLinkCategory>() .HasOne(bc => bc.WebLink).WithMany((b => b.WebLinkCategories)).HasForeignKey(bc => bc.WebLinkId);
            modelBuilder.Entity<WebLinkCategory>().HasOne(bc => bc.Category).WithMany((c => c.WebLinkCategories)).HasForeignKey(bc => bc.CategoryId);

            // Usage
            modelBuilder.Entity<Usage>().Property(t => t.Id).IsRequired();
            modelBuilder.Entity<Usage>().Property(t => t.WebLinkId).IsRequired();
            modelBuilder.Entity<Usage>().Property(t => t.CreatedDateTime).IsRequired();
            modelBuilder.Entity<Usage>().HasOne(bc => bc.WebLink).WithMany(c => c.Usages).HasForeignKey(bc => bc.WebLinkId);
        }
    }
}
