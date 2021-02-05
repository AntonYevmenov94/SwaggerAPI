using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public async Task<IActionResult> GetCurrentWeather(string city)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    using (HttpRequestMessage httpRequest = new HttpRequestMessage())
                    {
                        httpRequest.RequestUri = new Uri($"http://api.openweathermap.org/data/2.5/weather?q={city},ua&appid=161b4603a6a9f2a0adafd39b9fa6ce98&units=metric");
                        httpRequest.Method = HttpMethod.Get;
                        HttpResponseMessage responseMessage = await client.SendAsync(httpRequest);
                        if (responseMessage.StatusCode == HttpStatusCode.OK)
                        {
                            string response = await responseMessage.Content.ReadAsStringAsync();
                            WeatherTotal weatherTotal = JsonConvert.DeserializeObject<WeatherTotal>(response);
                            DateTime date = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(weatherTotal.Dt);

                            //StringBuilder builder = new StringBuilder();
                            //builder.AppendLine($"Город: {weatherTotal.Name}");
                            //builder.AppendLine($"Дата прогноза: {date}");
                            //builder.AppendLine($"Температура С: {(int)weatherTotal.main.Temp}");
                            //builder.AppendLine($"Скорость ветра м/с: {weatherTotal.wind.Speed}");
                            //builder.AppendLine($"Облачность %: {weatherTotal.clouds.All}");

                            return Ok(new
                            {
                                city = weatherTotal.Name,
                                date = date.ToString("yyyy.MM.dd HH:mm:ss"),
                                temp = weatherTotal.main.Temp,
                                wind = weatherTotal.wind.Speed,
                                clouds = weatherTotal.clouds.All
                            });
                        }
                        else
                        {
                            return BadRequest($"Error: {responseMessage.StatusCode}");
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    return BadRequest($"Error: {ex.Message}");
                }
            }
        }

        [HttpGet("GetForecast")]
        public async Task<IActionResult> GetForecast(string city)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    using (HttpRequestMessage httpRequest = new HttpRequestMessage())
                    {
                        httpRequest.RequestUri = new Uri($"http://api.openweathermap.org/data/2.5/forecast?q={city},ua&appid=161b4603a6a9f2a0adafd39b9fa6ce98&units=metric");
                        httpRequest.Method = HttpMethod.Get;
                        HttpResponseMessage responseMessage = await client.SendAsync(httpRequest);
                        if (responseMessage.StatusCode == HttpStatusCode.OK)
                        {
                            string response = await responseMessage.Content.ReadAsStringAsync();
                            WeatherForecast forecast = JsonConvert.DeserializeObject<WeatherForecast>(response);
                            return Ok(new
                            {
                                city = forecast.city.Name,
                                list = forecast.List
                            });
                        }
                        else
                        {
                            return BadRequest($"Error: {responseMessage.StatusCode}");
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    return BadRequest($"Error: {ex.Message}");
                }
            }
        }
    }
}
