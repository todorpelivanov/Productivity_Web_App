using Microsoft.EntityFrameworkCore;
using ProducitivityApp.DataAccess.Interfaces;
using ProductivityApp.Domain.Entities;

using Task = System.Threading.Tasks.Task;

namespace ProducitivityApp.DataAccess.Implementations
{
    public class UserSessionRepository : IUserSessionRepository
    {
        private readonly ProductivityAppDbContext _dbContext;

        public UserSessionRepository(ProductivityAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(UserSession entity)
        {
            _dbContext.UserSessions.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(UserSession entity)
        {
            _dbContext.UserSessions.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<UserSession>> GetAll()
        {
            return await _dbContext.UserSessions
                .Include(x => x.User)
                .Include(t => t.Tasks)
                .ToListAsync();
        }

        public async Task<UserSession> GetById(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _dbContext.UserSessions
                .Include(x => x.User)
                .Include(t => t.Tasks)
                .SingleOrDefaultAsync(s => s.Id == id);

        }

        public async Task Update(UserSession entity)
        {
            _dbContext.UserSessions.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
