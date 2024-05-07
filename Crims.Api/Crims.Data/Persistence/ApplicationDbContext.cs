using Crims.Data.Entities;
using Crims.Data.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crims.Data.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new AuditionInterceptor());
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRolesEntity>()
                .HasOne(p => p.User)
                .WithOne(p => p.UserRole)
                .HasForeignKey<UserEntity>(p => p.UserRoleId);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserRolesEntity> Roles { get; set; }
        public DbSet<TokenEntity> Tokens { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<EstablishmentEntity> Establishments { get; set; }
    }
}
