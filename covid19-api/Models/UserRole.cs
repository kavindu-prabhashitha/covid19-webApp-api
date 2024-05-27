using System.ComponentModel.DataAnnotations;

namespace covid19_api.Models
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Uid { get; set; } = "ANONYMOUS";
        public int? Extends { get; set; } 
        public List<RolePermission>? RolePermissions { get; set; }
    }
}
