namespace covid19_api.Dtos.UserRole
{
    public class AddPermissionsToUserRoleDto
    {
        public int UserRoleId { get; set; }

        public List<int> PermissionIds { get; set; } = new List<int>();
    }
}
