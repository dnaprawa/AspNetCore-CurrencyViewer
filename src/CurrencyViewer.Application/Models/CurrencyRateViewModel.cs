using System;

namespace CurrencyViewer.Application.Models
{
    public class CurrencyRateViewModel
    {
        public string CurrencyType { get; set; }
        public double BidValue { get; set; }
        public double AskValue { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
