using FlossApp.Application.Mock;
using FlossApp.Application.Services.I18n;

namespace FlossApp.Application.Tests.Tests.Services.I18n;

[TestClass]
public class I18nServiceTests
{
    private II18nService GetService()
    {
        return new I18nService(new MockServiceProvider()) { AnchorType = typeof(I18nServiceTestAnchor) };
    }

    [TestMethod]
    public void GetResource_Finds_FullyQualified()
    {
        var service = GetService();
        Assert.AreEqual("world", service.GetResource("I18nServiceTests.hello"));
    }

    [TestMethod]
    public void GetResource_Finds_ImplicitlyQualified()
    {
        var service = GetService();
        Assert.AreEqual("world", service.GetResource("hello"));
    }

    [TestMethod]
    public void GetResources_Finds_FullyQualified()
    {
        var service = GetService();
        Dictionary<string, object> resources = service.GetResources("I18nServiceTests.group");
        Assert.IsTrue(resources.Any(x => x.Key == "a" && x.Value.ToString() == "b"));
        Assert.IsTrue(resources.Any(x => x.Key == "c" && x.Value.ToString() == "d"));
    }

    [TestMethod]
    public void GetResources_Finds_ImplicitlyQualified()
    {
        var service = GetService();
        Dictionary<string, object> resources = service.GetResources("group");
        Assert.IsTrue(resources.Any(x => x.Key == "a" && x.Value.ToString() == "b"));
        Assert.IsTrue(resources.Any(x => x.Key == "c" && x.Value.ToString() == "d"));
    }

    [TestMethod]
    public void GetResource_Finds_CaseAgnostic()
    {
        var service = GetService();
        var res = service.GetResource("i18nservicetests.hello");
        Assert.AreEqual("world", res);
    }

    [TestMethod]
    public void GetResources_Finds_CaseAgnostic()
    {
        var service = GetService();
        Dictionary<string, object> resources = service.GetResources("i18nservicetests.GROUP");
        Assert.IsTrue(resources.Any(x => x.Key == "a" && x.Value.ToString() == "b"));
        Assert.IsTrue(resources.Any(x => x.Key == "c" && x.Value.ToString() == "d"));
    }
}
