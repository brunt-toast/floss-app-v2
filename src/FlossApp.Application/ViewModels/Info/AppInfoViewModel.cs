using System.Reflection;
using System.Runtime.InteropServices;
using FlossApp.Application.Consts;

namespace FlossApp.Application.ViewModels.Info;

public class AppInfoViewModel : IAppInfoViewModel
{
}

public interface IAppInfoViewModel
{
    public string ThirdPartyNoticesUri => $"{AppConsts.GitUri}/tree/main/docs/third-party-notices";
    public string GitRepoUri => AppConsts.GitUri;
    public string ApplicationName => AppConsts.ApplicationName;
    public string DotnetVersion => RuntimeInformation.FrameworkDescription;
    public string AppVersion => Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? "Unknown";
    public bool IsSigned => Assembly.GetEntryAssembly()?.GetName().GetPublicKey()?.Length > 0;
}
