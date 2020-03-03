using CurrencyViewer.Application.Models;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace CurrencyViewer.Application.Tests.Infrastructure
{
    public static class HttpClientFactoryProvider
    {
        public static IHttpClientFactory GetHttpClientFactory(object expectedValue = null)
        {
            var factoryMock = new Mock<IHttpClientFactory>();
            
            var returnValue = expectedValue ?? new CurrencyRateDto() { Rates = new List<Rate>() };

            var messageHandler = new MockHttpMessageHandler(JsonSerializer.Serialize(returnValue), HttpStatusCode.OK);
            
            var httpClient = new HttpClient(messageHandler);
            factoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            return factoryMock.Object;
        }
    }
}
