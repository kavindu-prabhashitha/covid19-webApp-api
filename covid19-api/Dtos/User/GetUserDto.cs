using covid19_api.Dtos.UserRole;

namespace covid19_api.Dtos.User
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;

        public GetUserRoleDto? Role { get; set; }
    }
}
