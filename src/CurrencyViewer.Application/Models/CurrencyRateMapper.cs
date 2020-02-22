using CurrencyViewer.Domain;

namespace CurrencyViewer.Application.Models
{
    public static class CurrencyRateMapper
    {
        public static CurrencyRateDto Map(CurrencyRate currencyRate)
        {
            return new CurrencyRateDto()
            {
                AskValue = currencyRate.AskValue,
                BidValue = currencyRate.BidValue,
                CurrencyType = currencyRate.CurrencyType,
                Date = currencyRate.Date,
                ReceivedAt = currencyRate.ReceivedAt
            };
        }
    }
}