using OnlineStore.Data;
using OnlineStore.Data.Models;
using OnlineStore.Models.DTO;

namespace OnlineStore.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;
        public UserService(AppDbContext db)
        {
            _db = db;
        }

        public void AddUser(UserDTO user)
        {
            if (user.Email == null)
                throw new ArgumentNullException("User email is not specified");

            if (user.Username == null)
                throw new ArgumentNullException("User first name is not specified");

            if (user.Password == null)
                throw new ArgumentNullException("User password is not specified");

            _db.Users.Add(UserDTO.ToEntity(user));
            _db.SaveChanges();
        }

        public UserDTO? GetUser(long id)
        {
            return UserDTO.FromEntity(_db.Users.First(user => user.Id == id)) ?? null;
        }

        public UserDTO? GetUser(string email, string password)
        {
            return UserDTO.FromEntity(_db.Users.First(user => user.Email == email && user.Password == password)) ?? null;
        }
    }
}
