using EloBaza.Domain.SharedKernel;
using EloBaza.Domain.SharedKernel.Exceptions;
using System;

namespace EloBaza.Domain.UserAggregate
{
    public class User : AggregateRoot
    {
        public const int EmailMaxLength = 320;
        public const int DisplayNameMaxLength = 100;

        public string Email { get; private set; }
        public string DisplayName { get; private set; }
        public bool HasActiveSubscription { get; private set; }
        public DateTime? SubscriptionEndsAt { get; private set; }

        protected User(Guid key, string email, string displayName, bool hasActiveSubscription, DateTime? subscriptionEndsAt = null)
        {
            Key = key;
            Email = email;
            DisplayName = displayName;
            HasActiveSubscription = hasActiveSubscription;
            SubscriptionEndsAt = subscriptionEndsAt;
        }

        public static User Create(Guid key, string email, string displayName)
        {
            Validate(email, displayName);

            var user = new User(key, email, displayName, false);
            user.SetCreationData(user.Id);

            return user;
        }

        private static void Validate(string email, string displayName)
        {
            using var validationContext = new ValidationContext();

            validationContext.Validate(() => string.IsNullOrWhiteSpace(email), nameof(email), "Email must be provided");
            validationContext.Validate(() => string.IsNullOrWhiteSpace(displayName), nameof(displayName), "Display name must be provided");
        }
    }
}
