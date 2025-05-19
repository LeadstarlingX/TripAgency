using Domain.Entities;
using Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Context
{
    public class IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> op) : IdentityDbContext<ApplicationUser, ApplicationRole, long>(op)
    {
        public virtual DbSet<IdentityUserClaim<long>> Claims { get; set; } 
        public virtual DbSet<IdentityUserLogin<long>> Logins { get; set; } 
        public virtual DbSet<IdentityUserToken<long>> Tokens { get; set; } 
        public override DbSet<ApplicationRole> Roles { get; set; }
        public override DbSet<ApplicationUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            #region IdentityDbContext 
            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.ToTable("Users");
                b.HasKey(p => p.Id);
                b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

                b.HasMany<IdentityUserClaim<long>>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired().HasConstraintName("FK_AspNetUserClaims_AspNetUsers_UserId");
                b.HasMany<IdentityUserRole<long>>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired().HasConstraintName("FK_AspNetUserRoles_AspNetUsers_UserId");
                b.HasMany<IdentityUserToken<long>>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired().HasConstraintName("FK_AspNetUserTokens_AspNetUsers_UserId");
                b.HasMany<IdentityUserLogin<long>>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired().HasConstraintName("FK_AspNetUserLogins_AspNetUsers_UserId");
            });

            modelBuilder.Entity<IdentityUserClaim<long>>(b =>
            {
                b.ToTable("UserClaims");
                b.HasKey(p => p.Id);
            });

            modelBuilder.Entity<IdentityUserLogin<long>>(b =>
            {
                b.ToTable("UserLogins");
                b.HasKey(p => new { p.UserId, p.LoginProvider, p.ProviderKey });
            });

            modelBuilder.Entity<IdentityUserToken<long>>(b =>
            {
                b.ToTable("UserTokens");
                b.HasKey(p => new { p.UserId, p.LoginProvider, p.Name });
            });

            modelBuilder.Entity<ApplicationRole>(b =>
            {
                b.ToTable("Roles");
                b.HasKey(p => p.Id);
                b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

                b.HasMany<IdentityUserRole<long>>().WithOne().HasForeignKey(us => us.RoleId).IsRequired();
                b.HasMany<IdentityRoleClaim<long>>().WithOne().HasForeignKey(uc => uc.RoleId).IsRequired();
            });

            modelBuilder.Entity<IdentityUserRole<long>>(b =>
            {
                b.ToTable("UserRoles");
                b.HasKey(p => new { p.UserId, p.RoleId });
            });

            modelBuilder.Entity<IdentityRoleClaim<long>>(b =>
            {
                b.ToTable("RoleClaims");
                b.HasKey(p => p.Id);
            });
            #endregion

        }

    }
}

//add-migration "name" -Context nameoftheDbcontext
//remove-migration  -Context nameoftheDbcontext 
//update-database -Context nameoftheDbcontext 

