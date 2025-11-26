using Ewenze.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ewenze.Infrastructure.DatabaseContext
{
    public class EWenzeDbContext : DbContext
    {
        public EWenzeDbContext(DbContextOptions<EWenzeDbContext> options) : base(options) { }

        public virtual DbSet<UserV2> UserV2s { get; set; }
        public virtual DbSet<ListingTypeV2> ListingTypeV2s { get; set; }
        public virtual DbSet<ListingV2> ListingV2s { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region User Mapping 
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
            #endregion

            #region ListingTypeV2 Mapping
            modelBuilder.Entity<ListingTypeV2>(entity =>
            {
                entity.ToTable("listing_types");

                entity.HasKey(e => e.Id);

                // Required
                entity.Property(e => e.Label)
                    .IsRequired();

                // Unique
                entity.HasIndex(e => e.Label)
                    .IsUnique();

                // Column types
                entity.Property(e => e.Description)
                    .HasColumnType("text");

                // Defaults (DB)
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            #endregion

            #region Listing Mapping 
            modelBuilder.Entity<ListingV2>(entity =>
            {
                entity.ToTable("listings");

                entity.HasKey(e => e.Id);

                // FK
                entity.HasOne(e => e.ListingType)
                    .WithMany()
                    .HasForeignKey(e => e.ListingTypeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Required
                entity.Property(e => e.CategoryPath).IsRequired();
                entity.Property(e => e.Title).IsRequired();

                // Defaults
                entity.Property(e => e.PriceCurrency).HasDefaultValue("EUR");
                entity.Property(e => e.LocationCountry).HasDefaultValue("France");
                entity.Property(e => e.ViewCount).HasDefaultValue(0);
                entity.Property(e => e.IsFeatured).HasDefaultValue(false);
                entity.Property(e => e.Status)
                    .HasDefaultValue("DRAFT");

              entity.Property(e => e.Status)
                  .HasConversion<string>()
                  .HasMaxLength(20)
                  .HasDefaultValue(ListingStatus.DRAFT);

                // JSONB
                entity.Property(e => e.Images).HasColumnType("jsonb");
                entity.Property(e => e.DynamicFields).HasColumnType("jsonb");

                // Tags (TEXT[])
                entity.Property(e => e.Tags).HasColumnType("text[]");

                // POINT
                entity.Property(e => e.LocationCoordinates).HasColumnType("point");

                // Timestamp defaults handled in DB
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

                // Indexes
                entity.HasIndex(e => e.ListingTypeId);
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.Status);
                entity.HasIndex(e => e.CategoryPath);
                entity.HasIndex(e => e.CreatedAt);

                entity.HasIndex(e => e.Price)
                      .HasFilter("price IS NOT NULL");

                entity.HasIndex(e => e.LocationCity)
                      .HasFilter("location_city IS NOT NULL");

                entity.HasIndex(e => new { e.StartDate, e.EndDate })
                      .HasFilter("start_date IS NOT NULL");

                entity.HasIndex(e => e.IsFeatured)
                      .HasFilter("is_featured = TRUE");

                entity.HasIndex(e => e.LocationCoordinates)
                      .HasMethod("GIST")
                      .HasFilter("location_coordinates IS NOT NULL");

                entity.HasIndex(e => e.Tags)
                      .HasMethod("GIN")
                      .HasFilter("tags IS NOT NULL");

                entity.HasIndex(e => e.DynamicFields)
                      .HasMethod("GIN");
            });

            #endregion
        }
    }
}
