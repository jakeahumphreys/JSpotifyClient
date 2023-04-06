using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using JCommon.Communication.Internal;
using JSpotifyClient.Types.Responses;

namespace JSpotifyClient;

public interface ISpotifyClient
{
    public Task<Result<GetPlaylistsForUserIdResponse>> GetPlaylistsForUserId(string userId);
    public Task<Result<GetPlaylistTracksResponse>> GetTracksForPlaylist(string playlistId);
}

public class SpotifyClient : ISpotifyClient
{
    private string ClientId { get; set; }
    private string ClientSecret { get; set; }

    public SpotifyClient(string clientId, string clientSecret)
    {
        ClientId = clientId;
        ClientSecret = clientSecret;
    }
    
    private async Task<Result<AuthenticationResponse>> GetBearerToken()
    {
        var basicAuthHeaderString = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ClientId}:{ClientSecret}"));
        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", basicAuthHeaderString);

            var requestForm = new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials"
            };

            var response = await httpClient.PostAsync("https://accounts.spotify.com/api/token", new FormUrlEncodedContent(requestForm));

            if (!response.IsSuccessStatusCode)
                return new Result<AuthenticationResponse>().WithError(response.ReasonPhrase ?? "An authentication error occurred.");

            var jsonString = await response.Content.ReadAsStreamAsync();
            var responseData = JsonSerializer.Deserialize<AuthenticationResponse>(jsonString);

            if (responseData == null)
                return new Result<AuthenticationResponse>().WithError("Unable to serialize the response from the spotify API.");

            if (!string.IsNullOrWhiteSpace(responseData.Error))
                return new Result<AuthenticationResponse>().WithError(responseData.Error);

            return new Result<AuthenticationResponse>(responseData);
        }
    }

    public async Task<Result<GetPlaylistsForUserIdResponse>> GetPlaylistsForUserId(string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            return new Result<GetPlaylistsForUserIdResponse>().WithError("A User ID was not provided.");

        var bearerTokenResult = GetBearerToken().Result;

        if (bearerTokenResult.IsFailure)
            return new Result<GetPlaylistsForUserIdResponse>().WithError(bearerTokenResult.Errors.First().Message);

        using (var httpClient = new HttpClient())
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerTokenResult.Content.AccessToken);

            var response = await httpClient.GetAsync($"https://api.spotify.com/v1/users/{userId}/playlists");

            var jsonString = await response.Content.ReadAsStringAsync();
            var responseData = JsonSerializer.Deserialize<GetPlaylistsForUserIdResponse>(jsonString);

            if (responseData == null)
                return new Result<GetPlaylistsForUserIdResponse>().WithError("Unable to deserialize the response from the spotify API");

            return new Result<GetPlaylistsForUserIdResponse>(responseData);
        }
    }

    public Task<Result<GetPlaylistTracksResponse>> GetTracksForPlaylist(string playlistId)
    {
        throw new NotImplementedException();
    }
}