namespace covid19_api.Dtos.UserRole
{
    public class UpdateRoleDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        //public string Uid { get; set; } = string.Empty;
        public int? Extends { get; set; }

        public string? Description { get; set; }
    }
}
