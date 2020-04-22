using System.Collections.Generic;
using System.Linq;
using HeroesForHire.Domain;
using Microsoft.EntityFrameworkCore;

namespace HeroesForHire.DataAccess
{
    public class HeroesDbContext : DbContext
    {
        public DbSet<Superpower> Superpowers { get; set; }
        
        public DbSet<Customer> Customers { get; set; }
        
        public DbSet<Hero> Heroes { get; set; }
        
        public DbSet<Order> Orders { get; set; }

        public HeroesDbContext(DbContextOptions options) : base(options)
        {
        }

        public List<Hero> FindHeroForOrder(Order order)
        {
            return Heroes
                .Where(h =>
                    h.Superpowers.Any(s => s.Superpower.Id == order.Superpower.Id)
                    && !h.Assignments.Any(a => order.Period.To >= a.Period.From && order.Period.From <= a.Period.To)
                ).ToList();
        }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MapSuperpower(modelBuilder);
            MapCustomer(modelBuilder);
            MapHero(modelBuilder);
            MapOrder(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private static void MapSuperpower(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Superpower>()
                .HasKey(s => s.Id);
            modelBuilder.Entity<Superpower>()
                .Property(s => s.Id)
                .HasConversion(s => s.Value, s => new SuperpowerId(s));
            modelBuilder.Entity<Superpower>()
                .Property(s => s.Code);
            modelBuilder.Entity<Superpower>()
                .Property(s => s.Name);
        }
        
        private static void MapCustomer(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasKey(s => s.Id);
            modelBuilder.Entity<Customer>()
                .Property(s => s.Id)
                .HasConversion(s => s.Value, s => new CustomerId(s));
            modelBuilder.Entity<Customer>()
                .Property(s => s.Code);
            modelBuilder.Entity<Customer>()
                .Property(s => s.Name);
        }
        
        private static void MapHero(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hero>()
                .HasKey(s => s.Id);
            modelBuilder.Entity<Hero>()
                .Property(s => s.Id)
                .HasConversion(s => s.Value, s => new HeroId(s));
            modelBuilder.Entity<Hero>()
                .Property(s => s.Name);

            modelBuilder.Entity<HeroPower>()
                .HasKey(s => s.Id);
            modelBuilder.Entity<HeroPower>()
                .Property(s => s.Id)
                .HasConversion(s => s.Value, s => new HeroPowerId(s));
            modelBuilder.Entity<HeroPower>()
                .HasOne(p => p.Hero)
                .WithMany(s => s.Superpowers);
            modelBuilder.Entity<HeroPower>()
                .HasOne(s => s.Superpower);

            //HeroAssignment
            modelBuilder.Entity<HeroAssignment>()
                .HasKey(s => s.Id);
            modelBuilder.Entity<HeroAssignment>()
                .Property(s => s.Id)
                .HasConversion(s => s.Value, s => new HeroAssignmentId(s));
            modelBuilder.Entity<HeroAssignment>()
                .HasOne(p => p.Hero)
                .WithMany(s => s.Assignments);
            modelBuilder.Entity<HeroAssignment>()
                .HasOne(s => s.Customer);
            modelBuilder.Entity<HeroAssignment>()
                .OwnsOne(s => s.Period, p =>
                {
                    p.Property(x => x.From);
                    p.Property(x => x.To);
                });
            modelBuilder.Entity<HeroAssignment>()
                .Property(x => x.Status);
        }

        private static void MapOrder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasKey(s => s.Id);
            modelBuilder.Entity<Order>()
                .Property(s => s.Id)
                .HasConversion(s => s.Value, s => new OrderId(s));
            modelBuilder.Entity<Order>()
                .HasOne(s => s.Superpower);
            modelBuilder.Entity<Order>()
                .OwnsOne(s => s.Period, s =>
                {
                    s.Property(x => x.From);
                    s.Property(x => x.To);
                });
            modelBuilder.Entity<Order>()
                .HasOne(s => s.Customer);
            modelBuilder.Entity<Order>()
                .Property(s => s.Status);
            modelBuilder.Entity<Order>()
                .Property(s => s.ProcessInstanceId);
        }
    }
}