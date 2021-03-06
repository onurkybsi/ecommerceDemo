using Infrastructure.Data;

namespace ecommerceDemo.Data.Model
{
    public class User : MongoDBEntity, Infrastructure.Service.IUser, IEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Token { get; set; }
        public string Role { get; set; } = Infrastructure.Service.Constants.JwtAuthenticationService.UserRole.User;
    }
}