using messager.models;

namespace messager.Services.Interfaces
{
    public interface IMessageService
    {
        public Task<string> AddMessage(string messagecontent, string reciverid, string CreatorId);
        public Task<List<MessageModel>> GetMessages(string CreatorId, string ReciverId);
        public Task<List<MessageModel>> GetMessagesByCreator(string CreatorId);
        public  Task<List<MessageModel>> GetMessagesByReceiver(string ReceiverId);
    }
}
