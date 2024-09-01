using api.BusinessLogic.Entity.Stock;
using api.DataAccess.Model;
using api.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace api.BusinessLogic.Service
{
    public class StockService
    {
        private StockRepository stockRepo;
        private CommentRepository commentRepo;
        public StockService(StockRepository stockRepo, CommentRepository commentRepo) 
        {
            this.stockRepo = stockRepo;
            this.commentRepo = commentRepo;
        }

        public async Task<List<Stock>> GetBy(StockQueryObject queryObject)
        {
            IQueryable<Stock> targetStocks = stockRepo.GetAllInQueryable();
            targetStocks = queryObject.ApplyAllQueries(targetStocks);
            return await targetStocks.ToListAsync();
        }

        public async Task<Stock?> GetBy(int id)
        {
            return await stockRepo.GetByTempEntityAsync(new Stock{Id = id});
        }

        public async Task<List<Stock>> GetBy(List<int> ids)
        {
            IQueryable<Stock> targetStocks = stockRepo.GetAllInQueryable();
            targetStocks = targetStocks.Where(s => ids.Contains(s.Id));
            return await targetStocks.ToListAsync();
        }

        public async Task<List<Stock>> GetIncludeCommentBy(StockQueryObject queryObject)
        {
            IQueryable<Stock> targetStocks = stockRepo.GetAllIncludeCommentInQueryable();
            targetStocks = queryObject.ApplyAllQueries(targetStocks);
            return await targetStocks.ToListAsync();
        }

        public async Task<Stock?> GetIncludeCommentBy(int id)
        {
            return await stockRepo.GetByIdIncludeCommentAsync(id);
        }

        public async Task<Stock> AddNewStock(Stock newStock)
        {
            var addedStock = await stockRepo.AddAsync(newStock);
            await stockRepo.SaveAsync();
            return addedStock;
        }

        public async Task<Stock?> UpdateStock(Stock tempStock)
        {
            Stock? updatedStock = await stockRepo.UpdateAsync(tempStock);
            if (updatedStock != null) await stockRepo.SaveAsync();
            return updatedStock;
        }

        public async Task<Stock?> DeleteStock(int id)
        {
            Stock? deletedStock = await stockRepo.DeleteAsync(new Stock{Id = id});
            if (deletedStock != null) await stockRepo.SaveAsync();
            return deletedStock;
        }

        public async Task<Stock?> DeleteStockWithComment(int id)
        {
            if (!await stockRepo.IsEntityExistsAsync(new Stock{Id = id})) return null;

            await commentRepo.DeleteCommentsWithStockIdAsync(id);
            Stock? deletedStock = await stockRepo.DeleteAsync(new Stock{Id = id});
            if (deletedStock != null) await stockRepo.SaveAsync(); //FIXME: It is not beautiful, saving should be single not in every repo 
            return deletedStock;
        }
    }
}