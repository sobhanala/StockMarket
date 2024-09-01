using api.DataAccess.Model;
using api.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace api.BusinessLogic.Service
{
    public class UserService
    {
        private UserRepository userRepo;
        private StockRepository stockRepo;
        private PortfolioRepository portfolioRepo;
        public UserService(UserRepository userRepo, StockRepository stockRepo, PortfolioRepository portfolioRepo)
        {
            this.userRepo = userRepo;
            this.stockRepo = stockRepo;
            this.portfolioRepo = portfolioRepo;
        }

        public async Task<List<User>> GetBy()
        {
            return await userRepo.GetAllInQueryable().ToListAsync();
        }

        public async Task<User?> GetBy(int id)
        {
            return await userRepo.GetByTempEntityAsync(new User{Id = id});
        }

        public async Task<List<User>> GetIncludePortfolioBy()
        {
            return await userRepo.GetAllIncludePortfolioInQueryable().ToListAsync();
        }

        public async Task<List<Stock>?> GetUserPortfolio(int userId)
        {
            if (!await userRepo.IsEntityExistsAsync(new User{Id = userId})) return null;

            IQueryable<Portfolio> targetPortfolios = portfolioRepo.GetAllIncludeStockInQueryable();
            IQueryable<Stock> targetStocks = targetPortfolios.Where(p => p.UserId == userId).Select(p => p.Stock); 
            return await targetStocks.ToListAsync();
        }
    }
}