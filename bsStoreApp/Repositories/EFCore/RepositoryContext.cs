using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repositories.EFCore.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore
{
    public class RepositoryContext : IdentityDbContext<User> //veri tabanı işlemleri için //efcore
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options)
           : base(options)
        {
        }
        public DbSet<Libs> Libs { get; set; } //TABLO oluşturulması
        public DbSet<Category> Categories { get; set; } //TABLO oluşturulması

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new LibConfig());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }

    }
}
