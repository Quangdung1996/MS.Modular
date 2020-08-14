using MediatR;
using MS.Modular.AccountManagement.Application.Contracts;
using MS.Modular.AccountManagement.Domain.AccountManagements;
using MS.Modular.AccountManagement.Domain.Accounts;
using MS.Modular.BuildingBlocks.Domain;

namespace MS.Modular.AccountManagement.Application.Accounts
{
    public class CreateAccountCommand : CommandBase<ReturnResponse<AccountDataTransformation>>
    {
        public CreateAccountCommand(AccountDataTransformation accountDataTransformation)
        {
            AccountDataTransformation = accountDataTransformation;
        }

        internal AccountDataTransformation AccountDataTransformation { get; }
    }
}