using Autofac;
using MS.Modular.AccountManagement.Application.Contracts;
using MS.Modular.AccountManagement.Infrastructure;

namespace MS.Modular.Modules.AccountManagement
{
    public class AccountManagementAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountManagementModule>()
                .As<IAccountManagementModule>()
                .InstancePerLifetimeScope();
        }
    }
}