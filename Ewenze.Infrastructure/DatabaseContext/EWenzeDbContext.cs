using Ewenze.Domain.Entities;
using Ewenze.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ewenze.Infrastructure.DatabaseContext
{
    public class EWenzeDbContext : DbContext
    {
        public EWenzeDbContext(DbContextOptions<EWenzeDbContext> options) : base(options) { }

        public virtual DbSet<UserV2> UserV2s { get; set; }
        public virtual DbSet<UserMeta> UserMetas { get; set; }
        public virtual DbSet<PostTypeEntity> PostTypes { get; set; }

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

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasMaxLength(20);

                entity.Property(e => e.IsActive)
                    .HasColumnName("is_active")
                    .HasDefaultValue(true);

                // Preferences
                entity.Property(e => e.PreferredLanguage)
                    .HasColumnName("preferred_language")
                    .HasMaxLength(10);

                entity.Property(e => e.PreferredCurrency)
                    .HasColumnName("preferred_currency")
                    .HasMaxLength(5);

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

            modelBuilder.Entity<UserMeta>(entity =>
            {
                entity.ToTable("wpu0_usermeta");
                entity.Property(e => e.Id).HasColumnType("bigint unsigned").IsRequired();
                entity.Property(e => e.UserId).HasColumnType("int").IsRequired();
                entity.Property(e => e.MetaKey).HasColumnType("varchar(255)").IsRequired();
                entity.Property(e => e.MetaValue).HasColumnType("varchar(255)");
            });

            modelBuilder.Entity<PostTypeEntity>(entity =>
            {
                entity.ToTable("wpu0_posts");
                entity.Property(p => p.Id).HasColumnType("bigint unsigned").IsRequired();
                entity.Property(p => p.PostTitle).HasColumnType("text");
                entity.Property(p => p.PostStatus).HasColumnType("varchar(20)");
                entity.Property(p => p.PostContent).HasColumnType("longtext");
                entity.Property(p => p.PostModified).HasColumnType("datetime");
                entity.Property(p => p.PostDate).HasColumnType("datetime");
                entity.Property(p => p.PostType).HasColumnType("varchar(20)");
            });
        }
    }
}
