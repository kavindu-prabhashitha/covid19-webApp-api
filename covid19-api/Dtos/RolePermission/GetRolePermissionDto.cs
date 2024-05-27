namespace covid19_api.Dtos.RolePermission
{
    public class GetRolePermissionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string RPid { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}
