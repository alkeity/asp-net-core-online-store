using OnlineStore.Data.Models;

namespace OnlineStore.Models.DTO
{
    public class UserDTO
    {
        public required string Email { get; set; }
        public string? Username { get; set; }
        public required string Password { get; set; }
        public DateTime CreatedAt { get; set; }

        public static UserDTO FromEntity(User user)
        {
            return new UserDTO()
            {
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                CreatedAt = user.CreatedAt
            };
        }

        public static User ToEntity(UserDTO user)
        {
            return new User()
            {
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                CreatedAt = user.CreatedAt
            };
        }

    }
}
