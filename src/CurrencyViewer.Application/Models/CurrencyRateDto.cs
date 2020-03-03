using Newtonsoft.Json;
using System.Collections.Generic;

namespace CurrencyViewer.Application.Models
{
    public class CurrencyRateDto
    {
        [JsonProperty("table")]
        public string Table { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("rates")]
        public List<Rate> Rates { get; set; }
    }

    public class Rate
    {
        [JsonProperty("no")]
        public string No { get; set; }
        [JsonProperty("effectiveDate")]
        public string EffectiveDate { get; set; }
        [JsonProperty("bid")]
        public double Bid { get; set; }
        [JsonProperty("ask")]
        public double Ask { get; set; }
    }
}
