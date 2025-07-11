using FlossApp.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FlossApp.Application.Tests.Services;

[TestClass]
public class ServiceHelperTests
{
    [TestMethod]
    public void DeclaredServicesCanBeResolved()
    {
        var serviceCollection = ServiceHelper.GetInternalServiceDescriptors();
        var serviceProvider = serviceCollection.BuildServiceProvider();

        foreach (ServiceDescriptor descriptor in serviceCollection)
        {
            _ = serviceProvider.GetRequiredService(descriptor.ServiceType);
        }
    }
}
