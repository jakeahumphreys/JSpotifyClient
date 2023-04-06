using System.Text.Json.Serialization;

namespace JSpotifyClient.Types;

public sealed class TrackItem
{
    [JsonPropertyName("added_at")]
    public string AddedAt { get; set; }

    [JsonPropertyName("added_by")]
    public AddedBy AddedBy { get; set; }

    [JsonPropertyName("is_local")]
    public bool IsLocal { get; set; }

    [JsonPropertyName("track")]
    public Track Track { get; set; }
}