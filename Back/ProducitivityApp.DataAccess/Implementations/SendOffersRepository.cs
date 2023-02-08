using Microsoft.EntityFrameworkCore;
using ProducitivityApp.DataAccess.Interfaces;
using ProductivityApp.Domain.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducitivityApp.DataAccess.Implementations
{
    public class SendOffersRepository : ISendOffersRepository
    {
        private readonly ProductivityAppDbContext _dbContext;

        public SendOffersRepository(ProductivityAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async System.Threading.Tasks.Task Add(OfferToUsers entity)
        {
            _dbContext.Offers.Add(entity);
            Log.Logger.Information("Offer has been added!");
            await _dbContext.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task Delete(OfferToUsers entity)
        {
            _dbContext.Offers.Remove(entity);
            Log.Logger.Warning("Offer has been deleted!");
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<OfferToUsers>> GetAll()
        {
            return await _dbContext.Offers
                .ToListAsync();
        }

        public async Task<OfferToUsers> GetById(int id)
        {
            return await _dbContext.Offers
                .SingleOrDefaultAsync(s => s.OfferId == id);
        }

        public async System.Threading.Tasks.Task Update(OfferToUsers entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
