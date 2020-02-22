using System;

namespace CurrencyViewer.Domain
{
    public class CurrencyRate
    {
        public CurrencyRate(CurrencyType currencyType, double bidValue, double askValue)
        {
            CurrencyType = currencyType;
            BidValue = bidValue;
            AskValue = askValue;
            Date = DateTime.UtcNow.Date;
            ReceivedAt = DateTime.UtcNow;
        }

        protected CurrencyRate() { }

        public int Id { get; set; }
        public CurrencyType CurrencyType { get; protected set; }
        public double BidValue { get; protected set; }
        public double AskValue { get; protected set; }
        public DateTime Date { get; protected set; }
        public DateTime ReceivedAt { get; protected set; }
        
    }
}
