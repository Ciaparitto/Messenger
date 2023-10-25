namespace messager.Services
{
    public interface IMessageService 
    {
        public void AddMessage(string messagecontent, string creatorid, string reciverid);
    }
}
