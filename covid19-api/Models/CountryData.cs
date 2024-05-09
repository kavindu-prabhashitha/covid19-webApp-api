using System.ComponentModel.DataAnnotations;

namespace covid19_api.Models
{
    public class CountryData
    {
        [Key]
        public int Id { get; set; }

        public string Country { get; set; }

        public string Region { get; set; }

        public List<Case> Cases { get; set; }
    }
}
