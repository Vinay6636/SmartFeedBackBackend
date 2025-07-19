using System;
namespace SmartFeedBack.Models
{

    public class Feedback
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string Category { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}