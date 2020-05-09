using System;
using System.Collections.Generic;

namespace HeroesForHire.Domain
{
    public class Notification : Entity<NotificationId>
    {
        public string Text { get; }
        
        public string TargetGroup { get; }
        
        public string TargetUser { get; }
        
        public bool IsRead { get; private set; }

        public Notification(string text, string targetGroup, string targetUser)
        {
            Id = NotificationId.NewId();
            Text = text;
            TargetGroup = targetGroup;
            TargetUser = targetUser;
            IsRead = false;
        }

        public void MarkAsRead() => this.IsRead = true;

        protected Notification()
        {
        }
    }

    public class NotificationId : ValueObject<NotificationId>
    {
        public Guid Value { get; }
        
        public NotificationId(Guid value)
        {
            Value = value;
        }

        protected NotificationId()
        {
        }
        
        public static NotificationId NewId() => new NotificationId(Guid.NewGuid());

        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Value;
        }
    }
}