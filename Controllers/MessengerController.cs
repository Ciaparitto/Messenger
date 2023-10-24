using messager.Signal.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using messager;

namespace messager.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class MessengerController : Controller
    {
        private readonly IHubContext<AppHub, IAppHub> _hubContext;

        public MessengerController(IHubContext<AppHub, IAppHub> hubContext)
        {
            _hubContext = hubContext;

        }
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            await _hubContext.Clients.All.ToAll("testczydziala");
            return Ok();
        }
    }
}
