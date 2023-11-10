using Microsoft.Extensions.DependencyInjection;

namespace Untitled.Helper;

public static class ServiceProviderHelper
{
    public static T GetService<T>(IServiceProvider serviceProvider)
    {
        return serviceProvider.GetService<T>() ?? throw new NullReferenceException();
    }
}