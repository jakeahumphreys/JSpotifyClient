using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using JCommon.Communication.Internal;
using JSpotifyClient.Types.Responses;

namespace JSpotifyClient;

public class SpotifyClient
{
    private string _clientId { get; set; }
    private string _clientSecret { get; set; }

    public SpotifyClient(string clientId, string clientSecret)
    {
        _clientId = clientId;
        _clientSecret = clientSecret;
    }

    public async Task<Result<AuthenticationResponse>> GetBearerToken()
    {
        var basicAuthHeaderString = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_clientId}:{_clientSecret}"));
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
}