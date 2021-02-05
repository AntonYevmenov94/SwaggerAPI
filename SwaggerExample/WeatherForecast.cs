using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerExample
{
    public class WeatherForecast
    {
        public City city { get; set; }
        public IList<ListWeather> List { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine($"Город: {city.Name}");
            foreach (var item in List)
            {
                builder.AppendLine($"Дата прогноза: {item.Dt_txt}");
                builder.AppendLine("{");
                builder.AppendLine($"\tТемпература С: {(int)item.main.Temp}");
                builder.AppendLine($"\tСкорость ветра м/с: {item.wind.Speed}");
                builder.AppendLine($"\tОблачность %: {item.clouds.All}");
                builder.AppendLine("}");
            }
            return builder.ToString();
        }
    }

    public class City
    {
        public string Name { get; set; }
        public string Country { get; set; }
    }

    public class ListWeather
    {
        public Main main { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public string Dt_txt { get; set; }
    }
}
