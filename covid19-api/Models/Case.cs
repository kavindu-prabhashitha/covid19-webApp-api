using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace covid19_api.Models
{
    public class Case
    {
        [Key]
        public int Id { get; set; }

        public DateTime? Date { get; set; }

        public int? Total { get; set; }

        public int? New { get; set; }

        [ForeignKey("CountryData")]
        public int CountryDataId { get; set; }

    }
}
