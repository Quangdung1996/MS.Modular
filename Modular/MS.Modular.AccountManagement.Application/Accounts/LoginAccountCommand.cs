using MS.Modular.AccountManagement.Application.Contracts;
using MS.Modular.AccountManagement.Domain.Accounts;
using MS.Modular.AccountManagement.Domain.ViewModels;
using MS.Modular.BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MS.Modular.AccountManagement.Application.Accounts
{
    public class LoginAccountCommand: CommandBase<ReturnResponse<RegisterAccountViewModel>>
    {
        public LoginAccountCommand(AccountSignIn accountLogin)
        {
            AccountLogin = accountLogin;
        }

        internal AccountSignIn AccountLogin { get; }
    }
}
