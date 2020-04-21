using System;
using System.Collections.Generic;

namespace HeroesForHire.Domain
{
    public class Order : Entity<OrderId>
    {
        public Customer Customer { get; }
        public Superpower Superpower { get; }
        public DateRange Period { get; }
        
        public OrderStatus Status { get; protected set; }

        public Order(Customer customer, Superpower superpower, DateRange period)
        {
            Id = OrderId.NewId();
            Customer = customer;
            Superpower = superpower;
            Period = period;
            Status = OrderStatus.New;
        }

        protected Order()
        {
        }
    }

    public enum OrderStatus
    {
        New,
        OfferCreated,
        Accepted,
        Rejected
    }
    
    public class OrderId : ValueObject<OrderId>
    {
        public Guid Value { get; }

        public OrderId(Guid value)
        {
            Value = value;
        }

        protected OrderId()
        {
        }
        
        public static OrderId NewId() => new OrderId(Guid.NewGuid());

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Value;
        }
    }
}