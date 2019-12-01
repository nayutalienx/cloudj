
using DataAccessLayer.Models;
using DataAccessLayer.Models.Billing;
using DataAccessLayer.Models.Collection;
using DataAccessLayer.Models.Solution;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccessLayer.EntityFramework
{
    public class CloudjContext : DbContext
    {
        public CloudjContext(DbContextOptions<CloudjContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }

        DbSet<Order> Orders { get; set; }
        DbSet<Balance> Balances { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Cloud> Clouds { get; set; }
        DbSet<DockerImage> DockerImages { get; set; }
        DbSet<Photo> Photos { get; set; }
        DbSet<Plan> Plans { get; set; }
        DbSet<Review> Reviews { get; set; }
        DbSet<Solution> Solutions { get; set; }
        DbSet<SolutionLink> SolutionLinks { get; set; } 
        DbSet<Collection> Collections { get; set; }
        


    }
}
