using System;
using System.Collections.Generic;
using System.Linq;

namespace HeroesForHire.Domain
{
    public class Hero : Entity<HeroId>
    {
        public string Name { get; private set; }
        private List<HeroPower> superpowers = new List<HeroPower>();
        public virtual ICollection<HeroPower> Superpowers => superpowers.AsReadOnly();
        
        private List<HeroAssignment> assignments = new List<HeroAssignment>();
        public virtual ICollection<HeroAssignment> Assignments => assignments.AsReadOnly();
        
        public decimal DailyRate { get; private set; }
        
        public Hero(HeroId id, string name, decimal rate)
        {
            Id = id;
            Name = name;
            DailyRate = rate;
        }

        protected Hero()
        {
        }
        
        public Hero AddPower(Superpower power)
        {
            superpowers.Add(new HeroPower(this,power));
            return this;
        }

        public Hero Assign(Customer customer, DateRange period)
        {
            assignments.Add(new HeroAssignment(this, customer, period));
            return this;
        }

        public void CancelAssignment(DateRange period)
        {
            var assignmentToCancel =
                assignments.FirstOrDefault(a => a.Period.From == period.From && a.Period.To == period.To);
            
            assignmentToCancel?.Cancel();
        }
    }

    public class HeroPower : Entity<HeroPowerId>
    {
        public virtual Hero Hero { get; }
        public virtual Superpower Superpower { get; }

        public HeroPower(Hero hero, Superpower superpower)
        {
            Id = HeroPowerId.NewId();
            Hero = hero;
            Superpower = superpower;
        }

        protected HeroPower()
        {
        }
    }

    public class HeroId : ValueObject<HeroId>
    {
        public Guid Value { get; }

        public HeroId(Guid value)
        {
            Value = value;
        }

        protected HeroId()
        {
        }
        
        public static HeroId NewId() => new HeroId(Guid.NewGuid());

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Value;
        }
    }
    
    public class HeroPowerId : ValueObject<HeroPowerId>
    {
        public Guid Value { get; }

        public HeroPowerId(Guid value)
        {
            Value = value;
        }

        protected HeroPowerId()
        {
        }
        
        public static HeroPowerId NewId() => new HeroPowerId(Guid.NewGuid());

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Value;
        }
    }
}