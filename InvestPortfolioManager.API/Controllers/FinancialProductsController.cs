using InvestPortfolioManager.Application.DTOs;
using InvestPortfolioManager.Application.Services;
using InvestPortfolioManager.Infrastructure.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace InvestPortfolioManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinancialProductsController : ControllerBase
    {
        private readonly FinancialProductService _financialProductService;
        private readonly MessagePublisher _messagePublisher;

        public FinancialProductsController(FinancialProductService financialProductService, MessagePublisher messagePublisher)
        {
            _financialProductService = financialProductService;
            _messagePublisher = messagePublisher;
        }

        [HttpGet("{id}")]
        public IActionResult GetFinancialProductById(int id)
        {
            var product = _financialProductService.GetFinancialProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet]
        public IActionResult GetAllFinancialProducts()
        {
            var products = _financialProductService.GetAllFinancialProducts();
            return Ok(products);
        }

        [HttpPost]
        public IActionResult AddFinancialProduct([FromBody] FinancialProductDto productDto)
        {
            _financialProductService.AddFinancialProduct(productDto);
            _messagePublisher.Publish("Financial product added", "exchange_name", "routing_key");
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateFinancialProduct([FromBody] FinancialProductDto productDto)
        {
            _financialProductService.UpdateFinancialProduct(productDto);
            _messagePublisher.Publish("Financial product updated", "exchange_name", "routing_key");
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteFinancialProduct(int id)
        {
            _financialProductService.DeleteFinancialProduct(id);
            _messagePublisher.Publish("Financial product deleted", "exchange_name", "routing_key");
            return Ok();
        }
    }
}
