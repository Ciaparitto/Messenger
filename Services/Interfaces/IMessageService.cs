using messager.models;

namespace messager.Services.Interfaces
{
    public interface IMessageService
    {
        public Task<string> AddMessage(string MessageContent, string ReciverId, string CreatorId);

    }
}
