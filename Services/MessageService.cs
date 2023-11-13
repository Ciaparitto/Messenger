using messager.models;
using messager.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace messager.Services
{
    public class MessageService : IMessageService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public MessageService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public MessageModel AddMessage(string messagecontent,string reciverid,string CreatorId)
        {

            using (var scope = _scopeFactory.CreateScope())
            {
                var Context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var Message = new MessageModel
                {
                    Content = messagecontent,
                    CreatorId = CreatorId,
                    ReciverId = reciverid
                };
                Context.MessageList.Add(Message);
                Context.SaveChangesAsync();
                return Message;
            }
            
        }
        public async Task<List<MessageModel>> GetMessages(string CreatorId, string ReciverId)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var Context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var Messages = await Context.MessageList.AsNoTracking()
            .Where(x => (x.CreatorId == CreatorId && x.ReciverId == ReciverId) || (x.CreatorId == ReciverId && x.ReciverId == CreatorId))
            .ToListAsync();



                return Messages;
            }
        }
        public async Task<List<MessageModel>> GetMessages(string CreatorId)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var Context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                return await Context.MessageList.Where(x => x.CreatorId == CreatorId).ToListAsync();
            }
            
        }
        }
}
