using System.Drawing;

namespace FlossApp.Application.Messages;

internal class ColorSelectedMessage
{
    public Color Color { get; }

    public ColorSelectedMessage(System.Drawing.Color color)
    {
        Color = color;
    }
}
