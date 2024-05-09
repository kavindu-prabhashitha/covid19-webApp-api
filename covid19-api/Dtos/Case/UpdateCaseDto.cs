namespace covid19_api.Dtos.Case
{
    public class UpdateCaseDto
    {
        public int Id { get; set; }

        public DateTime? Date { get; set; }

        public int? Total { get; set; }

        public int? New { get; set; }
    }
}


