using ProductivityApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductivityApp.Tests.ReminderServiceTests
{
    public class ReminderGeneralTest
    {
        public class ReminderGeneralTests
        {
            private readonly IReminderService _reminderService;

            public ReminderGeneralTests(IReminderService reminderService)
            {
                _reminderService= reminderService;
            }

            [Fact]
            public async void IsGetAllRemindersEmpty()
            {
                var result = await _reminderService.GetAllReminderWithPagination(10, -12, 2 );

                Assert.NotEmpty(result);

            }
        }
    }
}
