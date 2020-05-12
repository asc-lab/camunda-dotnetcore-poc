using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeroesForHire.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HeroesForHire.DataAccess
{
    public class HeroesDbContext : DbContext
    {
        public DbSet<Superpower> Superpowers { get; set; }
        
        public DbSet<Customer> Customers { get; set; }
        
        public DbSet<Hero> Heroes { get; set; }
        
        public DbSet<Order> Orders { get; set; }
        
        public DbSet<Offer> Offers { get; set; }
        
        public DbSet<Invoice> Invoices { get; set; }
        
        public DbSet<Notification> Notifications { get; set; }

        public HeroesDbContext(DbContextOptions options) : base(options)
        {
        }

        public async Task<List<Hero>> FindHeroForOrder(Order order)
        {
            return await Heroes
                .Where(h =>
                    h.Superpowers.Any(s => s.Superpower.Id == order.Superpower.Id)
                    && !h.Assignments.Any(a => a.Status!=AssignmentStatus.Cancelled && order.Period.To >= a.Period.From && order.Period.From <= a.Period.To)
                ).ToListAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SuperpowerConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new HeroConfiguration());
            modelBuilder.ApplyConfiguration(new HeroPowerConfiguration());
            modelBuilder.ApplyConfiguration(new HeroAssignmentConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OfferConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        private static void MapOffer(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Offer>()
                .HasKey(s => s.Id);
            modelBuilder.Entity<Offer>()
                .Property(s => s.Id)
                .HasConversion(s => s.Value, s => new OfferId(s));
            
            modelBuilder.Entity<Offer>()
                .HasOne(s => s.Order);
            
            modelBuilder.Entity<Offer>()
                .HasOne(s => s.AssignedHero);
            
            modelBuilder.Entity<Offer>()
                .Property(s => s.Status)
                .HasConversion(new EnumToStringConverter<OfferStatus>());
        }
    }
    
    class SuperpowerConfiguration : IEntityTypeConfiguration<Superpower>
    {
        public void Configure(EntityTypeBuilder<Superpower> modelBuilder)
        {
            modelBuilder.HasKey(s => s.Id);
            modelBuilder.Property(s => s.Id)
                .HasConversion(s => s.Value, s => new SuperpowerId(s));
            modelBuilder.Property(s => s.Code);
            modelBuilder.Property(s => s.Name);
        }
    }

    class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> modelBuilder)
        {
            modelBuilder.HasKey(s => s.Id);
            modelBuilder.Property(s => s.Id)
                .HasConversion(s => s.Value, s => new CustomerId(s));
            modelBuilder.Property(s => s.Code);
            modelBuilder.Property(s => s.Name);
        }
    }
    
    class HeroConfiguration : IEntityTypeConfiguration<Hero>
    {
        public void Configure(EntityTypeBuilder<Hero> modelBuilder)
        {
            modelBuilder.HasKey(s => s.Id);
            modelBuilder.Property(s => s.Id)
                .HasConversion(s => s.Value, s => new HeroId(s));
            modelBuilder.Property(s => s.Name);
            modelBuilder.Property(s => s.DailyRate);
        }
    }
    
    class HeroPowerConfiguration : IEntityTypeConfiguration<HeroPower>
    {
        public void Configure(EntityTypeBuilder<HeroPower> modelBuilder)
        {
            modelBuilder.HasKey(s => s.Id);
            modelBuilder.Property(s => s.Id)
                .HasConversion(s => s.Value, s => new HeroPowerId(s));
            modelBuilder.HasOne(p => p.Hero)
                .WithMany(s => s.Superpowers);
            modelBuilder.HasOne(s => s.Superpower);

        }
    }

    class HeroAssignmentConfiguration : IEntityTypeConfiguration<HeroAssignment>
    {
        public void Configure(EntityTypeBuilder<HeroAssignment> modelBuilder)
        {
            modelBuilder.HasKey(s => s.Id);
            modelBuilder.Property(s => s.Id)
                .HasConversion(s => s.Value, s => new HeroAssignmentId(s));
            modelBuilder.HasOne(p => p.Hero)
                .WithMany(s => s.Assignments);
            modelBuilder.HasOne(s => s.Customer);
            modelBuilder.OwnsOne(s => s.Period, p =>
                {
                    p.Property(x => x.From);
                    p.Property(x => x.To);
                });
            modelBuilder.Property(x => x.Status)
                .HasConversion(new EnumToStringConverter<AssignmentStatus>());
        }
    }
    
    class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> modelBuilder)
        {
            modelBuilder.HasKey(s => s.Id);
            modelBuilder.Property(s => s.Id)
                .HasConversion(s => s.Value, s => new OrderId(s));
            modelBuilder.HasOne(s => s.Superpower);
            modelBuilder.OwnsOne(s => s.Period, s =>
                {
                    s.Property(x => x.From);
                    s.Property(x => x.To);
                });
            modelBuilder.HasOne(s => s.Customer);
            modelBuilder.Property(s => s.Status).HasConversion(new EnumToStringConverter<OrderStatus>());
            modelBuilder.Property(s => s.ProcessInstanceId);
            modelBuilder.HasOne(s => s.Offer);
        }
    }
    
    class OfferConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> modelBuilder)
        {
            modelBuilder.HasKey(s => s.Id);
            modelBuilder.Property(s => s.Id)
                .HasConversion(s => s.Value, s => new OfferId(s));
            
            modelBuilder.HasOne(s => s.Order);
            
            modelBuilder.HasOne(s => s.AssignedHero);
            
            modelBuilder.Property(s => s.Status).HasConversion(new EnumToStringConverter<OfferStatus>());
        }
    }
    
    class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> modelBuilder)
        {
            modelBuilder.HasKey(s => s.Id);
            modelBuilder.Property(s => s.Id)
                .HasConversion(s => s.Value, s => new NotificationId(s));
            modelBuilder.Property(s => s.Text);
            modelBuilder.Property(s => s.TargetGroup);
            modelBuilder.Property(s => s.TargetUser);
            modelBuilder.Property(s => s.IsRead);
        }
    }
    
    class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> modelBuilder)
        {
            modelBuilder.HasKey(s => s.Id);
            modelBuilder.Property(s => s.Id)
                .HasConversion(s => s.Value, s => new InvoiceId(s));
            
            modelBuilder.HasOne(s => s.Order);
            
            modelBuilder.HasOne(s => s.Customer);
            
            modelBuilder.Property(s => s.Status).HasConversion(new EnumToStringConverter<InvoiceStatus>());
            
            modelBuilder.Property(s => s.Title);

            modelBuilder.Property((s => s.Amount));
        }
    }
}