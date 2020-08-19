using AutoMapper;
using MS.Modular.AccountManagement.Domain.Accounts;
using MS.Modular.AccountManagement.Domain.Users;
using MS.Modular.AccountManagement.Domain.ViewModels;

namespace MS.Modular.AccountManagement.Infrastructure.Configuration.Mapping
{
    public class AccountManagemenentProfile : Profile
    {
        public AccountManagemenentProfile()
        {
            CreateMap<User, RegisterAccountViewModel>().ReverseMap();
            CreateMap<User, CreateAccount>().ReverseMap();
        }
    }
}