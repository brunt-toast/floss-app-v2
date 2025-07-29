using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlossApp.Application.Enums;

namespace FlossApp.Application.Tests.Generators;

internal static class ImageSharpKnownResamplersGenerator
{
    public static IEnumerable<object[]> GenerateImageSharpKnownResamplers()
    {
        return Enum.GetValues<ImageSharpKnownResamplers>().Select<ImageSharpKnownResamplers, object[]>(x => [x]);
    }
}
