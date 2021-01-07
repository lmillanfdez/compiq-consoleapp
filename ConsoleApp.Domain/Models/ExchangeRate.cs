using System;

using Newtonsoft.Json;

namespace ConsoleApp.Domain.Models
{
    public class ExchangeRate
    {
        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }
        [JsonProperty(PropertyName = "rate")]
        public double Rate { get; set; }
        [JsonProperty(PropertyName = "whenObtained")]
        public DateTime WhenObtained { get; set; }
    }
}
