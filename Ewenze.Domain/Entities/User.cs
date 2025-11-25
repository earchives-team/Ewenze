using System.ComponentModel.DataAnnotations.Schema;

namespace Ewenze.Domain.Entities
{
    public class UserV2
    {
        public int Id { get; set; }

        // Basic Info
        public string Email { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }

        // Contact Info
        public string? Phone { get; set; }
        public bool PhoneVerified { get; set; }

        // Personal Info
        public string? Sex { get; set; }
        public DateTime? Birthday { get; set; }
        public string? AvatarUrl { get; set; }

        // Security & Verification
        public string? Otp { get; set; }
        public DateTimeOffset? OtpExpiration { get; set; }
        public bool IsEmailVerified { get; set; }

        // E-commerce/Marketplace specific
        public string Role { get; set; }
        public bool IsActive { get; set; } = true;

        // Preferences
        public string PreferredLanguage { get; set; }
        public string PreferredCurrency { get; set; }
        public bool NewsletterSubscribed { get; set; }

        // Additional Info
        public DateTimeOffset? LastLoginAt { get; set; }

        // Timestamps
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
