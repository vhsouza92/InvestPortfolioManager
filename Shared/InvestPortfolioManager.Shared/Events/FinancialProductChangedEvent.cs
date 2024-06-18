namespace InvestPortfolioManager.Shared.Events
{
    public class FinancialProductChangedEvent
    {
        public string Action { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public DateTime MaturityDate { get; set; }
        public decimal Value { get; set; }
    }
}
