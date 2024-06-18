namespace InvestPortfolioManager.Client.Application.DTOs
{
    public class TransactionDto
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Type { get; set; } = "Buy";
    }
}
