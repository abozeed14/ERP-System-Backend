using ERP.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Data.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region Identity Tables 
            builder.Entity<ApplicationUser>(e => e.ToTable("Users"));
            builder.Entity<IdentityRole>(e => e.ToTable("Roles"));
            builder.Entity<IdentityUserRole<string>>(e=>e.ToTable("UserRoles"));
            builder.Entity<IdentityUserClaim<string>>(e=>e.ToTable("UserClaims"));
            builder.Entity<IdentityRoleClaim<string>>(e=>e.ToTable("RoleClaims"));
            builder.Entity<IdentityUserLogin<string>>(e=>e.ToTable("UserLogins"));
            builder.Entity<IdentityUserToken<string>>(e=>e.ToTable("UserTokens"));
            #endregion


        }

    }
}
