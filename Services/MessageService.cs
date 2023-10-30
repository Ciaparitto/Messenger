using messager.models;
using messager.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Documents;
using Microsoft.EntityFrameworkCore;

namespace messager.Services
{
    public class MessageService : IMessageService
    {
        private readonly AppDbContext _Context;
        private readonly UserManager<UserModel> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MessageService(AppDbContext context, UserManager<UserModel> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _Context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public void AddMessage(string messagecontent,string reciverid,string CreatorId)
        {
     
                var Message = new Message
                {
                    Content = messagecontent,
                    Creatorid = CreatorId,
                    Reciverid = reciverid
                };
                _Context.Messages.Add(Message);
                _Context.SaveChangesAsync();
            
        }
        public async Task<List<Message>> GetMessages(string CreatorId, string ReciverId)
        {

            var Messages = await _Context.Messages
            .Where(x => (x.Creatorid == CreatorId && x.Reciverid == ReciverId) || (x.Creatorid == ReciverId && x.Reciverid == CreatorId))
            .ToListAsync();


            
            return Messages;

        }
    }
}
