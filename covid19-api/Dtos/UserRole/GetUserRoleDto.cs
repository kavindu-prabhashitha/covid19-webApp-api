using covid19_api.Dtos.RolePermission;

namespace covid19_api.Dtos.UserRole
{
    public class GetUserRoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Uid { get; set; } = string.Empty;
        public int? Extends { get; set; }

        public List<GetRolePermissionDto>? RolePermissions { get; set;}
    }
}
