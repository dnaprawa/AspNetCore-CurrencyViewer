using System;

namespace CurrencyViewer.Domain
{
    public class CurrencyRate
    {
        public CurrencyRate(CurrencyType currencyType, double bidValue, double askValue, DateTime date)
        {
            CurrencyType = currencyType;
            BidValue = bidValue;
            AskValue = askValue;
            Date = date;
            ReceivedAt = DateTime.UtcNow;
        }

        protected CurrencyRate() { }

        public int Id { get; set; }
        public CurrencyType CurrencyType { get; protected set; }
        public double BidValue { get; protected set; }
        public double AskValue { get; protected set; }
        public DateTime Date { get; protected set; }
        public DateTime ReceivedAt { get; protected set; }

        public void Update(double newBidValue, double newAskValue)
        {
            BidValue = newBidValue;
            AskValue = newAskValue;
        }
        
    }
}
