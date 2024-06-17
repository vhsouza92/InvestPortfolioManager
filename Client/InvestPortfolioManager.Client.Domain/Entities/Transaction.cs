﻿using System;

namespace InvestPortfolioManager.Client.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
    }
}
