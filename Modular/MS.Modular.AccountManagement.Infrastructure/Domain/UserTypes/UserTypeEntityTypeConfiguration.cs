using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MS.Modular.AccountManagement.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Modular.AccountManagement.Infrastructure.Domain.UserTypes
{
    public class UserTypeEntityTypeConfiguration : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> builder)
        {
            builder.ToTable("UserType");
            builder.HasKey(x => x.UserTypeId);
        }
    }
}
