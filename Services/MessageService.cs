using messager.models;

namespace messager.Services
{
    public class MessageService : IMessageService
    {
        private readonly AppDbContext _context;
        public MessageService(AppDbContext context) 
        {
            _context = context;
        }
        public void AddMessage(string messagecontent,string creatorid,string reciverid)
        {
           var Message = new Message{
               Content = messagecontent,
               Creatorid = creatorid,
               Reciverid = reciverid
           };
            _context.Messages.Add(Message);
            _context.SaveChanges();
        }
    }
}
