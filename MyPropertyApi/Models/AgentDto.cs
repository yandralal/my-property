namespace MyPropertyApi.Models
{
    public class AgentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Contact { get; set; } = "";
        public string Agency { get; set; } = "";
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; } = "";
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; } = "";
        public DateTime? ModifiedDate { get; set; }
    }
}