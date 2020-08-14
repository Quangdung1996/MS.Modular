using Autofac;
using MediatR;
using MS.Modular.AccountManagement.Application.Contracts;
using System.Threading.Tasks;

namespace MS.Modular.AccountManagement.Infrastructure.Configuration.Processing
{
    internal static class CommandsExecutor
    {
        internal static async Task ExecuteAsync(ICommand command)
        {
            using (var scope = AdministrationCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                await mediator.Send(command);
            }
        }

        internal static async Task<TResult> ExecuteAsync<TResult>(ICommand<TResult> command)
        {
            using (var scope = AdministrationCompositionRoot.BeginLifetimeScope())
            {
                var mediator = scope.Resolve<IMediator>();
                return await mediator.Send(command);
            }
        }
    }
}