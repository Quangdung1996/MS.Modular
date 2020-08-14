using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Modular.AccountManagement.Domain.Accounts;
using MS.Modular.AccountManagement.Domain.Users;

namespace MS.Modular.AccountManagement.Infrastructure.Domain.AccountDataTransformations
{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("AccountDataTransformations", "administration");

            builder.HasKey(x => x.UserId);

            builder.OwnsOne<Account>("Account", b =>
            {
                b.Property(p => p.AccountId).HasColumnName("AccountId");
                b.Property(p => p.DateCreated).HasColumnName("DateCreated");
                b.Property(p => p.DateUpdated).HasColumnName("DateUpdated");
                b.Property(p => p.Name).HasColumnName("Name");
                b.Property(p => p.PurchasedApplications).HasColumnName("PurchasedApplications");
            });
            builder.OwnsOne<UserType>("UserType", b =>
            {
                b.Property(p => p.UserTypeId).HasColumnName("UserTypeId");
                b.Property(p => p.Description).HasColumnName("Description");
            });
        }
    }
}