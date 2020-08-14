using MediatR;

namespace MS.Modular.AccountManagement.Application.Contracts
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}