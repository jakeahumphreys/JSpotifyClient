using System.Text.Json.Serialization;

namespace JSpotifyClient.Types.Responses;

public sealed class AuthenticationResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }
    [JsonPropertyName("error")]
    public string Error { get; set; }
}