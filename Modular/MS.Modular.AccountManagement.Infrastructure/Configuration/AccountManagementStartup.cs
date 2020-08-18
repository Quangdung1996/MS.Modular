using Autofac;
using MS.Modular.AccountManagement.Infrastructure.Configuration.DataAccess;
using MS.Modular.AccountManagement.Infrastructure.Configuration.Logging;
using MS.Modular.AccountManagement.Infrastructure.Configuration.Mapping;
using MS.Modular.AccountManagement.Infrastructure.Configuration.Mediation;
using Serilog;
using Serilog.AspNetCore;

namespace MS.Modular.AccountManagement.Infrastructure.Configuration
{
    public class AccountManagementStartup
    {
        private static IContainer _container;

        public static void Initialize(string connectionString, ILogger logger)
        {
            var moduleLogger = logger.ForContext("Module", "AccountManagement");

            ConfigureContainer(connectionString, moduleLogger);
        }

        private static void ConfigureContainer(string connectionString, ILogger logger)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new LoggingModule(logger));
            var loggerFactory = new SerilogLoggerFactory(logger);

            containerBuilder.RegisterModule(new DataAccessModule(connectionString, loggerFactory));

            containerBuilder.RegisterModule(new MediatorModule());

            containerBuilder.RegisterModule(new MappingProfileModule());

            _container = containerBuilder.Build();

            AdministrationCompositionRoot.SetContainer(_container);
        }
    }
}