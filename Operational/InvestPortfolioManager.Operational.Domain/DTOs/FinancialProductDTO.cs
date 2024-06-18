using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestPortfolioManager.Operational.Domain.DTOs
{
    public class FinancialProductDTO
    {
        public string Name { get; set; }
        public DateTime MaturityDate { get; set; }
        public decimal Value { get; set; }
    }
}
