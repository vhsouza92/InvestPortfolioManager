using System.Collections.Generic;
using System.Threading.Tasks;
using InvestPortfolioManager.Client.Application.DTOs;
using InvestPortfolioManager.Client.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvestPortfolioManager.Client.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioController : ControllerBase
    {
        private readonly PortfolioService _portfolioService;

        public PortfolioController(PortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetPortfolioByUserId(int userId)
        {
            try
            {
                var portfolio = await _portfolioService.GetPortfolioByUserIdAsync(userId);
                return Ok(portfolio);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
