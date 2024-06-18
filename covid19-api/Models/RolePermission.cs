namespace covid19_api.Models
{
    public class RolePermission
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int RPid { get; set; }
        public string? Description { get; set; } = string.Empty;
        public List<UserRole>? UserRoles { get; set; }


    }
}
