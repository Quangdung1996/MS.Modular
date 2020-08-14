using Autofac;
using MS.Modular.AccountManagement.Application.Contracts;
using MS.Modular.AccountManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS.Modular.Modules.AccountManagement
{
    public class AccountManagementAutofacModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountManagementModule>()
                .As<IAccountManagementModule>()
                .InstancePerLifetimeScope();
        }
    }
}
