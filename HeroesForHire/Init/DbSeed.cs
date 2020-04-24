using System;
using System.Threading.Tasks;
using HeroesForHire.DataAccess;
using HeroesForHire.Domain;

namespace HeroesForHire.Init
{
    public class DbSeed
    {
        private readonly HeroesDbContext db;

        public DbSeed(HeroesDbContext db)
        {
            this.db = db;
        }

        public async Task SeedData()
        {
            var nightVision = new Superpower("NIGHT_VISION", "Night vision");
            db.Superpowers.Add(nightVision);
            
            var superSpeed = new Superpower("SUPER_SPEED", "Super speed");
            db.Superpowers.Add(superSpeed);

            var nypd = new Customer(CustomerId.NewId(), "NYPD", "N.Y. Police"); 
            db.Customers.Add(nypd);
            var un = new Customer(CustomerId.NewId(), "UN", "United Nations");
            db.Customers.Add(un);
            
            var batman = new Hero(HeroId.NewId(), "Batman")
                .AddPower(nightVision);
            var darkWing = new Hero(HeroId.NewId(), "Dark Wing")
                .AddPower(superSpeed)
                .AddPower(nightVision);

            db.Heroes.Add(batman);
            db.Heroes.Add(darkWing);
            
            batman.Assign(nypd, DateRange.Between(DateTime.Now.AddDays(-7), DateTime.Now));

            var orderOne = new Order(
                nypd,
                nightVision,
                DateRange.Between(DateTime.Now, DateTime.Now.AddDays(7))
            );
            db.Orders.Add(orderOne);

            await db.SaveChangesAsync();
        }
    }
}