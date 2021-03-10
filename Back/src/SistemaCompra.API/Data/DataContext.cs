using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemaCompra.API.Model;


namespace SistemaCompra.API.Data
{
    public class DataContext : IdentityDbContext<Usuario, Roles, int,
                                                    IdentityUserClaim<int>,UserRole, IdentityUserLogin<int>,
                                                    IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }        
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserRole>(userRole => 
            {
                userRole.HasKey(ur => new {ur.UserId, ur.RoleId});

                userRole.HasOne(userRole => userRole.Role)
                                .WithMany(r => r.UserRoles)
                                .HasForeignKey(ur => ur.RoleId)
                                .IsRequired();
                userRole.HasOne(userRole => userRole.User)
                                .WithMany(r => r.UserRoles)
                                .HasForeignKey(ur => ur.UserId)
                                .IsRequired();
            }
            );
        }
    }
}