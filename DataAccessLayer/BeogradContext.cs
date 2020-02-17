using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Model;
using System;

namespace DataAccessLayer
{
    public class BeogradContext : DbContext
    {
        public DbSet<Mesto> Mesta { get; set; }

        public BeogradContext( DbContextOptions<BeogradContext> options) : base(options)
        {
        }
    }
}
