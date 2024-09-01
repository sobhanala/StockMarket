using api.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace api.DataAccess.Repository
{
    public class CommentRepository : IGenericRepository<Comment>
    {
        private ApplicationDbContext context;
        private DbSet<Comment> comments;
        public CommentRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.comments =  context.Comments;
        }
        
        public IQueryable<Comment> GetAllInQueryable()
        {
            return comments.AsQueryable();
        }

        public IQueryable<Comment> GetAllIncludeUserInQueryable()
        {
            return comments.Include(c => c.User);
        }

        public async Task<Comment?> GetByTempEntityAsync(Comment tempComment) 
        {
            return await comments.FirstOrDefaultAsync(c => c.Id == tempComment.Id);
        }

        public async Task<Comment> AddAsync(Comment newRecord)
        {
            await comments.AddAsync(newRecord);
            return newRecord;
        }

        public async Task<Comment?> UpdateAsync(Comment newRecord) 
        {
            var targetComment = await comments.FirstOrDefaultAsync(c => c.Id == newRecord.Id);
            if (targetComment == null) return null;

            targetComment.Title = newRecord.Title;
            targetComment.Content = newRecord.Content;

            return targetComment;
        }

        public async Task<Comment?> DeleteAsync(Comment tempComment) 
        {
            var targetComment = await comments.FirstOrDefaultAsync(c => c.Id == tempComment.Id);
            if (targetComment == null) return null;

            comments.Remove(targetComment);

            return targetComment;
        }

        public async Task DeleteCommentsWithStockIdAsync(int stockId)
        {
            var targetComments = await comments.Where(c => c.StockId == stockId).ToListAsync();
            foreach(var c in targetComments) comments.Remove(c);
        }

        public async Task<bool> IsEntityExistsAsync(Comment tempComment)
        {
            return await comments.AnyAsync(c => c.Id == tempComment.Id);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}