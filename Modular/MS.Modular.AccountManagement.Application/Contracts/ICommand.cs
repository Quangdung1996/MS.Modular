using MediatR;
using MS.Modular.AccountManagement.Application.Accounts;
using MS.Modular.AccountManagement.Domain.AccountManagements;
using MS.Modular.AccountManagement.Domain.Accounts;
using MS.Modular.BuildingBlocks.Domain;
using System;

namespace MS.Modular.AccountManagement.Application.Contracts
{
    public interface ICommand<out TResult> : IRequest<TResult>
    {
        ReturnResponse<AccountDataTransformation> ReturnResponse { get; }
    }

    public interface ICommand : IRequest<Unit>
    {
        ReturnResponse<AccountDataTransformation> ReturnResponse { get; }
    }
}