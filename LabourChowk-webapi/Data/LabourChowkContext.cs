using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabourChowk_webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace LabourChowk_webapi.Data
{
    public class LabourChowkContext : DbContext
    {
        public LabourChowkContext(DbContextOptions<LabourChowkContext> options)
                : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Job>()
                .HasKey(j => j.Id);

            modelBuilder.Entity<Worker>()
                .HasIndex(w => w.Phone).IsUnique();
            modelBuilder.Entity<WorkPoster>()
                .HasIndex(w => w.Phone).IsUnique();
        }

        public DbSet<Worker> Workers { get; set; }
        public DbSet<WorkPoster> WorkPosters { get; set; }
        public DbSet<Job> Jobs { get; set; }
    }
}