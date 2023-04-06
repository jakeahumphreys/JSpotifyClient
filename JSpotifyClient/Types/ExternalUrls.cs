using System.Text.Json.Serialization;

namespace JSpotifyClient.Types;

public sealed class ExternalUrls
{
    [JsonPropertyName("spotify")]
    public string Spotify { get; set; }
}