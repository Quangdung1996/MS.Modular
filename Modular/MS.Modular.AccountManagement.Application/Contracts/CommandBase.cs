using MS.Modular.AccountManagement.Domain.AccountManagements;
using MS.Modular.AccountManagement.Domain.Accounts;
using MS.Modular.AccountManagement.Domain.ViewModels;
using MS.Modular.BuildingBlocks.Domain;

namespace MS.Modular.AccountManagement.Application.Contracts
{
    public abstract class CommandBase<TResult> : ICommand<TResult>
    {
        protected CommandBase()
        {
            this.ReturnResponse = new ReturnResponse<RegisterAccountViewModel>();
        }

        protected CommandBase(ReturnResponse<RegisterAccountViewModel> returnResponse)
        {
            ReturnResponse = returnResponse;
        }


        public ReturnResponse<RegisterAccountViewModel> ReturnResponse { get; }

    }
}