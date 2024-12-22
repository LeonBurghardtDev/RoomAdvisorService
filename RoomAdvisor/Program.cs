using RoomAdvisor.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        ConfigurationManager configuration = builder.Configuration;
        configuration.AddEnvironmentVariables();

        // Retrieve API keys from environment variables
        string? geminiApiKey = configuration["GEMINI_API_KEY"];
        string? openWeatherApiKey = configuration["OPEN_WEATHER_API_KEY"];

        // Ensure API keys are not null
        if (string.IsNullOrEmpty(geminiApiKey) || string.IsNullOrEmpty(openWeatherApiKey))
        {
            throw new InvalidOperationException("API keys are missing. Please set GEMINI_API_KEY and OPEN_WEATHER_API_KEY environment variables.");
        }

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Add your services and pass API keys as needed
        builder.Services.AddSingleton(new WeatherService(openWeatherApiKey));
        builder.Services.AddSingleton(new GeminiRecommendationService(geminiApiKey));

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}