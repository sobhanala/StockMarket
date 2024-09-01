using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace api.DataAccess.Repository
{
    public class UserRepository : IGenericRepository<User>
    {
        private ApplicationDbContext context;
        private DbSet<User> users;
        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.users = context.Users;
        }

        public IQueryable<User> GetAllInQueryable()
        {
            return users.AsQueryable();
        }

        public IQueryable<User> GetAllIncludePortfolioInQueryable()
        {
            return users.Include(u => u.Portfolios);
        }

        public async Task<User?> GetByTempEntityAsync(User tempUser)
        {
            return await users.FirstOrDefaultAsync(u => u.Id == tempUser.Id);
        }

        public async Task<User> AddAsync(User newRecord)
        {
            await users.AddAsync(newRecord);
            return newRecord;
        }

        public async Task<User?> UpdateAsync(User newRecord)
        {
            User? targetUser = await users.FirstOrDefaultAsync(u => u.Id == newRecord.Id);
            if (targetUser == null) return null;

            targetUser.Name = newRecord.Name;
            targetUser.Password = newRecord.Password;

            return targetUser;
        }

        public async Task<User?> DeleteAsync(User tempUser) 
        {
            User? targetUser = await users.FirstOrDefaultAsync(u => u.Id == tempUser.Id);
            if (targetUser == null) return null;

            users.Remove(targetUser);

            return targetUser;
        }

        public async Task<bool> IsEntityExistsAsync(User tempUser)
        {
            return await users.AnyAsync(u => u.Id == tempUser.Id);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}