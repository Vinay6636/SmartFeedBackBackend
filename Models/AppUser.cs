using Microsoft.AspNetCore.Identity;
namespace SmartFeedBack.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<Feedback> Feedbacks { get; set; }
        public string FullName { get; set; }
    }

}
