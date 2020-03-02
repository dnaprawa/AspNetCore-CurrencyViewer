using CurrencyViewer.Application.Models;
using CurrencyViewer.Domain;
using System;
using System.Linq;

namespace CurrencyViewer.Application.Infrastructure
{
    public static class CurrencyMapper
    {
        public static CurrencyRate MapFromDto(CurrencyRateDto dto)
        {
            return new CurrencyRate(
                dto.Code.ToEnum<CurrencyType>(),
                dto.Rates.First().Bid,
                dto.Rates.First().Ask,
                DateTime.Parse(dto.Rates.First().EffectiveDate)
                );
        }
    }
}
