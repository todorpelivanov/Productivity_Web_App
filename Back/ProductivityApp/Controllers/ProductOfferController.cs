//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.SignalR;
//using ProductivityApp.Services.Implementations;
//using ProductivityApp.Services.Interfaces;

//namespace ProductivityApp.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ProductOfferController : ControllerBase
//    {
//        private IHubContext<MessageHub, IMessageHubClient> messageHub;

//        public ProductOfferController(IHubContext<MessageHub, IMessageHubClient> _messageHub)
//        {
//            messageHub = _messageHub;
//        }

//        [AllowAnonymous]
//        [HttpPost]
//        [Route("productoffers")]
//        //public async Task<ActionResult> Get()
//        //{


//        //    //try
//        //    //{
//        //    ////List<string> offers = new List<string>();
//        //    ////offers.Add("20% Off on basic subscription");
//        //    ////offers.Add("15% Off on pro subscription");
//        //    ////offers.Add("25% Off for yearly subscription");
//        //    //   return await messageHub.Clients.All.SendOffersToUser(offers);
//        //    //    return Ok(messageHub.Clients.All("Offers have been send!"));
//        //    //}
//        //    //catch(Exception ex)
//        //    //{
//        //    //    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
//        //    //}
//        //}
//    //}
//}
