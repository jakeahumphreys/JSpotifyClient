using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using JCommon.Communication.Internal;
using JSpotifyClient.Types.Responses;

namespace JSpotifyClient;

public interface ISpotifyClient
{
    public Task<Result<GetPlaylistsForUserIdResponse>> GetPlaylistsForUserId(string userId);
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

    public Task<Result<GetPlaylistsForUserIdResponse>> GetPlaylistsForUserId(string userId)
    {
        throw new NotImplementedException();
    }
}