using System;
using System.Collections.Generic;

namespace HeroesForHire.Domain
{
    public class Superpower : Entity<SuperpowerId>
    {
        public string Code { get; }
        public string Name { get; }

        public Superpower(string code, string name)
        {
            Id = SuperpowerId.NewId();
            Code = code;
            Name = name;
        }

        protected Superpower()
        {
        }
    }
    
    public class SuperpowerId : ValueObject<SuperpowerId>
    {
        public Guid Value { get; }

        public SuperpowerId(Guid value)
        {
            Value = value;
        }

        protected SuperpowerId()
        {
        }

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Value;
        }

        public static SuperpowerId NewId() => new SuperpowerId(Guid.NewGuid());
    }
}