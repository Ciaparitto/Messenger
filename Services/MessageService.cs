using messager.models;
using messager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace messager.Services
{
    public class MessageService : IMessageService
    {      
        private readonly AppDbContext _Context;
        public MessageService(AppDbContext context)
        {          
            _Context = context;
        }

        public async Task<string> AddMessage(string messagecontent, string reciverid, string CreatorId)
        {
            var Message = new MessageModel
            {
                Content = messagecontent,
                CreatorId = CreatorId,
                ReciverId = reciverid
            };

            await _Context.MessageList.AddAsync(Message);
            await _Context.SaveChangesAsync();
            return Message.Id.ToString();
        }
        public async Task<List<MessageModel>> GetMessages(string CreatorId, string ReciverId)
        {
            var Messages = await _Context.MessageList.AsNoTracking()
           .Where(x => (x.CreatorId == CreatorId && x.ReciverId == ReciverId) ||
           (x.CreatorId == ReciverId && x.ReciverId == CreatorId))
           .ToListAsync();

            return Messages;
        }
        public async Task<List<MessageModel>> GetMessages(string CreatorId)
        {
        return await _Context.MessageList.Where(x => x.CreatorId == CreatorId).ToListAsync();           
        }
    }
}
