using covid19_api.Constants;

namespace covid19_api.Dtos.UserRole
{
    public class AddUserRoleDto
    {

        public string Name { get; set; }
        public string Uid{ get; set; } = string.Empty;
        public int? Extends { get; set; }
        public string? Description { get; set; } = string.Empty;
    }
}
