using Autofac;
using PwnedPass2.Interfaces;
using PwnedPass2.Modules;
using PwnedPass2.Services;

namespace PwnedPass2
{
    public class AppSetup
    {
        public IContainer CreateContainer()
        {
            var containerBuilder = new ContainerBuilder();
            RegisterDependencies(containerBuilder);
            return containerBuilder.Build();
        }

        protected virtual void RegisterDependencies(ContainerBuilder cb)
        {
            cb.RegisterModule(new ConfigurationModule());
        }
    }
}