using CurrencyViewer.Application.Exceptions;
using CurrencyViewer.Application.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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
            if (date < DateTime.UtcNow.AddDays(-90))
            {
                throw new BadRequestException("Cannot find data for period longer than 90 days");
            };

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
                var responseString = await response.Content.ReadAsStringAsync();
                currencyRate = JsonConvert.DeserializeObject<CurrencyRateDto>(responseString);
            }
            else
            {
                throw new BadRequestException("Not Found - invalid data");
            }

            return currencyRate;
        }

        private string PrepareUrl(string currencyCode, DateTime date)
        {
            var sb = new StringBuilder();

            sb.Append(_config.BaseUrl);
            sb.Append("/api/exchangerates/rates/c");
            sb.Append($"/{currencyCode}");
            sb.Append($"/{date.ToString("yyyy-MM-dd")}");
            sb.Append($"?format=json");

            return sb.ToString();
        }
    }
}
