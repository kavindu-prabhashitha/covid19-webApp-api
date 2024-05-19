using covid19_api.Constants;
using System.ComponentModel.DataAnnotations;

namespace covid19_api.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[0] ;
        public byte[] PasswordSalt { get; set; } = new byte[0];
        public UserRoles Role { get; set; } = UserRoles.USER;
    }
}