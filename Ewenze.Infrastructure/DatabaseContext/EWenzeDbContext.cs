using Ewenze.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ewenze.Infrastructure.DatabaseContext
{
    public class EWenzeDbContext : DbContext
    {
        public EWenzeDbContext(DbContextOptions<EWenzeDbContext> options) : base(options) { }

        public virtual DbSet<User>  Users { get; set; }
        public virtual DbSet<UserMeta> UserMetas { get; set; }  

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //User Mapping 
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("wpu0_users");
                entity.Property(e => e.Id).HasColumnType("bigint unsigned").IsRequired();
                entity.Property(e => e.LoginName).HasColumnType("varchar(60)").IsRequired().HasDefaultValue("");
                entity.Property(e => e.Password).HasColumnType("varchar(255)").IsRequired().HasDefaultValue("");
                entity.Property(e => e.NiceName).HasColumnType("varchar(50)").IsRequired().HasDefaultValue("");
                entity.Property(e => e.Email).HasColumnType("varchar(100)").IsRequired().HasDefaultValue("");
                entity.Property(e => e.Url).HasColumnType("varchar(100)").IsRequired().HasDefaultValue("");
                entity.Property(e => e.RegisteredDate).HasColumnType("datetime").IsRequired()
                      .HasDefaultValue(new DateTime()); 
                entity.Property(e => e.ActivationKey).HasColumnType("varchar(255)").IsRequired().HasDefaultValue("");
                entity.Property(e => e.UserStatus).HasColumnType("int").IsRequired().HasDefaultValue(0);
                entity.Property(e => e.DisplayName).HasColumnType("varchar(250)").IsRequired().HasDefaultValue("");
            });

            modelBuilder.Entity<UserMeta>(entity =>
            {
                entity.ToTable("wpu0_usermeta");
                entity.Property(e => e.Id).HasColumnType("bigint unsigned").IsRequired();
                entity.Property(e => e.UserId).HasColumnType("int").IsRequired();
                entity.Property(e => e.MetaKey).HasColumnType("varchar(255)").IsRequired();
                entity.Property(e => e.MetaValue).HasColumnType("varchar(255)");
            }); 
        }
    }
}
