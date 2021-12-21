using Domain.Users;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Infrastructure.Context
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var user in GetCatalogBrands())
            {
                modelBuilder.Entity<User>().HasData(user);
            }
        }

        private static IEnumerable<User> GetCatalogBrands()
        {
            SecurityHelper securityHelper = new SecurityHelper();
            return new List<User>()
            {
                new User() { Id=Guid.Parse("448D6B80-CBAD-4803-BD69-39DB8ACBC7AA"), UserName = "Arash" ,IsActive = true,Password= securityHelper.Getsha256Hash("123456")},
                new User() { Id=Guid.Parse("458D6B80-CBAD-4803-BD69-39DB8ACBC7AA"), UserName = "Ali ",IsActive = true,Password= securityHelper.Getsha256Hash("123456") },
                new User() { Id=Guid.Parse("468D6B80-CBAD-4803-BD69-39DB8ACBC7AA"), UserName = "Abolfazl",IsActive = true,Password= securityHelper.Getsha256Hash("123456") },
            };
        }
    }
}
