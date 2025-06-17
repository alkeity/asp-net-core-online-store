using OnlineStore.Models.DTO;

namespace OnlineStore.Services
{
    public interface IUserService
    {
        public UserDTO? GetUser(long id);
        public UserDTO? GetUser(string email, string password);
        public void AddUser(UserDTO user);
    }
}
