using Microsoft.AspNetCore.Mvc;
using RoomAdvisor.Helper;
using RoomAdvisor.Models;
using RoomAdvisor.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoomAdvisor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly WeatherService _weatherService;
        private readonly GeminiRecommendationService _geminiRecommendationService;

        public RoomController(WeatherService weatherService, GeminiRecommendationService geminiRecommendationService)
        {
            _weatherService = weatherService;
            _geminiRecommendationService = geminiRecommendationService;
        }

        [HttpPost("process-rooms")]
        public async Task<IActionResult> ProcessRooms(
            [FromQuery] double latitude,
            [FromQuery] double longitude,
            [FromBody] List<Room> rooms)
        {
            try
            {
                WeatherData weatherData = await _weatherService.GetWeatherDataAsObjectAsync(latitude, longitude);

                double weatherTemperature = weatherData.Main.Temp;
                double weatherHumidity = weatherData.Main.Humidity;

                string prompt = PromptHelper.GetPromptWithData(rooms, weatherTemperature, weatherHumidity);
                
                AiRecommendationResponse aiResponse = _geminiRecommendationService.GetAiRecommendation(prompt);

                return Ok(new
                {
                    Weather = new { Temperature = latitude, Humidity = longitude },
                    Recommendations = aiResponse
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
            }
        }
    }
}
