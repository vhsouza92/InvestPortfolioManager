namespace InvestPortfolioManager.Client.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}
