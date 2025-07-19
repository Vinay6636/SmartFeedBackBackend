namespace SmartFeedBack.Dtos.Feedback
{
    public class FeedbackAnalyticsDto
    {
        public int TotalFeedbacks { get; set; }
        public Dictionary<string, int> FeedbacksByCategory { get; set; }
        public Dictionary<DateTime, int> FeedbacksByDate { get; set; }
    }
}
