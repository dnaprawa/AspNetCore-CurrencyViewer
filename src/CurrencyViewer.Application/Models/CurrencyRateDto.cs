﻿using System.Collections.Generic;

namespace CurrencyViewer.Application.Models
{
    public class CurrencyRateDto
    {
        public string Table { get; set; }
        public string Currency { get; set; }
        public string Code { get; set; }
        public IEnumerable<Rate> Rates { get; set; }
    }

    public class Rate
    {
        public string No { get; set; }
        public string EffectiveDate { get; set; }
        public string Bid { get; set; }
        public string Ask { get; set; }
    }
}
