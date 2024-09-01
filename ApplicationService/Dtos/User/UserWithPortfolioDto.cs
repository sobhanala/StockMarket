using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.ApplicationService.Dtos.Stock;

namespace api.ApplicationService.Dtos.User
{
    public class UserWithPortfolioDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<StockDto> Portfolio { get; set; }
        public UserWithPortfolioDto(DataAccess.Model.User userModel, List<DataAccess.Model.Stock> stocks)
        {
            this.Id = userModel.Id;
            this.Name = userModel.Name;
            this.Portfolio = stocks.Select(s => new StockDto(s)).ToList();
        }
    }
}