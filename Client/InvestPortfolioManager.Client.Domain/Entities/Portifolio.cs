using System;
using System.Collections.Generic;

namespace InvestPortfolioManager.Client.Domain.Entities
{
    public class Portfolio
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public ICollection<PortfolioItem>? Items { get; set; }
    }

    public class PortfolioItem
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalValue { get; set; }
    }
}