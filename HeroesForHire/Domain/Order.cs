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
        
        public string ProcessInstanceId { get; private set; }

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
            if (Status!=OrderStatus.New) 
                throw new ApplicationException("Only order in status New can have new offer");
            var offer = new Offer(this, candidateHero);
            candidateHero.Assign(this.Customer, this.Period.Clone());
            this.Status = OrderStatus.OfferCreated;
            this.Offer = offer;
        }

        public void RejectBecauseNoHeroesAvailable()
        {
            if (Status!=OrderStatus.New) 
                throw new ApplicationException("Only order in status New can be ended with no heroes available");
            this.Status = OrderStatus.NoHeroesAvailable;
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

        public void Cancel()
        {
            if (Status!=OrderStatus.Accepted) 
                throw new ApplicationException("Only order in status Accepted can be accepted");
            
            Status = OrderStatus.Cancelled;
            Offer.AssignedHero.CancelAssignment(Period);
        }
    }

    public enum OrderStatus
    {
        New,
        OfferCreated,
        NoHeroesAvailable,
        Accepted,
        Rejected,
        Cancelled
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