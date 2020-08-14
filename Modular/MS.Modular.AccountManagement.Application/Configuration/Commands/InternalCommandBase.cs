using MS.Modular.AccountManagement.Application.Contracts;
using MS.Modular.AccountManagement.Domain.AccountManagements;
using MS.Modular.BuildingBlocks.Domain;

namespace MS.Modular.AccountManagement.Application.Configuration.Commands
{
    public abstract class InternalCommandBase : ICommand
    {
        protected InternalCommandBase(ReturnResponse<AccountDataTransformation> returnResponse)
        {
            ReturnResponse = returnResponse;
        }

        public ReturnResponse<AccountDataTransformation> ReturnResponse { get; }
    }

    public abstract class InternalCommandBase<TResult> : ICommand<TResult>
    {
        protected InternalCommandBase()
        {
            ReturnResponse = new ReturnResponse<AccountDataTransformation>();
        }

        protected InternalCommandBase(ReturnResponse<AccountDataTransformation> returnResponse)
        {
            ReturnResponse = returnResponse;
        }

        public ReturnResponse<AccountDataTransformation> ReturnResponse { get; }
    }
}