using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SeriesBoxd.Data
{
    public class SerieContext : IdentityDbContext
    {
        public SerieContext(DbContextOptions<SerieContext> options)
            : base(options)
        {
        }

        public DbSet<Entities.Models.Serie> Serie { get; set; } = default!;
        public DbSet<Entities.Models.Season> Season { get; set; } = default!;
        public DbSet<Entities.Models.Actor> Actor { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Serie>()
            .HasMany(s => s.Seasons)
            .WithOne(x => x.Serie)
            .HasForeignKey(s => s.SerieId);

            modelBuilder.Entity<Serie>()
            .HasMany(s => s.Actors)
            .WithMany(s => s.Series)
            .UsingEntity("SerieActor");

            base.OnModelCreating(modelBuilder);
        }
    }
}
