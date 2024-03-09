using Microsoft.AspNetCore.Mvc;

namespace Messenger.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessengerController : Controller
    {
        private readonly AppDbContext _Context;

        public MessengerController(AppDbContext context)
        {
            _Context = context;
        }

        [HttpGet]
        [Route("/DisplayImage/{imageId}")]
        public IActionResult DisplayImage(int ImageId)
        {
            var Image = _Context.ImageList.FirstOrDefault(i => i.Id == ImageId);

            if (Image == null)
            {
                return NotFound();
            }

            return File(Image.image, Image.ContentType);
        }
    }
}
