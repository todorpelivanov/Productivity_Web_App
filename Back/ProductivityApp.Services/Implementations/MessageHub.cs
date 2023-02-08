using Microsoft.AspNetCore.SignalR;
using ProducitivityApp.DataAccess.Interfaces;
using ProductivityApp.Domain.Entities;
using ProductivityApp.Dtos.OfferDtos;
using ProductivityApp.Dtos.ReminderDtos;
using ProductivityApp.Mappers.OfferReminder;
using ProductivityApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityApp.Services.Implementations
{
    public class MessageHub : Hub<IMessageHubClient>
    {
        private readonly ISendOffersRepository _ISendOffersRepository;

        public MessageHub(ISendOffersRepository iSendOfferRepository) 
        {
            _ISendOffersRepository = iSendOfferRepository;
        }

        public async Task<List<OfferDto>> GetAllOffers(int offerId)
        {
            List<OfferToUsers> offersDb = await _ISendOffersRepository.GetAll();

            List<OfferDto> offersDto = offersDb.Where(x => x.OfferId == offerId).Select(s => s.ToOfferDto()).ToList();
            return offersDto;
        }

        //public async Task SendOffersToUser(List<string> message)
        //{
        //    await Clients.All.SendOffersToUser(message);
        //}
    }
}
