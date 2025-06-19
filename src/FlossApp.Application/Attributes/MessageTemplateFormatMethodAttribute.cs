using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlossApp.Application.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public sealed class MessageTemplateFormatMethodAttribute : Attribute
{
    public string FormatParameterName { get; }

    public MessageTemplateFormatMethodAttribute(string formatParameterName)
    {
        FormatParameterName = formatParameterName;
    }
}
