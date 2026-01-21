using System.Text.Json.Serialization;
using FactorioModUpdater.Misc;

namespace FactorioModUpdater.Files;

public class ModApiInfo
{
    [JsonPropertyName("latest_release")]
    public ReleaseInfo LatestRelease { get; set; }
    [JsonPropertyName("downloads_count")]
    public int DownloadsCount { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("owner")]
    public string Owner { get; set; }
    [JsonPropertyName("releases")]
    public ReleaseInfo[] Releases { get; set; }
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("category")]
    public string Category { get; set; }
    [JsonPropertyName("score")]
    public double Score { get; set; }
    [JsonPropertyName("thumbnail")]
    public string ThumbnailUrl { get; set; }
    [JsonPropertyName("changelog")]
    public string Changelog { get; set; }
    [JsonPropertyName("create_at")]
    [JsonConverter(typeof(IsoDateTimeConverter))]
    public DateTime CreationDate { get; set; }
    [JsonPropertyName("updated_at")]
    [JsonConverter(typeof(IsoDateTimeConverter))]
    public DateTime UpdateDate { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("source_url")]
    public string SourceUrl { get; set; }
}