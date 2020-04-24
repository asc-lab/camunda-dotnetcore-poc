using System;
using System.Collections.Generic;

namespace HeroesForHire.Domain
{
    public class Offer : Entity<OfferId>
    {
        public virtual Order Order { get; }
        public virtual Hero AssignedHero { get; }
        public OfferStatus Status { get; private set; }

        public Offer(Order order, Hero assignedHero)
        {
            Id = OfferId.NewId();
            Order = order;
            AssignedHero = assignedHero;
            Status = OfferStatus.New;
        }

        protected Offer()
        {
        }

        public void Accept()
        {
            if (Status!=OfferStatus.New) throw new ApplicationException("Only offer in status new can be accepted");
            Status = OfferStatus.Accepted;
        }
    }

    public enum OfferStatus
    {
        New,
        Accepted,
        Rejected
    }
    
    public class OfferId : ValueObject<OfferId>
    {
        public Guid Value { get; }

        public OfferId(Guid value)
        {
            Value = value;
        }

        protected OfferId()
        {
        }
        
        public static OfferId NewId() => new OfferId(Guid.NewGuid());

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Value;
        }
    }
}