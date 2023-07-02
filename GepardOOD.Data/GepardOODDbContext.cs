using System.Reflection;
using GepardOOD.Data.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GepardOOD.Web.Data
{
    public class GepardOODDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public GepardOODDbContext(DbContextOptions<GepardOODDbContext> options)
            : base(options)
        {
        }

        public DbSet<BeerCategory> BeerCategories { get; set; } = null!;

        public DbSet<SodaCategory> SodaCategories { get; set; } = null!;

        public DbSet<WineCategory> WineCategories { get; set; } = null!;

        public DbSet<WhiskeyCategory> WhiskeyCategories { get; set; } = null!;

        public DbSet<Beer> Beers { get; set; } = null!;

        public DbSet<Soda> Sodas { get; set; } = null!;

        public DbSet<Wine> Wines { get; set; } = null!;

        public DbSet<Whiskey> Whiskeys { get; set; } = null!;

        public DbSet<Associate> Associates { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Assembly configAssembly = Assembly.GetAssembly(typeof(GepardOODDbContext)) ??
                                      Assembly.GetExecutingAssembly();

            builder.ApplyConfigurationsFromAssembly(configAssembly);


            base.OnModelCreating(builder);
        }
    }
}