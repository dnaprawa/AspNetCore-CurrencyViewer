using CurrencyViewer.Domain;
using System;

namespace CurrencyViewer.Application.Models
{
    public class CurrencyRateDto
    {
        public CurrencyType CurrencyType { get; set; }
        public double BidValue { get; set; }
        public double AskValue { get; set; }
        public DateTime Date { get; set; }
        public DateTime ReceivedAt { get; set; }
    }
}
