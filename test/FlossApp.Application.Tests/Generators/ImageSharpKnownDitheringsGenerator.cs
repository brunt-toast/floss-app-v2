using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlossApp.Application.Enums;

namespace FlossApp.Application.Tests.Generators;

internal static class ImageSharpKnownDitheringsGenerator
{
    public static IEnumerable<object[]> GenerateImageSharpKnownDitherings()
    {
        return Enum.GetValues<ImageSharpKnownDitherings>().Select<ImageSharpKnownDitherings, object[]>(x => [x]);
    }
}
