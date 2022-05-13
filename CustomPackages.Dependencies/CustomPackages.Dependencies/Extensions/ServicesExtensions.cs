using System.Reflection;
using CustomPackages.Dependencies.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace CustomPackages.Dependencies.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, Assembly assembly)
    {
        var types = assembly.GetTypes()
            .Select(x => new
            {
                Type = x,
                DependencyAttribute = x.GetCustomAttribute<DependencyAttribute>()
            })
            .Where(x => x.DependencyAttribute is not null);

        foreach (var dependency in types)
        {
            if (dependency.DependencyAttribute!.Exposes.Any())
            {
                var exposeAs = dependency.DependencyAttribute.Exposes.ToList();
                var mainType = exposeAs.First();
                exposeAs.Remove(mainType);
                services.Add(new ServiceDescriptor(
                    mainType,
                    dependency.Type,
                    dependency.DependencyAttribute!.Lifetime
                ));

                foreach (var type in exposeAs)
                {
                    services.Add(new ServiceDescriptor(
                        type,
                        provider => provider.GetRequiredService(mainType),
                        dependency.DependencyAttribute.Lifetime
                    ));
                }
            }
            else
            {
                services.Add(new ServiceDescriptor(
                    dependency.Type,
                    dependency.Type,
                    dependency.DependencyAttribute!.Lifetime
                ));
            }
        }

        return services;
    }
}
