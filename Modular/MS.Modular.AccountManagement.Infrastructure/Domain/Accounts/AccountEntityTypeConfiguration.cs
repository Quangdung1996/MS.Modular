using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Modular.AccountManagement.Domain.Accounts;

namespace MS.Modular.AccountManagement.Infrastructure.Domain.Accounts
{
    internal class AccountEntityTypeConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");

            builder.HasKey(x => x.AccountId);
        }
    }
}