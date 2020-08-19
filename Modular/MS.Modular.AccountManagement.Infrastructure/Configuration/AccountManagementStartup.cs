using Autofac;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using MS.Modular.AccountManagement.Domain.Dto;
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
        
        public static void Initialize(string connectionString, IOptions<JwtOptions> options, IDistributedCache distributedCache, ILogger logger)
        {
            var moduleLogger = logger.ForContext("Module", "AccountManagement");

            ConfigureContainer(connectionString, options, distributedCache, moduleLogger);
        }

        private static void ConfigureContainer(string connectionString, IOptions<JwtOptions> options, IDistributedCache distributedCache, ILogger logger)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new LoggingModule(logger));
            var loggerFactory = new SerilogLoggerFactory(logger);

            containerBuilder.RegisterModule(new DataAccessModule(connectionString, options, distributedCache, loggerFactory));

            containerBuilder.RegisterModule(new MediatorModule());

            containerBuilder.RegisterModule(new MappingProfileModule());

            _container = containerBuilder.Build();

            AdministrationCompositionRoot.SetContainer(_container);
        }
    }
}