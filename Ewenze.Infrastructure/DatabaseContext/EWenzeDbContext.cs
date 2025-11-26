using Ewenze.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ewenze.Infrastructure.DatabaseContext
{
    public class EWenzeDbContext : DbContext
    {
        public EWenzeDbContext(DbContextOptions<EWenzeDbContext> options) : base(options) { }

        public virtual DbSet<UserV2> UserV2s { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //User Mapping 
            modelBuilder.Entity<UserV2>(entity =>
            {
                // Table
                entity.ToTable("users");

                // Primary Key
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                // Basic Info
                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsRequired();
                entity.HasIndex(e => e.Email)
                    .IsUnique();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(e => e.PasswordHash)
                    .HasColumnName("password_hash")
                    .HasMaxLength(255)
                    .IsRequired();

                // Contact Info
                entity.Property(e => e.Phone)
                    .HasColumnName("phone")
                    .HasMaxLength(20);

                entity.Property(e => e.PhoneVerified)
                    .HasColumnName("phone_verified")
                    .HasDefaultValue(false);

                // Personal Info
                entity.Property(e => e.Sex)
                    .HasColumnName("sex")
                    .HasMaxLength(10);

                entity.Property(e => e.Birthday)
                    .HasColumnName("birthday")
                    .HasColumnType("date");

                entity.Property(e => e.AvatarUrl)
                    .HasColumnName("avatar_url")
                    .HasMaxLength(500);

                // Security & Verification
                entity.Property(e => e.Otp)
                    .HasColumnName("otp")
                    .HasMaxLength(10);

                entity.Property(e => e.OtpExpiration)
                    .HasColumnName("otp_expiration")
                    .HasColumnType("timestamp with time zone");

                entity.Property(e => e.IsEmailVerified)
                    .HasColumnName("is_email_verified")
                    .HasDefaultValue(false);

                // E-commerce/Marketplace specific
                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasMaxLength(20)
                    .HasDefaultValue("customer");

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValue(true);

                // Preferences
                entity.Property(e => e.PreferredLanguage)
                    .HasColumnName("preferred_language")
                    .HasMaxLength(10)
                    .HasDefaultValue("fr");

                entity.Property(e => e.PreferredCurrency)
                    .HasColumnName("preferred_currency")
                    .HasMaxLength(3)
                    .HasDefaultValue("USD");

                entity.Property(e => e.NewsletterSubscribed)
                    .HasColumnName("newsletter_subscribed")
                    .HasDefaultValue(false);

                // Additional Info
                entity.Property(e => e.LastLoginAt)
                    .HasColumnName("last_login_at")
                    .HasColumnType("timestamp with time zone");

                // Timestamps
                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("timestamp with time zone")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasColumnType("timestamp with time zone")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
        }
    }
}
