using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DataAccess.Model;
using api.DataAccess.Repository;

namespace api.BusinessLogic.Service
{
    public class PortfolioService
    {
        private PortfolioRepository portfolioRepo;
        private StockRepository stockRepo;
        private UserRepository userRepo;
        public PortfolioService(PortfolioRepository portfolioRepo, StockRepository stockRepo, UserRepository userRepo)
        {
            this.portfolioRepo = portfolioRepo;
            this.stockRepo = stockRepo;
            this.userRepo = userRepo;
        }

        public async Task<Portfolio?> AddPortfolio(Portfolio tempPortfolio)
        {
            if (!await stockRepo.IsEntityExistsAsync(new Stock{Id = tempPortfolio.StockId})) return null;
            if (!await userRepo.IsEntityExistsAsync(new User{Id = tempPortfolio.UserId})) return null;

            Portfolio addedPortfolio = await portfolioRepo.AddAsync(tempPortfolio);
            await portfolioRepo.SaveAsync();
            return addedPortfolio;
        }

        public async Task<Portfolio?> DeletePortfolio(int userId, int stockId)
        {
            Portfolio? deletedPortfolio = await portfolioRepo.DeleteAsync(new Portfolio{UserId = userId, StockId = stockId});
            if (deletedPortfolio != null) await portfolioRepo.SaveAsync();
            return deletedPortfolio; 
        }
    }
}