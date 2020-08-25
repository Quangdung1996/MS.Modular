using MS.Modular.AccountManagement.Application.Contracts;
using MS.Modular.AccountManagement.Domain.Accounts;
using MS.Modular.BuildingBlocks.Domain;

namespace MS.Modular.AccountManagement.Application.Accounts
{
    internal class UpdateAccountCommand : CommandUpdate<ReturnResponse<UpdateAccount>>
    {
        internal UpdateAccountCommand()
        {
        }

        internal UpdateAccount UpdateAccount { get; set; }
    }
}