using messager.models;

namespace messager.Services.Interfaces
{
    public interface IMessageService
    {
        public void AddMessage(string messagecontent, string reciverid, string CreatorId);
        public Task<List<Message>> GetMessages(string CreatorId, string ReciverId);
    }
}
