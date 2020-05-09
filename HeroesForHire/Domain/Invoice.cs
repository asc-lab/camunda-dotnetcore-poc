using System;
using System.Collections.Generic;

namespace HeroesForHire.Domain
{
    public class Invoice : Entity<InvoiceId>
    {
        public virtual Customer Customer { get; }
        
        public virtual Order Order { get; }
        
        public InvoiceStatus Status { get; private set; }
        
        public string Title { get; }
        
        public decimal Amount { get; }

        public Invoice(Customer customer, Order order, string title, decimal amount)
        {
            Id = InvoiceId.NewId();
            Customer = customer;
            Order = order;
            Title = title;
            Amount = amount;
            Status = InvoiceStatus.NEW;
        }

        public static Invoice ForOrder(Order o)
        {
            var title = $"For {o.Offer.AssignedHero.Name} work between {o.Period.From.ToShortDateString()} and {o.Period.To.ToShortDateString()}";
            var total = o.Offer.AssignedHero.DailyRate * o.Period.NumberOfDays;
            return new Invoice(o.Customer, o, title, total);
        }

        protected Invoice()
        {
        }

        public void MarkPaid() => this.Status = InvoiceStatus.PAID;

        public void Cancel() => this.Status = InvoiceStatus.CANCELLED;
    }

    public enum InvoiceStatus
    {
        NEW,
        PAID,
        CANCELLED
    }
    
    public class InvoiceId : ValueObject<InvoiceId>
    {
        public Guid Value { get; }

        public InvoiceId(Guid value)
        {
            Value = value;
        }

        protected InvoiceId()
        {
        }
        
        public static InvoiceId NewId() => new InvoiceId(Guid.NewGuid());

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Value;
        }
    }
}