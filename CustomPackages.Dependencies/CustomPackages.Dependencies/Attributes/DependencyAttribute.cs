using Microsoft.Extensions.DependencyInjection;

namespace CustomPackages.Dependencies.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class DependencyAttribute : Attribute
{
    public Type[] Exposes { get; init; } = Array.Empty<Type>();

    public ServiceLifetime Lifetime { get; init; } = ServiceLifetime.Transient;
}
