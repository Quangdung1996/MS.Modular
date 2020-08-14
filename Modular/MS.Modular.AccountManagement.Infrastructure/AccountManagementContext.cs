using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MS.Modular.AccountManagement.Domain.Accounts;
using MS.Modular.AccountManagement.Domain.Users;
using MS.Modular.AccountManagement.Infrastructure.Domain.AccountDataTransformations;
using MS.Modular.AccountManagement.Infrastructure.Domain.Accounts;
using MS.Modular.AccountManagement.Infrastructure.Domain.UserTypes;

namespace MS.Modular.AccountManagement.Infrastructure
{
    public class AccountManagementContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;

        public DbSet<User> Users { get; set; }

        internal DbSet<Account> Accounts { get; set; }

        internal DbSet<UserType> UserTypes { get; set; }

        public AccountManagementContext(DbContextOptions options, ILoggerFactory loggerFactory)
            : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserTypeEntityTypeConfiguration());
        }
    }
}