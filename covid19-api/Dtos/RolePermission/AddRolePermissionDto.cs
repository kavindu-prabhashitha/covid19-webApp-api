namespace covid19_api.Dtos.RolePermission
{
    public class AddRolePermissionDto
    {
        public string Name { get; set; } = string.Empty;
        public string RPid { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}
