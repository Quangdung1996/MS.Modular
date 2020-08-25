using MS.Modular.AccountManagement.Application.Contracts;
using MS.Modular.AccountManagement.Domain.Accounts;
using MS.Modular.BuildingBlocks.Domain;

namespace MS.Modular.AccountManagement.Application.Configuration.Commands
{
    public abstract class InternalCommandUpdate : ICommandUpdate
    {
        protected InternalCommandUpdate(ReturnResponse<UpdateAccount> returnResponse)
        {
            ReturnResponse = returnResponse;
        }

        public ReturnResponse<UpdateAccount> ReturnResponse { get; set; }
    }

    public abstract class InternalCommandUpdate<TResult> : ICommandUpdate<TResult>
    {
        protected InternalCommandUpdate(ReturnResponse<UpdateAccount> returnResponse)
        {
            ReturnResponse = returnResponse;
        }

        protected InternalCommandUpdate()
        {
            ReturnResponse = new ReturnResponse<UpdateAccount>();
        }

        public ReturnResponse<UpdateAccount> ReturnResponse { get; set; }
    }
}