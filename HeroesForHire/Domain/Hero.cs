using System;
using System.Collections.Generic;

namespace HeroesForHire.Domain
{
    public class Hero : Entity<HeroId>
    {
        public string Name { get; private set; }
        private List<HeroPower> superpowers = new List<HeroPower>();
        public ICollection<HeroPower> Superpowers => superpowers.AsReadOnly();
        
        private List<HeroAssignment> assignments = new List<HeroAssignment>();
        public ICollection<HeroAssignment> Assignments => assignments.AsReadOnly();
        
        public Hero(HeroId id, string name)
        {
            Id = id;
            Name = name;
        }

        public Hero AddPower(Superpower power)
        {
            superpowers.Add(new HeroPower(this,power));
            return this;
        }

        public Hero Assign(Customer customer, DateRange period)
        {
            assignments.Add(new HeroAssignment(this,customer,period));
            return this;
        }

        protected Hero()
        {
        }
    }

    public class HeroPower : Entity<HeroPowerId>
    {
        public Hero Hero { get; }
        public Superpower Superpower { get; }

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