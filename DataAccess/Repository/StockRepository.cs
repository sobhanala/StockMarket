using api.BusinessLogic.Service;
using api.DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;

namespace api.DataAccess.Repository
{
    public class StockRepository : IGenericRepository<Stock>
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<Stock> stocks;
        public StockRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.stocks = context.Stocks;
        }

        public IQueryable<Stock> GetAllInQueryable()
        {
            return stocks.AsQueryable();
        }

        public IQueryable<Stock> GetAllIncludeCommentInQueryable()
        {
            return stocks.Include(s => s.Comments);
        }

        public async Task<Stock?> GetByTempEntityAsync(Stock tempStock)
        {
            return await stocks.FirstOrDefaultAsync(s => s.Id == tempStock.Id);
        }

        public async Task<Stock?> GetByIdIncludeCommentAsync(int id)
        {
            return await stocks.Include(s => s.Comments).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Stock>> FilterBySymbolAndNameContains(string symbol, string name)
        {
            var targetStocks = stocks.AsQueryable();
            if (symbol != string.Empty) targetStocks = targetStocks.Where(s => s.Symbol.Contains(symbol)).AsQueryable();
            if (name != string.Empty) targetStocks = targetStocks.Where(s => s.CompanyName.Contains(name));
            return await targetStocks.ToListAsync();
        }

        public async Task<Stock> AddAsync(Stock newRecord)
        {
            await stocks.AddAsync(newRecord);
            return newRecord;
        }

        public async Task<Stock?> UpdateAsync(Stock newRecord)
        {
            var targetStock = await stocks.FirstOrDefaultAsync(s => s.Id == newRecord.Id);
            if (targetStock == null) return null;

            targetStock.Symbol = newRecord.Symbol;
            targetStock.CompanyName = newRecord.CompanyName;
            targetStock.Purchase = newRecord.Purchase;
            targetStock.LastDiv = newRecord.LastDiv;
            targetStock.Industry = newRecord.Industry;
            targetStock.MarketCap = newRecord.MarketCap;

            return targetStock;
        }

        public async Task<Stock?> DeleteAsync(Stock tempStock)
        {
            var targetStock = await stocks.FirstOrDefaultAsync(s => s.Id == tempStock.Id);
            if (targetStock == null) return null;

            stocks.Remove(targetStock);

            return targetStock;
        }

        public async Task<bool> IsEntityExistsAsync(Stock tempStock) 
        {
            return await stocks.AnyAsync(s => s.Id == tempStock.Id);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
