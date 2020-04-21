using System;
using System.Collections.Generic;

namespace HeroesForHire.Domain
{
    public class DateRange : ValueObject<DateRange>
    {
        public virtual DateTime From { get; }
        public virtual DateTime To { get; }

        public DateRange(DateTime @from, DateTime to)
        {
            From = @from;
            To = to;
        }

        protected DateRange()
        {
        }

        public static DateRange Between(DateTime @from, DateTime to) => new DateRange(@from,to);

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return From;
            yield return To;
        }
    }
}