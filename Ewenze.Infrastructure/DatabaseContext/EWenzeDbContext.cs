using Ewenze.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Nodes;

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
                entity.Property(x => x.Id)
                       .HasColumnName("id");

                // Required
                entity.Property(e => e.Label)
                    .HasColumnName("label")
                    .IsRequired();

                // Unique
                entity.HasIndex(e => e.Label)
                    .IsUnique();

                entity.Property(entity => entity.Icon)
                    .HasColumnName("icon")
                    .HasMaxLength(100);

                // Column types
                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("text");

                // Defaults (DB)
                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            #endregion

            #region Listing Mapping 
            modelBuilder.Entity<ListingV2>(entity =>
            {
                entity.ToTable("listings");

                // ---------- Primary key ----------
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id)
                       .HasColumnName("id");

                // ---------- Basic info ----------
                entity.Property(x => x.Title)
                       .HasColumnName("title")
                       .IsRequired()
                       .HasMaxLength(255);

                entity.Property(x => x.CategoryPath)
                       .HasColumnName("category_path")
                       .IsRequired()
                       .HasMaxLength(500);

                entity.Property(x => x.Description)
                       .HasColumnName("description");

                // ---------- Price ----------
                entity.Property(x => x.Price)
                       .HasColumnName("price")
                       .HasColumnType("numeric(12,2)");

                entity.Property(x => x.PriceCurrency)
                       .HasColumnName("price_currency")
                       .HasMaxLength(3)
                       .HasDefaultValue("EUR");

                // ---------- Location ----------
                entity.Property(x => x.City)
                       .HasColumnName("location_city")
                       .HasMaxLength(100);

                entity.Property(x => x.PostalCode)
                       .HasColumnName("location_postal_code")
                       .HasMaxLength(20);

                entity.Property(x => x.Country)
                       .HasColumnName("location_country")
                       .HasMaxLength(100)
                       .HasDefaultValue("France");

                // ---------- Dates ----------
                entity.Property(x => x.StartDate)
                       .HasColumnName("start_date");

                entity.Property(x => x.EndDate)
                       .HasColumnName("end_date");

                // ---------- Media ----------
                entity.Property(x => x.Images)
                       .HasColumnName("images")
                       .HasColumnType("jsonb")
                       .HasConversion(
                            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                            v => string.IsNullOrEmpty(v)
                                ? new List<string>()
                                : JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null) ?? new List<string>()
                       ).IsRequired(false);

                entity.Property(x => x.CoverImage)
                       .HasColumnName("cover_image")
                       .HasMaxLength(500);

                // ---------- Metadata ----------
                entity.Property(x => x.Tags)
                       .HasColumnName("tags")
                       .HasColumnType("text[]");

                entity.Property(x => x.ViewCount)
                       .HasColumnName("view_count")
                       .HasDefaultValue(0);

                entity.Property(x => x.IsFeatured)
                       .HasColumnName("is_featured")
                       .HasDefaultValue(false);

                entity.Property(x => x.DynamicFields)
                       .HasColumnName("dynamic_fields")
                       .HasColumnType("jsonb")
                       .HasConversion(
                            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                            v => string.IsNullOrEmpty(v)
                                ? null
                                : JsonSerializer.Deserialize<JsonObject>(v, (JsonSerializerOptions)null)
                       ).IsRequired(false);

                // ---------- Status ----------
                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasConversion<string>()
                    .HasMaxLength(20)
                    .HasDefaultValue(ListingStatus.DRAFT);

                // ---------- Timestamps ----------
                entity.Property(x => x.CreatedAt)
                       .HasColumnName("created_at")
                       .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(x => x.UpdatedAt)
                       .HasColumnName("updated_at")
                       .HasDefaultValueSql("CURRENT_TIMESTAMP");

                // ---------- Foreign Keys ----------
                entity.Property(x => x.ListingTypeId)
                       .HasColumnName("listing_type_id");

                entity.Property(x => x.UserId)
                       .HasColumnName("user_id");
                // Indexes
                entity.HasIndex(e => e.ListingTypeId);
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.Status);
                entity.HasIndex(e => e.CategoryPath);
                entity.HasIndex(e => e.CreatedAt);

                entity.HasIndex(e => e.Price)
                      .HasFilter("price IS NOT NULL");

                entity.HasIndex(e => e.City)
                      .HasFilter("location_city IS NOT NULL");

                entity.HasIndex(e => new { e.StartDate, e.EndDate })
                      .HasFilter("start_date IS NOT NULL");

                entity.HasIndex(e => e.IsFeatured)
                      .HasFilter("is_featured = TRUE");

                entity.HasIndex(e => e.Tags)
                      .HasMethod("GIN")
                      .HasFilter("tags IS NOT NULL");

                //entity.HasIndex(e => e.DynamicFields)
                //      .HasMethod("GIN");
            });

            #endregion
        }
    }
}
