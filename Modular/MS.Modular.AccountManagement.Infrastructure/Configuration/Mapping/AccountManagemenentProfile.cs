using AutoMapper;
using MS.Modular.AccountManagement.Domain.AccountManagements;
using MS.Modular.AccountManagement.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Modular.AccountManagement.Infrastructure.Configuration.Mapping
{
    public class AccountManagemenentProfile : Profile
    {
        public AccountManagemenentProfile()
        {
            CreateMap<User, AccountDataTransformation>().ReverseMap();
        }
    }
}
