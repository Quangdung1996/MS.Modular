using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using MS.Modular.AccountManagement.Domain.AccountManagements;
using MS.Modular.AccountManagement.Infrastructure.AccountManagements;
using MS.Modular.BuildingBlocks.Application.Data;
using MS.Modular.BuildingBlocks.Infrastustructure;

namespace MS.Modular.AccountManagement.Infrastructure.Configuration.DataAccess
{
    internal class DataAccessModule : Autofac.Module
    {
        private readonly string _databaseConnectionString;
        private readonly ILoggerFactory _loggerFactory;

        internal DataAccessModule(string databaseConnectionString, ILoggerFactory loggerFactory)
        {
            _databaseConnectionString = databaseConnectionString;
            _loggerFactory = loggerFactory;
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
            builder.RegisterType<AccountManagementService>().As<IAccountManagementService>();
        }
    }
}