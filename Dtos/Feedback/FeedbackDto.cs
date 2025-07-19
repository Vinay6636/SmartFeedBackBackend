namespace SmartFeedBack.Dtos.Feedback
{
    public class FeedbackDto
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
