using Autofac;
using AutoMapper;

namespace MS.Modular.AccountManagement.Infrastructure.Configuration.Mapping
{
    internal class MappingProfileModule : Module
    {
        internal MappingProfileModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            var infrastructureAssembly = typeof(Profile).Assembly;

            builder.RegisterAssemblyTypes(infrastructureAssembly)
              .Where(type => type.Name.EndsWith("Profile"))
              .AsImplementedInterfaces()
              .InstancePerLifetimeScope()
              .FindConstructorsWith(new AllConstructorFinder());

            builder.Register(c => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AccountManagemenentProfile());
            })).AsSelf().AutoActivate().SingleInstance();

            builder.Register(c =>
            {
                var scope = c.Resolve<ILifetimeScope>();
                return new Mapper(c.Resolve<MapperConfiguration>(), scope.Resolve);
            }).As<IMapper>().SingleInstance();
        }
    }
}