using CurrencyViewer.Application.Exceptions;
using CurrencyViewer.Application.Interfaces;
using CurrencyViewer.Application.Models;
using CurrencyViewer.Application.Services;
using CurrencyViewer.Application.Tests.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CurrencyViewer.Application.Tests
{
    public class CurrencyRatesBetweenDatesReceiverTests
    {
        [Fact]
        public async Task When_DateFrom_Is_Greater_Than_DateFrom_Should_Throw_Exception()
        {
            var config = ConfigFactory.GetConfig();
            var httpClientFactory = HttpClientFactoryProvider.GetHttpClientFactory(new CurrencyRateDto() { Rates = new List<Rate>() });

            ICurrencyRatesBetweenDatesReceiver receiver = new CurrencyRatesBetweenDatesReceiver(config, httpClientFactory);

            Task result() => receiver.GetCurrencyRatesBetweenDaysAsync(DateTime.UtcNow.Date.AddDays(-1), DateTime.UtcNow.Date.AddDays(-2));

            await Assert.ThrowsAsync<InvalidParameterException>(result);
        }

        [Fact]
        public async Task When_DateFrom_Is_Lower_Than_90_Days_Should_Throw_Exception()
        {
            var config = ConfigFactory.GetConfig();
            var httpClientFactory = HttpClientFactoryProvider.GetHttpClientFactory(new CurrencyRateDto() { Rates = new List<Rate>() });

            ICurrencyRatesBetweenDatesReceiver receiver = new CurrencyRatesBetweenDatesReceiver(config, httpClientFactory);

            Task result() => receiver.GetCurrencyRatesBetweenDaysAsync(DateTime.UtcNow.Date.AddDays(-91), DateTime.UtcNow.Date.AddDays(-1));

            await Assert.ThrowsAsync<BadRequestException>(result);
        }

        [Fact]
        public async Task Should_Return_Data_For_Valid_Request()
        {
            var config = ConfigFactory.GetConfig();
            var httpClientFactory = HttpClientFactoryProvider.GetHttpClientFactory(
                new List<CurrencyRateDto>() { new CurrencyRateDto() { Rates = new List<Rate>() } }
                );

            ICurrencyRatesBetweenDatesReceiver receiver = new CurrencyRatesBetweenDatesReceiver(config, httpClientFactory);

            var result = await receiver.GetCurrencyRatesBetweenDaysAsync(DateTime.UtcNow.Date.AddDays(-40), DateTime.UtcNow.Date.AddDays(-30));

            Assert.NotEmpty(result);
        }
    }
}
