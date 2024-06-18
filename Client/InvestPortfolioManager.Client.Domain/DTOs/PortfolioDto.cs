using System;
using System.Collections.Generic;

namespace InvestPortfolioManager.Client.Application.DTOs
{
    public class PortfolioDto
    {
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<PortfolioItemDto> Items { get; set; }
    }

    public class PortfolioItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalValue { get; set; }
    }
}
