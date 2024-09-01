using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.ApplicationService.Dtos.Portfolio
{
    public class PortfolioCreateDto
    {
        public int UserId { get; set; }
        public int StockId { get; set; }

        public DataAccess.Model.Portfolio ToPortfolioModel()
        {
            return new DataAccess.Model.Portfolio()
            {
                StockId = this.StockId,
                UserId = this.UserId
            };
        }
    }
}