using System;
using System.Collections.Generic;

namespace HeroesForHire.Domain
{
    public class HeroAssignment : Entity<HeroAssignmentId>
    {
        public virtual Hero Hero { get; }
        public virtual Customer Customer { get; }
        public virtual DateRange Period { get; }
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

        public void Cancel()
        {
            Status = AssignmentStatus.Cancelled;
        }
    }

    public enum AssignmentStatus
    {
        Planned,
        Confirmed,
        Cancelled
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