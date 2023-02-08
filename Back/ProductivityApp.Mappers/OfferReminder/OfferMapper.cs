using ProductivityApp.Domain.Entities;
using ProductivityApp.Dtos.OfferDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityApp.Mappers.OfferReminder
{
    public static class OfferMapper
    {
        public static OfferDto ToOfferDto(this OfferToUsers userDb)
        {
            return new OfferDto
            {
                OfferName = userDb.OfferName,
                OfferMessage = userDb.OfferMessage,
            };
        }
    }
}
