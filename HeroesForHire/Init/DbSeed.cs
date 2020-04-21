using System;
using System.Threading.Tasks;
using HeroesForHire.Adapters.DataAccess;
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
            db.Add(nightVision);

            var nypd = new Customer(CustomerId.NewId(), "NYPD", "N.Y. Police"); 
            db.Add(nypd);
            var un = new Customer(CustomerId.NewId(), "UN", "United Nations");
            db.Add(un);
            
            var batman = new Hero(HeroId.NewId(), "Batman")
                .AddPower(nightVision);

            db.Heroes.Add(batman);
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