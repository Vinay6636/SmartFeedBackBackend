namespace SmartFeedBack.Dtos.Admin
{
    public class ExportFeedbackDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Category { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
