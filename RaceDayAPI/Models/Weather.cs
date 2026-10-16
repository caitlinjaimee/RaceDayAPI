
namespace RaceDayAPI.Models
{
    public class Weather
    {
        public int WeatherID { get; set; }

        public int EventID { get; set; }

        public decimal? Temperature { get; set; }

        public string? WeatherCondition { get; set; }

        public decimal? WindSpeed { get; set; }

        public DateTime ForecastDate { get; set; }

        public Event? Event { get; set; }
    }
}


