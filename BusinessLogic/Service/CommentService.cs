using api.DataAccess.Model;
using api.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;

namespace api.BusinessLogic.Service
{
    public class CommentService
    {
        private StockRepository stockRepo;
        private CommentRepository commentRepo;
        private UserRepository userRepo;
        public CommentService(StockRepository stockRepo, CommentRepository commentRepo, UserRepository userRepo) 
        {
            this.stockRepo = stockRepo;
            this.commentRepo = commentRepo;
            this.userRepo = userRepo;
        }

        public async Task<List<Comment>> GetBy() 
        {
            var comments = commentRepo.GetAllInQueryable();
            return await comments.ToListAsync();
        }

        public async Task<Comment?> GetBy(int id)
        {
            return await commentRepo.GetByTempEntityAsync(new Comment{Id = id});
        }

        public async Task<List<Comment>> GetBy(IEnumerable<int> ids)
        {
            IQueryable<Comment> targetComments = commentRepo.GetAllInQueryable().Where(c => ids.Contains(c.Id)); 
            return await targetComments.ToListAsync();
        }

        public async Task<List<Comment>> GetIncludeUserBy(IEnumerable<int> ids)
        {
            IQueryable<Comment> targetComments = commentRepo.GetAllIncludeUserInQueryable().Where(c => ids.Contains(c.Id)); 
            return await targetComments.ToListAsync();
        }

        public async Task<Comment?> AddComment(int stockId, Comment tempComment)
        {
            if (!await stockRepo.IsEntityExistsAsync(new Stock{Id = stockId})) return null;
            if (!await userRepo.IsEntityExistsAsync(new User{Id = tempComment.UserId})) return null;

            Comment addedComment = await commentRepo.AddAsync(tempComment);
            await commentRepo.SaveAsync();
            return addedComment;
        }

        public async Task<Comment?> UpdateComment(Comment tempComment)
        {
            Comment? updatedComment = await commentRepo.UpdateAsync(tempComment);
            if (updatedComment != null) await commentRepo.SaveAsync();
            return updatedComment;
        }

        public async Task<Comment?> DeleteComment(int id)
        {
            Comment? deletedComment = await commentRepo.DeleteAsync(new Comment{Id = id});
            if (deletedComment != null) await commentRepo.SaveAsync();
            return deletedComment;
        }
    }
}