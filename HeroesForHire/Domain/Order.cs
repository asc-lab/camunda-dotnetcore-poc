using System;
using System.Collections.Generic;

namespace HeroesForHire.Domain
{
    public class Order : Entity<OrderId>
    {
        public virtual Customer Customer { get; }
        public virtual Superpower Superpower { get; }
        public virtual DateRange Period { get; }
        
        public OrderStatus Status { get; protected set; }
        
        public string ProcessInstanceId { get; protected set; }

        public virtual Offer Offer { get; protected set; }

        public Order(Customer customer, Superpower superpower, DateRange period)
        {
            Id = OrderId.NewId();
            Customer = customer;
            Superpower = superpower;
            Period = period;
            Status = OrderStatus.New;
            ProcessInstanceId = null;
        }

        protected Order()
        {
        }

        public void AssociateWithProcessInstance(string processInstanceId)
        {
            this.ProcessInstanceId = processInstanceId;
        }

        public void CreateOfferWithHero(Hero candidateHero)
        {
            var offer = new Offer(this, candidateHero);
            candidateHero.Assign(this.Customer, this.Period);
            this.Status = OrderStatus.OfferCreated;
            this.Offer = offer;
        }

        public void AcceptOffer()
        {
            if (Status!=OrderStatus.OfferCreated) 
                throw new ApplicationException("Only order in status OfferCreated can be accepted");
            
            Status = OrderStatus.Accepted;
            Offer.Accept();
        }
        
        public void RejectOffer()
        {
            if (Status!=OrderStatus.OfferCreated) 
                throw new ApplicationException("Only order in status OfferCreated can be accepted");
            
            Status = OrderStatus.Rejected;
            Offer.Reject();
            Offer.AssignedHero.CancelAssignment(Period);
        }
    }

    public enum OrderStatus
    {
        New,
        OfferCreated,
        NoHeroesAvailable,
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