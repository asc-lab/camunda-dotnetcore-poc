using System;
using System.Collections.Generic;

namespace HeroesForHire.Domain
{
    public class HeroAssignment : Entity<HeroAssignmentId>
    {
        public Hero Hero { get; }
        public Customer Customer { get; }
        public DateRange Period { get; }
        public AssignmentStatus Status { get; protected set; }

        public HeroAssignment(Hero hero, Customer customer, DateRange period)
        {
            Id = HeroAssignmentId.NewId();
            Hero = hero;
            Customer = customer;
            Period = period;
            Status = AssignmentStatus.Planned;
        }

        protected HeroAssignment()
        {
        }

        public void Confirm()
        {
            Status = AssignmentStatus.Confirmed;
        }
    }

    public enum AssignmentStatus
    {
        Planned,
        Confirmed
    }
    
    public class HeroAssignmentId : ValueObject<HeroAssignmentId>
    {
        public Guid Value { get; }

        public HeroAssignmentId(Guid value)
        {
            Value = value;
        }

        protected HeroAssignmentId()
        {
        }
        
        public static HeroAssignmentId NewId() => new HeroAssignmentId(Guid.NewGuid());

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Value;
        }
    }
}