using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartFeedBack.Data;
using SmartFeedBack.Dtos.Feedback;
using SmartFeedBack.Helpers;
using SmartFeedBack.Models;
using System.Security.Claims;


namespace SmartFeedBack.Services
{
    public class FeedbackService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;

        public FeedbackService(AppDbContext db, UserManager<AppUser> userManager)
            => (_db, _userManager) = (db, userManager);

        public async Task<FeedbackDto> SubmitFeedbackAsync(ClaimsPrincipal userPrincipal, FeedbackCreateDto dto)
        {
            var userId = _userManager.GetUserId(userPrincipal);
            var feedback = new Feedback
            {
                UserId = userId,
                Category = dto.Category,
                Content = dto.Content,
                ImagePath = await FileUploadHelper.SaveFileAsync(dto.Image),
                CreatedAt = DateTime.UtcNow
            };
            _db.Feedbacks.Add(feedback);
            await _db.SaveChangesAsync();

            return new FeedbackDto
            {
                Id = feedback.Id,
                Category = feedback.Category,
                Content = feedback.Content,
                ImageUrl = feedback.ImagePath,
                CreatedAt = feedback.CreatedAt
            };
        }

        public async Task<List<FeedbackDto>> GetUserFeedbackAsync(ClaimsPrincipal userPrincipal)
        {
            var userId = _userManager.GetUserId(userPrincipal);
            return await _db.Feedbacks
                .Where(f => f.UserId == userId)
                .OrderByDescending(f => f.CreatedAt)
                .Select(f => new FeedbackDto
                {
                    Id = f.Id,
                    Category = f.Category,
                    Content = f.Content,
                    ImageUrl = f.ImagePath,
                    CreatedAt = f.CreatedAt
                })
                .ToListAsync();
        }
    }
}
