using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Threading.Tasks;
using api.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace api.DataAccess.Repository
{
    public class PortfolioRepository : IGenericRepository<Portfolio>
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<Portfolio> portfolios;
        public PortfolioRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.portfolios = context.Portfolios;
        }

        public IQueryable<Portfolio> GetAllInQueryable()
        {
            return portfolios.AsQueryable();
        }

        public IQueryable<Portfolio> GetAllIncludeStockInQueryable()
        {
            return portfolios.Include(p => p.Stock);
        }

        public async Task<Portfolio?> GetByTempEntityAsync(Portfolio tempPortfolio)
        {
            return await portfolios.FirstOrDefaultAsync(p => 
                p.StockId == tempPortfolio.StockId && p.UserId == tempPortfolio.UserId
            );
        }

        public async Task<Portfolio> AddAsync(Portfolio tempPortfolio)
        {
            await portfolios.AddAsync(tempPortfolio);
            return tempPortfolio;
        }

        public async Task<Portfolio?> UpdateAsync(Portfolio tempPortfolio)
        {
            return null;
        }

        public async Task<Portfolio?> DeleteAsync(Portfolio tempPortfolio)
        {
            Portfolio? targetPortfolio = await portfolios.FirstOrDefaultAsync(p => 
                p.StockId == tempPortfolio.StockId && p.UserId == tempPortfolio.UserId
            );
            if (targetPortfolio == null) return null;

            portfolios.Remove(targetPortfolio);
            
            return targetPortfolio;
        }
        
        public async Task<bool> IsEntityExistsAsync(Portfolio tempPortfolio)
        {
            return await portfolios.AnyAsync(p => 
                p.StockId == tempPortfolio.StockId && p.UserId == tempPortfolio.UserId
            );
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}