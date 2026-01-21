using System.Text.Json.Serialization;
using FactorioModUpdater.Misc;

namespace FactorioModUpdater.Files;

public class ReleaseInfo
{
    [JsonPropertyName("download_url")]
    public string DownloadUrl  { get; set; }
    [JsonPropertyName("file_name")]
    public string FileName { get; set; }
    [JsonPropertyName("info_json")]
    public ModInfo InfoJson { get; set; }
    [JsonPropertyName("released_at")]
    [JsonConverter(typeof(IsoDateTimeConverter))]
    public DateTime ReleaseDate { get; set; }
    [JsonPropertyName("version")]
    public string Version { get; set; }
    [JsonPropertyName("sha1")]
    public string ShaKey { get; set; }
    [JsonPropertyName("feature_flags")]
    public string[] FeatureFlags { get; set; }
}