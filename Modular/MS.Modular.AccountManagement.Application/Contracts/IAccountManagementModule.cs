using System.Threading.Tasks;

namespace MS.Modular.AccountManagement.Application.Contracts
{
    public interface IAccountManagementModule
    {
        Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);

        Task ExecuteCommandAsync(ICommand command);

        Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query);
    }
}