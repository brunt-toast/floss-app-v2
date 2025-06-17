using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FlossApp.Application.Metrics;

public static class MethodTimeLogger
{
    // ReSharper disable once UnusedMember.Global
    public static void Log(MethodBase methodBase, long elapsedMs, string message)
    {
        Console.WriteLine($"{methodBase.DeclaringType?.Name}.{methodBase.Name}: {elapsedMs}ms {message}");
    }
}
