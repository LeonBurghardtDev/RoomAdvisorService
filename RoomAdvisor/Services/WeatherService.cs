using Newtonsoft.Json;
using RoomAdvisor.Models;

public class WeatherService
{
    private readonly string _apiKey;
    private readonly HttpClient _httpClient;

    public WeatherService(string apiKey)
    {
        _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
        _httpClient = new HttpClient();
    }

    private async Task<string> FetchWeatherDataAsync(double latitude, double longitude)
    {
        string url = $"https://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid={_apiKey}&units=metric";

        HttpResponseMessage response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Failed to fetch weather data: {response.ReasonPhrase}");
        }

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<WeatherData> GetWeatherDataAsObjectAsync(double latitude, double longitude)
    {
        string rawJson = await FetchWeatherDataAsync(latitude, longitude);

        return JsonConvert.DeserializeObject<WeatherData>(rawJson);
    }

    public async Task<string> GetWeatherDataAsStringAsync(double latitude, double longitude)
    {
        return await FetchWeatherDataAsync(latitude, longitude);
    }

    
}