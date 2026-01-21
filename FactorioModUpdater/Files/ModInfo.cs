using System.Text.Json.Serialization;

namespace FactorioModUpdater.Files;

public class ModInfo
{
    public string Name { get; set; }
    public string Version { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    [JsonPropertyName("factorio_version")]
    public string FactorioVersion { get; set; }
    public string[] Dependencies { get; set; }
    public string Description { get; set; }
}