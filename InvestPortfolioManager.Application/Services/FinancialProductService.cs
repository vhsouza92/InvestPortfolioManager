using InvestPortfolioManager.Domain.Entities;
using InvestPortfolioManager.Domain.Repositories;
using InvestPortfolioManager.Application.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace InvestPortfolioManager.Application.Services
{
    public class FinancialProductService
    {
        private readonly IFinancialProductRepository _financialProductRepository;

        public FinancialProductService(IFinancialProductRepository financialProductRepository)
        {
            _financialProductRepository = financialProductRepository;
        }

        public FinancialProductDto GetFinancialProductById(int id)
        {
            var product = _financialProductRepository.GetById(id);
            return new FinancialProductDto { Id = product.Id, Name = product.Name, MaturityDate = product.MaturityDate, Value = product.Value };
        }

        public IEnumerable<FinancialProductDto> GetAllFinancialProducts()
        {
            var products = _financialProductRepository.GetAll();
            return products.Select(product => new FinancialProductDto { Id = product.Id, Name = product.Name, MaturityDate = product.MaturityDate, Value = product.Value });
        }

        public void AddFinancialProduct(FinancialProductDto productDto)
        {
            var product = new FinancialProduct
            {
                Name = productDto.Name,
                MaturityDate = productDto.MaturityDate,
                Value = productDto.Value
            };
            _financialProductRepository.Add(product);
        }

        public void UpdateFinancialProduct(FinancialProductDto productDto)
        {
            var product = new FinancialProduct
            {
                Id = productDto.Id,
                Name = productDto.Name,
                MaturityDate = productDto.MaturityDate,
                Value = productDto.Value
            };
            _financialProductRepository.Update(product);
        }

        public void DeleteFinancialProduct(int id)
        {
            _financialProductRepository.Delete(id);
        }
    }
}
