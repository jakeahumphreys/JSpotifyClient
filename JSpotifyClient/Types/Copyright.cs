using System.Text.Json.Serialization;

namespace JSpotifyClient.Types;

public sealed class Copyright
{
    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}