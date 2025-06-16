using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlossApp.Application.Utils;
using Newtonsoft.Json;

namespace FlossApp.Application.Data;

public struct DmcColor
{
    [JsonProperty("r")] public byte Red { get; set; }
    [JsonProperty("g")] public byte Green { get; set; }
    [JsonProperty("b")] public byte Blue { get; set; }
    [JsonProperty("description")] public string Name { get; set; } 
    [JsonProperty("floss")] public string Number { get; set; } 

    public static async Task<IEnumerable<DmcColor>> GetAllAsync()
    {
        string json = await AsyncEmbeddedResourceReader.ReadEmbeddedResourceAsync(typeof(DmcColor).Assembly, "Dmc.json");
        return JsonConvert.DeserializeObject<DmcColor[]>(json) ?? [];
    }
}
