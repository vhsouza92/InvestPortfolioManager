using InvestPortfolioManager.Domain.Entities;
using InvestPortfolioManager.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace InvestPortfolioManager.Infrastructure.Repositories
{
    public class FinancialProductRepository : IFinancialProductRepository
    {
        private readonly AppDbContext _context;

        public FinancialProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public FinancialProduct GetById(int id)
        {
            return _context.FinancialProducts.Find(id);
        }

        public IEnumerable<FinancialProduct> GetAll()
        {
            return _context.FinancialProducts.ToList();
        }

        public void Add(FinancialProduct product)
        {
            _context.FinancialProducts.Add(product);
            _context.SaveChanges();
        }

        public void Update(FinancialProduct product)
        {
            _context.FinancialProducts.Update(product);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = _context.FinancialProducts.Find(id);
            if (product != null)
            {
                _context.FinancialProducts.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}