using MediatR;
using MS.Modular.AccountManagement.Domain.Accounts;
using MS.Modular.BuildingBlocks.Domain;

namespace MS.Modular.AccountManagement.Application.Contracts
{
    public interface ICommandUpdate<out TResult> : IRequest<TResult>
    {
        ReturnResponse<UpdateAccount> ReturnResponse { get; }
    }

    public interface ICommandUpdate : IRequest<Unit>
    {
        ReturnResponse<UpdateAccount> ReturnResponse { get; }
    }
}