using FlossApp.Application.Enums;
using Microsoft.AspNetCore.Components;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace FlossApp.Wasm.Components;

public partial class EyedropperComponent
{
    [Parameter]
    public Image<Rgba32> Image
    {
        get;
        set
        {
            field = value;
            _grid = CalculateGrid(value);
        }
    } = new(1, 1);

    [Parameter]
    public ColorSchema Schema
    {
        get => ViewModel.TargetSchema;
        set
        {
            if (EqualityComparer<ColorSchema>.Default.Equals(ViewModel.TargetSchema, value))
            {
                return;
            }

            ViewModel.TargetSchema = value;
            SchemaChanged.InvokeAsync(value);
        }
    }

    [Parameter] public EventCallback<ColorSchema> SchemaChanged { get; set; }

    [Parameter]
    public ColorComparisonAlgorithms ComparisonAlgorithm
    {
        get => ViewModel.ComparisonAlgorithm;
        set
        {
            if (EqualityComparer<ColorComparisonAlgorithms>.Default.Equals(ViewModel.ComparisonAlgorithm, value))
            {
                return;
            }

            ViewModel.ComparisonAlgorithm = value;
            ComparisonAlgorithmChanged.InvokeAsync(value);
        }
    }

    [Parameter] public EventCallback<ColorComparisonAlgorithms> ComparisonAlgorithmChanged { get; set; }
    private List<List<System.Drawing.Color>> _grid = [];

    private List<List<System.Drawing.Color>> CalculateGrid(Image<Rgba32> image)
    {
        Logger.LogInformation("Start");
        List<List<System.Drawing.Color>> ret = [];

        image.ProcessPixelRows(accessor =>
        {
            for (int y = 0; y < accessor.Height; y++)
            {
                var thisRow = new List<System.Drawing.Color>();
                ret.Add(thisRow);

                var rowSpan = accessor.GetRowSpan(y);
                for (int x = 0; x < rowSpan.Length; x++)
                {
                    Logger.LogInformation($"{x}x{y}");
                    var color = System.Drawing.Color.FromArgb(rowSpan[x].A, rowSpan[x].R, rowSpan[x].G, rowSpan[x].B);
                    thisRow.Add(color);
                }
            }
        });

        return ret;
    }
}
