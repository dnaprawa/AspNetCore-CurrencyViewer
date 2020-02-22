using System;

namespace CurrencyViewer.Application.Models
{
    public class CurrencyRateViewModel
    {
        public string CurrencyType { get;  set; }
        public double AverageBidValue { get;  set; }
        public double AverageAskValue { get;  set; }
        public DateTime DateFrom { get;  set; }
        public DateTime DateTo { get;  set; }
    }
}
