using messager.models;

namespace messager.Services.Interfaces
{
    public interface IUserService
    {
        public  void Register(string username, string password, string Email);
        public  void Login(string username, string password);
        public  void Logout();
        public Task<UserModel> GetLoggedUser();
        public Task<UserModel> GetUserById(string id);
        public Task<List<UserModel>> GetUsers(string CreatorId);
        public Task<List<UserModel>> GetUsersByIds(IEnumerable<string> userIds);

    }
}
