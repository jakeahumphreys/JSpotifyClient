using System.Text.Json.Serialization;

namespace JSpotifyClient.Types;

public sealed class Image
{
    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("height")]
    [JsonNumberHandling(JsonNumberHandling.WriteAsString)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int? Height { get; set; }

    [JsonPropertyName("width")]
    [JsonNumberHandling(JsonNumberHandling.WriteAsString)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public int? Width { get; set; }
}