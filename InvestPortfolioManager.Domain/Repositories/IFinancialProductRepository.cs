using InvestPortfolioManager.Domain.Entities;

namespace InvestPortfolioManager.Domain.Repositories
{
    public interface IFinancialProductRepository
    {
        FinancialProduct GetById(int id);
        IEnumerable<FinancialProduct> GetAll();
        void Add(FinancialProduct product);
        void Update(FinancialProduct product);
        void Delete(int id);
    }
}