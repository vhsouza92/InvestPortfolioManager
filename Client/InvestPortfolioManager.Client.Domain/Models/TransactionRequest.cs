namespace InvestPortfolioManager.Client.Application.Models
{
    public class TransactionRequest
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Type { get; set; } = "Buy";
    }
}
