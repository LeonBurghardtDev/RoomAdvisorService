using Microsoft.AspNetCore.Mvc;
using RoomAdvisor.Services;
using System.Threading.Tasks;

namespace RoomAdvisor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherService _weatherService;

        public WeatherController(WeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentWeather([FromQuery] double latitude, [FromQuery] double longitude)
        {
            try
            {
                Models.WeatherData weatherData = await _weatherService.GetWeatherDataAsObjectAsync(latitude, longitude);

                return Ok(weatherData);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
            }
        }
    }
}
