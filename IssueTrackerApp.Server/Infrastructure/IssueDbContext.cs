using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using IssueTrackerApp.Server.Domain;

namespace IssueTrackerApp.Server.Infrastructure
{
    public class IssueDbContext : DbContext
    {
        public IssueDbContext(DbContextOptions<IssueDbContext> options) : base(options) { }
        public DbSet<IssueItem> Issues { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IssueItem>().HasKey(i => i.Id);
            modelBuilder.Entity<IssueItem>().Property(i => i.Title).IsRequired();
            modelBuilder.Entity<IssueItem>().Property(i => i.Priority).IsRequired();
            modelBuilder.Entity<IssueItem>().Property(i => i.Status).IsRequired();

        }
    }

}
