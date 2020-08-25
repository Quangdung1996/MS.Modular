using MS.Modular.AccountManagement.Application.Contracts;
using MS.Modular.AccountManagement.Domain.Accounts;
using MS.Modular.AccountManagement.Domain.ViewModels;
using MS.Modular.BuildingBlocks.Domain;

namespace MS.Modular.AccountManagement.Application.Accounts
{
    public class CreateAccountCommand : CommandBase<ReturnResponse<RegisterAccountViewModel>>
    {
        public CreateAccountCommand(CreateAccount createAccount)
        {
            CreateAccount = createAccount;
        }

        internal CreateAccount CreateAccount { get; }
    }
}