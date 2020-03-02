using CurrencyViewer.Application.Models;
using CurrencyViewer.Application.Tests.Infrastructure;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace CurrencyViewer.Application.Tests
{
    public class CurrencyRatesReceiverTests
    {

        [Fact]
        public async Task Should_Return_Anything_For_Valid_Request()
        {
            var options = new Mock<IOptions<CurrencyRatesConfig>>();
            options.SetupGet(x => x.Value).Returns(new CurrencyRatesConfig() { BaseUrl = "http://api.nbp.pl", CurrencyCodes= new string[] { "usd", "eur" } });
           

            var factoryMock = new Mock<IHttpClientFactory>();
            var returnValue = new CurrencyRateDto() { Rates = new List<Rate>() };
            var messageHandler = new MockHttpMessageHandler(JsonSerializer.Serialize<CurrencyRateDto>(returnValue), HttpStatusCode.OK);
            var httpClient = new HttpClient(messageHandler);
            factoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            ICurrencyRatesReceiver receiver = new CurrencyRatesReceiver(options.Object, factoryMock.Object);

            var result = await receiver.GetCurrencyRatesAsync(DateTime.UtcNow.Date);

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task When_Config_Is_Invalid_Should_Throw_Exception()
        {
            var options = new Mock<IOptions<CurrencyRatesConfig>>();

            var factoryMock = new Mock<IHttpClientFactory>();
            var returnValue = new CurrencyRateDto() { Rates = new List<Rate>() };
            var messageHandler = new MockHttpMessageHandler(JsonSerializer.Serialize(returnValue), HttpStatusCode.OK);
            var httpClient = new HttpClient(messageHandler);
            factoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            ICurrencyRatesReceiver receiver = new CurrencyRatesReceiver(options.Object, factoryMock.Object);

            Task result() => receiver.GetCurrencyRatesAsync(DateTime.UtcNow.Date);

            await Assert.ThrowsAsync<InvalidOperationException>(result);
        }
    }
}
