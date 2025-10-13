using DevicesApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Device> Devices { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Device>(entity =>
            {
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Name).IsRequired();
                entity.Property(d => d.Brand).IsRequired();
                entity.Property(d => d.State).IsRequired();
                entity.Property(d => d.CreatedAt).IsRequired();
            });
        }
    }
}
