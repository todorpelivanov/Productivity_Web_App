﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityApp.Services.Interfaces
{
    public interface IMessageHubClient
    {
        Task SendOffersToUser(List<string> message);
    }
}
