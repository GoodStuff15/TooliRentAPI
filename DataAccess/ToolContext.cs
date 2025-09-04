using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ToolContext : DbContext
    {
        public ToolContext(DbContextOptions<ToolContext> options) : base(options)
        {

        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<Tool> Tools { get; set; }

        public DbSet<ToolType> ToolTypes { get; set; }
        
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .Property(p => p.DelayPrice)
                .HasPrecision(10, 2);

            SeedData(modelBuilder);
        }

        public void SeedData(ModelBuilder modelBuilder)
        {

            // Seed Tool Categories

            modelBuilder.Entity<Category>().HasData(
            new Category
            {
            Id = 1,
            Name = "Power Tools",
            Description = "Electric and battery-powered tools for construction and repair.",
            DelayPrice = 15.00m
            },
            new Category
            {
            Id = 2,
            Name = "Hand Tools",
            Description = "Manual tools for everyday tasks and repairs.",
            DelayPrice = 5.00m
            },
            new Category
            {
            Id = 3,
            Name = "Garden Tools",
            Description = "Tools for gardening and landscaping.",
            DelayPrice = 8.50m
            }
            );

            // Seed ToolTypes
            modelBuilder.Entity<ToolType>().HasData(
                new ToolType { Id = 1, Name = "Cordless Drill", MaxLoanDays = 7, MinLoanDays = 1, CategoryId = 1 },
                new ToolType { Id = 2, Name = "Circular Saw", MaxLoanDays = 5, MinLoanDays = 1, CategoryId = 1 },
                new ToolType { Id = 3, Name = "Angle Grinder", MaxLoanDays = 4, MinLoanDays = 1, CategoryId = 1 },
                new ToolType { Id = 4, Name = "Hammer", MaxLoanDays = 10, MinLoanDays = 1, CategoryId = 2 },
                new ToolType { Id = 5, Name = "Screwdriver Set", MaxLoanDays = 10, MinLoanDays = 1, CategoryId = 2 },
                new ToolType { Id = 6, Name = "Wrench", MaxLoanDays = 8, MinLoanDays = 1, CategoryId = 2 },
                new ToolType { Id = 7, Name = "Lawn Mower", MaxLoanDays = 3, MinLoanDays = 1, CategoryId = 3 },
                new ToolType { Id = 8, Name = "Hedge Trimmer", MaxLoanDays = 3, MinLoanDays = 1, CategoryId = 3 },
                new ToolType { Id = 9, Name = "Shovel", MaxLoanDays = 7, MinLoanDays = 1, CategoryId = 3 },
                new ToolType { Id = 10, Name = "Rake", MaxLoanDays = 7, MinLoanDays = 1, CategoryId = 3 }
            );



        }
    }
}
