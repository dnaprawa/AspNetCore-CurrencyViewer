using System.Collections.Generic;

namespace CurrencyViewer.Application.Models
{
    public class CurrencyRatesConfig
    {
        public const string JsonPropertyName = "CurrencyRatesConfig";
        public string BaseUrl { get; set; }
        public IEnumerable<string> CurrencyCodes { get; set; }
    }
}
