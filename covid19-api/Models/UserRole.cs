using covid19_api.Constants;
using System.ComponentModel.DataAnnotations;

namespace covid19_api.Models
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public UserRoles Uid { get; set; } = UserRoles.USER;
        public int? Extends { get; set; } 
    }
}
