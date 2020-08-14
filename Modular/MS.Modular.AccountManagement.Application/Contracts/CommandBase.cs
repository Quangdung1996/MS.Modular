using MS.Modular.AccountManagement.Domain.AccountManagements;
using MS.Modular.AccountManagement.Domain.Accounts;
using MS.Modular.BuildingBlocks.Domain;

namespace MS.Modular.AccountManagement.Application.Contracts
{
    public abstract class CommandBase<TResult> : ICommand<TResult>
    {
        protected CommandBase()
        {
            this.ReturnResponse = new ReturnResponse<AccountDataTransformation>();
        }

        protected CommandBase(ReturnResponse<AccountDataTransformation> returnRespons)
        {
            ReturnResponse = returnRespons;
        }

        public ReturnResponse<AccountDataTransformation> ReturnResponse { get; }
    }
}