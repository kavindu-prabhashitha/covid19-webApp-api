using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace covid19_api.Models
{
    public class UserRefreshToken
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string RefreshToken { get; set; } = string.Empty ;

        public bool IsActive { get; set; } = true;

        public DateTime TokenExpireDateTime { get; set; } = new DateTime(2024,1,1);


    }
}
