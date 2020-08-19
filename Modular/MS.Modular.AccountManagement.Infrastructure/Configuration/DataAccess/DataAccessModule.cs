using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MS.Modular.AccountManagement.Domain;
using MS.Modular.AccountManagement.Domain.AccountManagements;
using MS.Modular.AccountManagement.Domain.Dto;
using MS.Modular.AccountManagement.Domain.Redis;
using MS.Modular.AccountManagement.Infrastructure.AccountManagements;
using MS.Modular.AccountManagement.Infrastructure.Domain.Redis;
using MS.Modular.AccountManagement.Infrastructure.Domain.Token;
using MS.Modular.BuildingBlocks.Application.Data;
using MS.Modular.BuildingBlocks.Infrastustructure;

namespace MS.Modular.AccountManagement.Infrastructure.Configuration.DataAccess
{
    internal class DataAccessModule : Module
    {
        private readonly string _databaseConnectionString;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IOptions<JwtOptions> _options;
        private readonly IDistributedCache _distributedCache;

        internal DataAccessModule(string databaseConnectionString, IOptions<JwtOptions> options, IDistributedCache distributedCache, ILoggerFactory loggerFactory)

        {
            _databaseConnectionString = databaseConnectionString;
            _loggerFactory = loggerFactory;
            _options = options;
            _distributedCache = distributedCache;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlConnectionFactory>()
                .As<ISqlConnectionFactory>()
                .WithParameter("connectionString", _databaseConnectionString)
                .InstancePerLifetimeScope();

            builder.Register(c =>
            {
                var dbContextOptionsBuilder = new DbContextOptionsBuilder<AccountManagementContext>();
                dbContextOptionsBuilder.UseSqlServer(_databaseConnectionString);

                dbContextOptionsBuilder
                    .ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>();

                return new AccountManagementContext(dbContextOptionsBuilder.Options, _loggerFactory);
            }).AsSelf().As<DbContext>().InstancePerLifetimeScope();

            var infrastructureAssembly = typeof(AccountManagementContext).Assembly;

            builder.RegisterAssemblyTypes(infrastructureAssembly)
                .Where(type => type.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .FindConstructorsWith(new AllConstructorFinder());

            builder.RegisterType<RedisService>().AsSelf().As<IRedisService>().InstancePerLifetimeScope();

            builder.Register(cfg =>
            {
                return new TokenService(_options, cfg.Resolve<IRedisService>());
            }).AsSelf().As<ITokenService>().InstancePerLifetimeScope();

            builder.Register(cfg =>
            {
                return new RedisService(_distributedCache);
            }).AsSelf().As<IRedisService>().InstancePerLifetimeScope();

            builder.RegisterType<AccountManagementService>().AsSelf().As<IAccountManagementService>().InstancePerLifetimeScope();
        }
    }
}