﻿using messager.models;

namespace messager.Services.Interfaces
{
    public interface IMessageGetter
    {
        public Task<List<MessageModel>> GetMessages(string CreatorId, string ReciverId);
        public Task<List<MessageModel>> GetMessagesByCreator(string CreatorId);
        public Task<List<MessageModel>> GetMessagesByReceiver(string ReceiverId);
    }
}
