namespace SmartFeedBack.Dtos.Feedback
{
    public class FeedbackCreateDto
    {
        public string Category { get; set; }
        public string Content { get; set; }
        public IFormFile? Image { get; set; }
    }
}
