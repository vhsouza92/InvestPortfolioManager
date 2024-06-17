using System.Collections.Generic;
using System.Threading.Tasks;
using InvestPortfolioManager.Operational.Application.Services;
using InvestPortfolioManager.Operational.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InvestPortfolioManager.Operational.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialProductsController : ControllerBase
    {
        private readonly FinancialProductService _service;

        public FinancialProductsController(FinancialProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinancialProduct>>> GetAll()
        {
            var products = await _service.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FinancialProduct>> GetById(int id)
        {
            var product = await _service.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Create(FinancialProduct product)
        {
            await _service.AddProductAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, FinancialProduct product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            await _service.UpdateProductAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
