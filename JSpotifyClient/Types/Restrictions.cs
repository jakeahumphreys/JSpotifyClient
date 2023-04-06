using System.Text.Json.Serialization;

namespace JSpotifyClient.Types;

public sealed class Restrictions
{
    [JsonPropertyName("reason")]
    public string Reason { get; set; }
}