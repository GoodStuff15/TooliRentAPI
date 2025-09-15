using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ToolContext : IdentityDbContext<IdentityUser, IdentityRole, string>
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

            // Seed Tools
            modelBuilder.Entity<Tool>().HasData(
                new Tool { Id = 1, Name = "Makita Cordless Drill", Description = "High-performance cordless drill for heavy-duty tasks.", IsAvailable = true, ToolTypeId = 1 },
                new Tool { Id = 2, Name = "Bosch Power Drill", Description = "Reliable cordless drill suitable for home and professional use.", IsAvailable = true, ToolTypeId = 1 },

                new Tool { Id = 3, Name = "DeWalt Circular Saw", Description = "Precision circular saw for clean and accurate cuts.", IsAvailable = true, ToolTypeId = 2 },
                new Tool { Id = 4, Name = "Ryobi Circular Saw", Description = "Lightweight circular saw ideal for quick jobs.", IsAvailable = true, ToolTypeId = 2 },

                new Tool { Id = 5, Name = "Milwaukee Angle Grinder", Description = "Durable angle grinder for metal and masonry work.", IsAvailable = true, ToolTypeId = 3 },
                new Tool { Id = 6, Name = "Hitachi Angle Grinder", Description = "Compact angle grinder for detailed grinding tasks.", IsAvailable = true, ToolTypeId = 3 },

                new Tool { Id = 7, Name = "Stanley Claw Hammer", Description = "Classic claw hammer for carpentry and repairs.", IsAvailable = true, ToolTypeId = 4 },
                new Tool { Id = 8, Name = "Estwing Framing Hammer", Description = "Heavy-duty framing hammer for construction projects.", IsAvailable = true, ToolTypeId = 4 },

                new Tool { Id = 9, Name = "Wiha Screwdriver Set", Description = "Precision screwdriver set for electronics and small repairs.", IsAvailable = true, ToolTypeId = 5 },
                new Tool { Id = 10, Name = "Craftsman Screwdriver Set", Description = "Versatile screwdriver set for household tasks.", IsAvailable = true, ToolTypeId = 5 },

                new Tool { Id = 11, Name = "Klein Adjustable Wrench", Description = "Adjustable wrench for plumbing and mechanical work.", IsAvailable = true, ToolTypeId = 6 },
                new Tool { Id = 12, Name = "Irwin Pipe Wrench", Description = "Heavy-duty pipe wrench for tough jobs.", IsAvailable = true, ToolTypeId = 6 },

                new Tool { Id = 13, Name = "Honda Lawn Mower", Description = "Efficient lawn mower for medium to large gardens.", IsAvailable = true, ToolTypeId = 7 },
                new Tool { Id = 14, Name = "Greenworks Electric Mower", Description = "Eco-friendly electric mower for quiet operation.", IsAvailable = true, ToolTypeId = 7 },

                new Tool { Id = 15, Name = "Black+Decker Hedge Trimmer", Description = "Cordless hedge trimmer for easy garden maintenance.", IsAvailable = true, ToolTypeId = 8 },
                new Tool { Id = 16, Name = "Stihl Hedge Trimmer", Description = "Professional hedge trimmer for precise cutting.", IsAvailable = true, ToolTypeId = 8 },

                new Tool { Id = 17, Name = "Fiskars Garden Shovel", Description = "Sturdy garden shovel for digging and planting.", IsAvailable = true, ToolTypeId = 9 },
                new Tool { Id = 18, Name = "Ames Digging Shovel", Description = "Heavy-duty digging shovel for landscaping projects.", IsAvailable = true, ToolTypeId = 9 },

                new Tool { Id = 19, Name = "True Temper Leaf Rake", Description = "Wide leaf rake for efficient yard cleanup.", IsAvailable = true, ToolTypeId = 10 },
                new Tool { Id = 20, Name = "Garant Garden Rake", Description = "Durable garden rake for soil preparation.", IsAvailable = true, ToolTypeId = 10 }
            );

            //// Seed Borrowers
            //modelBuilder.Entity<Borrower>().HasData(
            //    new Borrower { Id = 1, FirstName = "Alice", LastName = "Johnson", IsActive = true },
            //    new Borrower { Id = 2, FirstName = "Michael", LastName = "Smith", IsActive = true },
            //    new Borrower { Id = 3, FirstName = "Sophie", LastName = "Williams", IsActive = true },
            //    new Borrower { Id = 4, FirstName = "David", LastName = "Brown", IsActive = true },
            //    new Borrower { Id = 5, FirstName = "Emma", LastName = "Davis", IsActive = true }
            //);



        }
    }
}
