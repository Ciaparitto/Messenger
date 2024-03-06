using messager.models;

namespace messager.Services.Interfaces
{
    public interface IUserGetter
    {
        public Task<UserModel> GetLoggedUser();
        public Task<UserModel> GetUserById(string Id);
        public Task<List<UserModel>> GetUsers(string CreatorId);

    }
}
