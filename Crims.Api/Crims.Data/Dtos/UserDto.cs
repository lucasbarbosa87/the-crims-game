using Crims.Data.Entities;
using Newtonsoft.Json;

namespace Crims.Data.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        [JsonIgnore]
        public UserRolesEntity UserRole { get; set; }
    }
}
