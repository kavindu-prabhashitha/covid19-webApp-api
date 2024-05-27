namespace covid19_api.Dtos.UserRole
{
    public class UpdateUserRoleDto
    {
        public string? Name { get; set; }
        public string? Uid { get; set; } = string.Empty;
        public int? Extends { get; set; }
    }
}
