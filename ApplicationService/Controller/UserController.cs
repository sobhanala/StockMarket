using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.ApplicationService.Dtos.User;
using api.BusinessLogic.Service;
using api.DataAccess.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.ApplicationService.Controller
{
    [Route("/api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserService userService;
        private StockService stockService;
        public UserController(UserService userService, StockService stockService)
        {
            this.userService = userService;
            this.stockService = stockService;
        }


        [HttpGet]
        public async Task<IActionResult> GetBy()
        {
            var users = await userService.GetBy();
            var userDtos = users.Select(u => new UserDto(u));
            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBy([FromRoute] int id)
        {
            User? targetUser = await userService.GetBy(id);
            return (targetUser == null) ? NotFound() : Ok(new UserDto(targetUser));
        }

        [HttpGet("with-portfolio")]
        public async Task<IActionResult> GetIncludePortfolioBy()
        {
            List<User> users = await userService.GetBy();
            List<UserWithPortfolioDto> userDtos = new List<UserWithPortfolioDto>();
            foreach(User user in users)
            {
                List<Stock>? userPortfolio = await userService.GetUserPortfolio(user.Id);
                if (userPortfolio != null) userDtos.Add(new UserWithPortfolioDto(user, userPortfolio));
            }
            return Ok(userDtos);
        }

        [HttpGet("{id}/with-portfolio")]
        public async Task<IActionResult> GetIncludePortfolioBy([FromRoute] int id)
        {
            List<User> users = await userService.GetBy();
            List<UserWithPortfolioDto> userDtos = new List<UserWithPortfolioDto>();
            foreach(User user in users)
            {
                List<Stock>? userPortfolio = await userService.GetUserPortfolio(user.Id);
                if (userPortfolio != null) userDtos.Add(new UserWithPortfolioDto(user, userPortfolio));
            }
            var target = userDtos.Where(u => u.Id==id);
            return Ok(target);
        }
    }
}