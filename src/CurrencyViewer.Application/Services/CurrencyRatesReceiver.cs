using CurrencyViewer.Application.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CurrencyViewer.Application
{
    public class CurrencyRatesReceiver : ICurrencyRatesReceiver
    {
        private readonly CurrencyRatesConfig _config;
        private readonly IHttpClientFactory _clientFactory;
        public CurrencyRatesReceiver(IOptions<CurrencyRatesConfig> options, IHttpClientFactory clientFactory)
        {
            _config = options.Value;
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<CurrencyRateDto>> GetCurrencyRatesAsync(DateTime date)
        {
            if(_config == null || string.IsNullOrWhiteSpace(_config?.BaseUrl) || !_config.CurrencyCodes.Any())
            {
                throw new InvalidOperationException("Invalid configuration");
            }

            var data = new List<CurrencyRateDto>();
            foreach (var item in _config.CurrencyCodes)
            {
                var currencyRate = await GetSingleCurrencyRateAsync(item, date);
                
                data.Add(currencyRate);
            }

            return data;
        }

        private async Task<CurrencyRateDto> GetSingleCurrencyRateAsync(string currencyCode, DateTime date)
        {
            var url = PrepareUrl(currencyCode, date);

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            CurrencyRateDto currencyRate = null;
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                currencyRate = await JsonSerializer.DeserializeAsync<CurrencyRateDto>(responseStream);
            }

            return currencyRate;
        }

        private string PrepareUrl(string currencyCode, DateTime date)
        {
            var sb = new StringBuilder();

            sb.Append(_config.BaseUrl);
            sb.Append("/api/exchangerates/rates/c");
            sb.Append($"/{currencyCode}");
            sb.Append($"/{date.ToShortDateString()}");
            sb.Append($"/format = json");

            return sb.ToString();
        }
    }
}
