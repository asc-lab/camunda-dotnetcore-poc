using System;
using System.Collections.Generic;

namespace HeroesForHire.Domain
{
    public class Customer : Entity<CustomerId>
    {
        public string Code { get; }
        public string Name { get; }

        public Customer(CustomerId id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }

        protected Customer()
        {
        }
    }
    
    public class CustomerId : ValueObject<CustomerId>
    {
        public Guid Value { get; }

        public CustomerId(Guid value)
        {
            Value = value;
        }

        protected CustomerId()
        {
        }
        
        public static CustomerId NewId() => new CustomerId(Guid.NewGuid());

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Value;
        }
    }
}