using Microsoft.EntityFrameworkCore;
using ProducitivityApp.DataAccess.Interfaces;
using ProductivityApp.Domain.Entities;
using Serilog;
using System.Timers;
using Task = System.Threading.Tasks.Task;

namespace ProducitivityApp.DataAccess.Implementations
{
    public class ReminderRepository : IReminderRepository
    {

        private readonly ProductivityAppDbContext _dbContext;

        public ReminderRepository(ProductivityAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Reminder entity)
        {
            _dbContext.Reminders.Add(entity);
            Log.Logger.Information($"Reminder has been added by user with id {entity.User.Fullname}!");
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Reminder entity)
        {
            _dbContext.Reminders.Remove(entity);
            Log.Logger.Information($"Reminder was deleted by user {entity.User.Fullname} at {DateTime.Now}");
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Reminder>> GetAll()
        {
            Log.Logger.Warning($"List of all reminders {_dbContext.Reminders}, _dbContext.Reminders");
            return await _dbContext.Reminders
                .Include(x => x.User)
                .ToListAsync();
        }

        public async Task<Reminder> GetById(int id)
        {
            Log.Logger.Debug($"Remindeders accessed with id: {id} at {DateTime.Now}");
            return await _dbContext.Reminders
                .Include(x => x.User)
                .SingleOrDefaultAsync(s => s.ReminderId == id);
        }

        public async Task Update(Reminder entity)
        {
            Log.Logger.Warning($"The reminder with title {entity.ReminderTitle} is gonna be updated");
            _dbContext.Reminders.Update(entity);
            Log.Logger.Information($"Updated Reminder {entity}");
            await _dbContext.SaveChangesAsync();
        }
    }
}
