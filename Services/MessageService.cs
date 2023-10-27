using messager.models;
using messager.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Documents;

namespace messager.Services
{
    public class MessageService : IMessageService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<UserModel> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MessageService(AppDbContext context, UserManager<UserModel> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public void AddMessage(string messagecontent,string reciverid)
        {
            var _User = _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var Message = new Message{
               Content = messagecontent,
               Creatorid = _User.Id.ToString(),
               Reciverid = reciverid
           };
            _context.Messages.Add(Message);
            _context.SaveChanges();
        }
    }
}
