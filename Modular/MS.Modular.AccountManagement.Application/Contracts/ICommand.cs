using MediatR;
using MS.Modular.AccountManagement.Domain.Accounts;
using MS.Modular.AccountManagement.Domain.ViewModels;
using MS.Modular.BuildingBlocks.Domain;

namespace MS.Modular.AccountManagement.Application.Contracts
{
    public interface ICommand<out TResult> : IRequest<TResult>
    {
        ReturnResponse<RegisterAccountViewModel> ReturnResponse { get; }
    }
    public interface ICommand : IRequest<Unit>
    {
        ReturnResponse<RegisterAccountViewModel> ReturnResponse { get; }
    }
}