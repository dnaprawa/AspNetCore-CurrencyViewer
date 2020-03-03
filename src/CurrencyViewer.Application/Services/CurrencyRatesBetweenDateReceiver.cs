using CurrencyViewer.Application.Exceptions;
using CurrencyViewer.Application.Interfaces;
using CurrencyViewer.Application.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyViewer.Application.Services
{
    public class CurrencyRatesBetweenDatesReceiver : ICurrencyRatesBetweenDatesReceiver
    {
        private readonly CurrencyRatesConfig _config;
        private readonly IHttpClientFactory _clientFactory;
        public CurrencyRatesBetweenDatesReceiver(IOptions<CurrencyRatesConfig> options, IHttpClientFactory clientFactory)
        {
            _config = options.Value;
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<CurrencyRateDto>> GetCurrencyRatesBetweenDaysAsync(DateTime dateFrom, DateTime dateTo)
        {
            if (dateFrom > dateTo)
            {
                throw new InvalidParameterException("DateFrom cannot be greater than DateTo");
            }

            if (dateFrom < DateTime.UtcNow.AddDays(-90))
            {
                throw new BadRequestException("Cannot find data for period longer than 90 days");
            };

            var data = new List<CurrencyRateDto>();
            foreach (var item in _config.CurrencyCodes)
            {
                var currencyRate = await GetSingleCurrencyRateAsync(item, dateFrom, dateTo);

                data.Add(currencyRate);
            }

            return data;
        }

        private async Task<CurrencyRateDto> GetSingleCurrencyRateAsync(string currencyCode, DateTime dateFrom, DateTime dateTo)
        {
            var url = PrepareUrl(currencyCode, dateFrom, dateTo);

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            CurrencyRateDto currencyRates = null;
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                currencyRates = JsonConvert.DeserializeObject<CurrencyRateDto>(responseString);
            }
            else
            {
                throw new BadRequestException("Not Found - invalid data");
            }

            return currencyRates;
        }

        private string PrepareUrl(string currencyCode, DateTime dateFrom, DateTime dateTo)
        {
            var sb = new StringBuilder();

            sb.Append(_config.BaseUrl);
            sb.Append("/api/exchangerates/rates/c");
            sb.Append($"/{currencyCode}");
            sb.Append($"/{dateFrom.ToString("yyyy-MM-dd")}");
            sb.Append($"/{dateTo.ToString("yyyy-MM-dd")}");
            sb.Append($"?format=json");

            return sb.ToString();
        }
    }
}
