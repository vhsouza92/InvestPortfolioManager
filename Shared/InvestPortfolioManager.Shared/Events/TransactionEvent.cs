namespace InvestPortfolioManager.Shared.Events
{
    public class TransactionEvent
    {
        public int TransactionId { get; set; }
        public int ProductId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
    }
}