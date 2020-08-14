using Autofac;
using MediatR;
using MS.Modular.AccountManagement.Application.Contracts;
using MS.Modular.AccountManagement.Infrastructure.Configuration;
using MS.Modular.AccountManagement.Infrastructure.Configuration.Processing;
using System.Threading.Tasks;

namespace MS.Modular.AccountManagement.Infrastructure
{
    public class AccountManagementModule : IAccountManagementModule
    {
        public async Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command)
        {
            return await CommandsExecutor.ExecuteAsync(command);
        }

        public async Task ExecuteCommandAsync(ICommand command)
        {
            await CommandsExecutor.ExecuteAsync(command);
        }

        public async Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
        {
            using (var scope = AdministrationCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();

                return await mediator.Send(query);
            }
        }
    }
}