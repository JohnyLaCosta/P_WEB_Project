using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ECarSharing.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<EUser> EUsers { get; set; }
        public DbSet<EUserType> EUserTypes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<MobilityCard> MobilityCards { get; set; }
        public DbSet<VehicleImage> VehicleImages { get; set; }
        public DbSet<AppAccount> AppAccounts { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}