using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Model;
using System;

namespace DataAccessLayer
{
    public class BeogradContext : DbContext
    {
        public DbSet<Mesto> Mesta { get; set; }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Kategorija> Kategorije { get; set; }
        public DbSet<Dogadjaj> Dogadjaji { get; set; }
        public DbSet<Komentar> Komentari { get; set; }
        public DbSet<Svidjanje> Svidjanja { get; set; }
        public DbSet<KategorijaDogadjaj> KategorijeDogadjaji { get; set; }

        public BeogradContext( DbContextOptions<BeogradContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Komentar>().HasKey(k => new { k.KorisnikID, k.DogadjajID });
            modelBuilder.Entity<Svidjanje>().HasKey(s => new { s.KorisnikID, s.DogadjajID });
            modelBuilder.Entity<KategorijaDogadjaj>().HasKey(kd => new { kd.KategorijaID, kd.DogadjajID });

        }
    }
}
