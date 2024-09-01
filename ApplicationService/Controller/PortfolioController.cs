using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.ApplicationService.Dtos.Portfolio;
using api.BusinessLogic.Service;
using api.DataAccess.Model;
using Microsoft.AspNetCore.Mvc;

namespace api.ApplicationService.Controller
{
    [Route("/api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private PortfolioService portfolioService;
        public PortfolioController(PortfolioService portfolioService)
        {
            this.portfolioService = portfolioService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPortfolio([FromBody] PortfolioCreateDto portfolioDto)
        {
            Portfolio? addedPortfolio = await portfolioService.AddPortfolio(portfolioDto.ToPortfolioModel());
            if (addedPortfolio == null) return NotFound();
            else return Created();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePortfolio([FromQuery] int? userId, [FromQuery] int? stockId)
        {
            if (userId == null || stockId == null) return BadRequest();
            Portfolio? deletedPortfolio = await portfolioService.DeletePortfolio((int)userId, (int)stockId);
            return (deletedPortfolio == null) ? NotFound() : NoContent();
        }
    }
}