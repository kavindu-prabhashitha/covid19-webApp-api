namespace covid19_api.Models
{
    public class RolePermission
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string RPid { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public List<UserRole>? UserRoles { get; set; }


    }
}
