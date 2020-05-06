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
            
            var xRayVision = new Superpower("X_RAY_VISION", "X-ray vision");
            db.Superpowers.Add(xRayVision);
            
            var superSpeed = new Superpower("SUPER_SPEED", "Super speed");
            db.Superpowers.Add(superSpeed);

            var superStrength = new Superpower("SUPER_STRENGTH", "Super strength");
            db.Superpowers.Add(superStrength);
            
            var fly = new Superpower("FLY", "Fly ability");
            db.Superpowers.Add(fly);

            var wisdom = new Superpower("WISDOM", "Wisodom");
            db.Superpowers.Add(wisdom);

            
            var nypd = new Customer(CustomerId.NewId(), "NYPD", "N.Y. Police"); 
            db.Customers.Add(nypd);
            var un = new Customer(CustomerId.NewId(), "UN", "United Nations");
            db.Customers.Add(un);
            
            var batman = new Hero(HeroId.NewId(), "Batman")
                .AddPower(nightVision)
                .AddPower(superStrength);
            var darkWing = new Hero(HeroId.NewId(), "Dark Wing")
                .AddPower(superSpeed)
                .AddPower(nightVision);
            var captainAmerica = new Hero(HeroId.NewId(), "Captain America")
                .AddPower(superStrength);
            var hulk = new Hero(HeroId.NewId(), "Hulk")
                .AddPower(superStrength);
            var superman = new Hero(HeroId.NewId(), "Superman")
                .AddPower(superStrength)
                .AddPower(xRayVision)
                .AddPower(superSpeed)
                .AddPower(fly);
            var wonderWoman = new Hero(HeroId.NewId(), "Wonder Woman")
                .AddPower(superStrength)
                .AddPower(superSpeed)
                .AddPower(wisdom);

            db.Heroes.Add(batman);
            db.Heroes.Add(darkWing);
            db.Heroes.Add(captainAmerica);
            db.Heroes.Add(hulk);
            db.Heroes.Add(superman);
            db.Heroes.Add(wonderWoman);
            
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