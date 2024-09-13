using System.Reflection;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Castle.DynamicProxy;
using Core.Entities.Abstract;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using Module = Autofac.Module;

namespace Business.DependencyResolvers.Autofac;

public class AutofacBusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var businessAssembly = Assembly.GetAssembly(typeof(IBusinessService));
        var dataAccessAssembly = Assembly.GetAssembly(typeof(IDataAccessService));
        var coreAssembly = Assembly.GetAssembly(typeof(ICoreService));

        RegisterServices(builder, businessAssembly);
        RegisterServices(builder, dataAccessAssembly);
        RegisterServices(builder, coreAssembly);

        builder.RegisterAssemblyTypes(businessAssembly, dataAccessAssembly, coreAssembly)
            .AsImplementedInterfaces()
            .EnableInterfaceInterceptors(new ProxyGenerationOptions { Selector = new AspectInterceptorSelector() })
            .SingleInstance();
    }

    private void RegisterServices(ContainerBuilder builder, Assembly assembly)
    {
        var serviceTypes = assembly.GetTypes()
            .Where(type => type.IsInterface && !type.IsGenericTypeDefinition);

        foreach (var serviceType in serviceTypes)
        {
            var implementingTypes = assembly.GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && serviceType.IsAssignableFrom(type));

            foreach (var implementingType in implementingTypes)
                if (!implementingType.IsGenericTypeDefinition)
                    builder.RegisterType(implementingType).As(serviceType);
        }
    }
}