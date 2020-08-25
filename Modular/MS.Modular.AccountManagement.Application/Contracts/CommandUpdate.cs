using MS.Modular.AccountManagement.Domain.Accounts;
using MS.Modular.BuildingBlocks.Domain;

namespace MS.Modular.AccountManagement.Application.Contracts
{
    public abstract class CommandUpdate<TResult> : ICommandUpdate<TResult>
    {
        protected CommandUpdate()
        {
            this.ReturnResponse = new ReturnResponse<UpdateAccount>();
        }

        protected CommandUpdate(ReturnResponse<UpdateAccount> returnResponse)
        {
            ReturnResponse = returnResponse;
        }

        public ReturnResponse<UpdateAccount> ReturnResponse { get; }
    }
}