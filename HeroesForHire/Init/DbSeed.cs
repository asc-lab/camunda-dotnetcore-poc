using System;
using System.Linq;
using System.Threading.Tasks;
using HeroesForHire.DataAccess;
using HeroesForHire.Domain;
using Microsoft.EntityFrameworkCore;

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
            db.Database.EnsureCreated();
            
            await CleanUp();

            if (db.Superpowers.Any())
                return;
            
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

            var wisdom = new Superpower("WISDOM", "Wisdom");
            db.Superpowers.Add(wisdom);

            
            var nypd = new Customer(CustomerId.NewId(), "NYPD", "N.Y. Police"); 
            db.Customers.Add(nypd);
            var un = new Customer(CustomerId.NewId(), "UN", "United Nations");
            db.Customers.Add(un);
            
            var batman = new Hero(HeroId.NewId(), "Batman", 1_000M)
                .AddPower(nightVision)
                .AddPower(superStrength);
            var darkWing = new Hero(HeroId.NewId(), "Dark Wing", 800M)
                .AddPower(superSpeed)
                .AddPower(nightVision);
            var captainAmerica = new Hero(HeroId.NewId(), "Captain America", 1_100M)
                .AddPower(superStrength);
            var hulk = new Hero(HeroId.NewId(), "Hulk", 500M)
                .AddPower(superStrength);
            var superman = new Hero(HeroId.NewId(), "Superman", 2_000M)
                .AddPower(superStrength)
                .AddPower(xRayVision)
                .AddPower(superSpeed)
                .AddPower(fly);
            var wonderWoman = new Hero(HeroId.NewId(), "Wonder Woman", 2_500M)
                .AddPower(superStrength)
                .AddPower(superSpeed)
                .AddPower(wisdom);

            db.Heroes.Add(batman);
            db.Heroes.Add(darkWing);
            db.Heroes.Add(captainAmerica);
            db.Heroes.Add(hulk);
            db.Heroes.Add(superman);
            db.Heroes.Add(wonderWoman);
            
            await db.SaveChangesAsync();
        }

        private async Task CleanUp()
        {
            db.Database.ExecuteSqlRaw("DELETE FROM \"Notifications\"");
            db.Database.ExecuteSqlRaw("DELETE FROM \"Invoices\"");
            db.Database.ExecuteSqlRaw("DELETE FROM \"Offers\"");
            db.Database.ExecuteSqlRaw("DELETE FROM \"Orders\"");
            db.Database.ExecuteSqlRaw("DELETE FROM \"HeroAssignment\"");
            db.Database.ExecuteSqlRaw("DELETE FROM \"HeroPower\"");
            db.Database.ExecuteSqlRaw("DELETE FROM \"Heroes\"");
            db.Database.ExecuteSqlRaw("DELETE FROM \"Customers\"");
            db.Database.ExecuteSqlRaw("DELETE FROM \"Superpowers\"");
            
            await db.SaveChangesAsync();
        }
    }
}