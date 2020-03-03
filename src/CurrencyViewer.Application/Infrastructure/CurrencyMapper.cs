using CurrencyViewer.Application.Models;
using CurrencyViewer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CurrencyViewer.Application.Infrastructure
{
    public static class CurrencyMapper
    {
        public static CurrencyRate MapSingle(CurrencyRateDto dto)
        {
            return new CurrencyRate(
                dto.Code.ToEnum<CurrencyType>(),
                dto.Rates.First().Bid,
                dto.Rates.First().Ask,
                DateTime.Parse(dto.Rates.First().EffectiveDate)
                );
        }

        public static List<CurrencyRate> MapyMany(CurrencyRateDto dto)
        {
            var rates = new List<CurrencyRate>();
            foreach (var rate in dto.Rates)
            {
                rates.Add(
                    new CurrencyRate(
                        dto.Code.ToEnum<CurrencyType>(),
                        rate.Bid,
                        rate.Ask,
                        DateTime.Parse(rate.EffectiveDate)
                        )
                    );
            }
            return rates;
        }

        public static CurrencyRateViewModel MapFromDto(CurrencyRate entity)
        {
            return new CurrencyRateViewModel()
            {
                AskValue = entity.AskValue,
                BidValue = entity.BidValue,
                CurrencyType = entity.CurrencyType.ToString(),
                Date = entity.Date,
            };
        }
    }
}
