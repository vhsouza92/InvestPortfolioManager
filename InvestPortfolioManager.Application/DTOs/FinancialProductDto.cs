namespace InvestPortfolioManager.Application.DTOs
{
    public class FinancialProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime MaturityDate { get; set; }
        public decimal Value { get; set; }
    }
}