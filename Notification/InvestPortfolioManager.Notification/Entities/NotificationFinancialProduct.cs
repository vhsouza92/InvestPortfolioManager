namespace InvestPortfolioManager.Notification.Entities
{
  public class NotificationFinancialProduct
  {
      public int Id { get; set; }
      public string Name { get; set; }
      public decimal Value { get; set; }
      public DateTime MaturityDate { get; set; }
  }
}