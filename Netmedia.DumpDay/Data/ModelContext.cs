using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using Netmedia.DumpDay.Models;
using Netmedia.Infrastructure.EntityFramework.Conventions;

namespace Netmedia.DumpDay.Data
{
    public class ModelContext : IdentityDbContext<User>
    {
        public ModelContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 1800;
        }

        public static ModelContext Create()
        {
            return new ModelContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new DecimalPrecision4PlacesForMoney());
            modelBuilder.Conventions.Add(new RenameEnumConvention());
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>().ToTable("Users").Property(p => p.Id).HasColumnName("Id");
            modelBuilder.Entity<User>().ToTable("Users").Property(p => p.Id).HasColumnName("Id");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");

            modelBuilder.Entity<IdentityUser>().HasMany(u => u.Roles).WithOptional().HasForeignKey(r => r.UserId);
            modelBuilder.Entity<IdentityUser>().HasMany(u => u.Roles).WithOptional().HasForeignKey(r => r.UserId);
            modelBuilder.Entity<IdentityUser>().HasMany(u => u.Logins).WithOptional().HasForeignKey(l => l.UserId);
            modelBuilder.Entity<IdentityUser>().HasMany(u => u.Claims).WithOptional().HasForeignKey(c => c.UserId);

            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }


        public IDbSet<Session> Sessions { get; set; }
    }
}