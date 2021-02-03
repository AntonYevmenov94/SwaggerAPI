using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        [HttpGet("GetCurrentWeather")]
        public async Task<string> GetCurrentWeather()
        {
            using (var client = new HttpClient())
            {
                using (HttpRequestMessage httpRequest = new HttpRequestMessage())
                {
                    httpRequest.RequestUri = new Uri("http://api.openweathermap.org/data/2.5/weather?q=Dnipro,ua&appid=161b4603a6a9f2a0adafd39b9fa6ce98&units=metric");
                    httpRequest.Method = HttpMethod.Get;
                    HttpResponseMessage responseMessage = await client.SendAsync(httpRequest);
                    string response = await responseMessage.Content.ReadAsStringAsync();
                    WeatherTotal weatherTotal = JsonConvert.DeserializeObject<WeatherTotal>(response);
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine($"Город: {weatherTotal.Name}");
                    DateTime date = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(weatherTotal.Dt);
                    builder.AppendLine($"Дата прогноза: {date}");
                    builder.AppendLine($"Температура С: {(int)weatherTotal.main.Temp}");
                    builder.AppendLine($"Скорость ветра м/с: {weatherTotal.wind.Speed}");
                    builder.AppendLine($"Облачность %: {weatherTotal.clouds.All}");
                    return builder.ToString();
                }
            }
        }

        [HttpGet("GetForecast")]
        public async Task<string> GetForecast()
        {
            using (var client = new HttpClient())
            {
                using (HttpRequestMessage httpRequest = new HttpRequestMessage())
                {
                    httpRequest.RequestUri = new Uri("http://api.openweathermap.org/data/2.5/forecast?q=Dnipro,ua&appid=161b4603a6a9f2a0adafd39b9fa6ce98&units=metric");
                    httpRequest.Method = HttpMethod.Get;
                    HttpResponseMessage responseMessage = await client.SendAsync(httpRequest);
                    string response = await responseMessage.Content.ReadAsStringAsync();
                    WeatherForecast forecast = JsonConvert.DeserializeObject<WeatherForecast>(response);
                    return forecast.ToString();
                }
            }
        }
    }
}
