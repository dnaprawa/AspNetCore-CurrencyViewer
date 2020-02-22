using System;

namespace CurrencyViewer.Domain
{
    public class CurrencyRate
    {
        public CurrencyRate(CurrencyType currencyType, double value)
        {
            CurrencyType = currencyType;
            Value = value;
            Date = DateTime.UtcNow.Date;
            ReceivedAt = DateTime.UtcNow;
        }

        protected CurrencyRate() { }

        public int Id { get; set; }
        public CurrencyType CurrencyType { get; protected set; }
        public double Value { get; set; }
        public DateTime Date { get; protected set; }
        public DateTime ReceivedAt { get; protected set; }
    }
}
