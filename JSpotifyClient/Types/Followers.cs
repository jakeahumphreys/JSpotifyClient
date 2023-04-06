using System.Text.Json.Serialization;

namespace JSpotifyClient.Types;

public sealed class Followers
{
    [JsonPropertyName("href")]
    public string Href { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }
}