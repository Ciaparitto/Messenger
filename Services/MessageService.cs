using messager.models;
using messager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace messager.Services
{
    public class MessageService : IMessageService
    {
        private readonly AppDbContext _Context;
        public MessageService(AppDbContext Context)
        {
            _Context = Context;
        }

        public async Task<string> AddMessage(string MessageContent, string ReceiverId, string CreatorId)
        {
            var Message = new MessageModel
            {
                Content = MessageContent,
                CreatorId = CreatorId,
                ReciverId = ReceiverId
            };

            await _Context.MessageList.AddAsync(Message);
            await _Context.SaveChangesAsync();
            return Message.Id.ToString();
        }

    }
}
