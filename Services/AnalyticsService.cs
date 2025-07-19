using Microsoft.EntityFrameworkCore;
using SmartFeedBack.Data;
using SmartFeedBack.Dtos.Admin;
using SmartFeedBack.Dtos.Feedback;


namespace SmartFeedBack.Services
{
    public class AnalyticsService
    {
        private readonly AppDbContext _db;

        public AnalyticsService(AppDbContext db)
            => _db = db;

        public async Task<FeedbackAnalyticsDto> GetAnalyticsAsync()
        {
            var feedbacks = await _db.Feedbacks.ToListAsync();
            return new FeedbackAnalyticsDto
            {
                TotalFeedbacks = feedbacks.Count,
                FeedbacksByCategory = feedbacks.GroupBy(f => f.Category)
                    .ToDictionary(g => g.Key, g => g.Count()),
                FeedbacksByDate = feedbacks.GroupBy(f => f.CreatedAt.Date)
                    .ToDictionary(g => g.Key, g => g.Count())
            };
        }

        public async Task<List<ExportFeedbackDto>> GetFeedbackByCategoryAndDateAsync(string? cat, DateTime? date)
        {
            var query = _db.Feedbacks.Include(f => f.User).AsQueryable();
            if (!string.IsNullOrEmpty(cat))
                query = query.Where(f => f.Category == cat);
            if (date.HasValue)
                query = query.Where(f => f.CreatedAt.Date == date.Value.Date);

            return await query.OrderByDescending(f => f.CreatedAt)
                .Select(f => new ExportFeedbackDto
                {
                    Id = f.Id,
                    UserName = f.User.UserName,
                    Category = f.Category,
                    Content = f.Content,
                    CreatedAt = f.CreatedAt
                }).ToListAsync();
        }

        public async Task<List<ExportFeedbackDto>> ExportFeedbackAsync()
        {
            // For demo, just return all feedbacks
            return await _db.Feedbacks.Include(f => f.User)
                .OrderByDescending(f => f.CreatedAt)
                .Select(f => new ExportFeedbackDto
                {
                    Id = f.Id,
                    UserName = f.User.UserName,
                    Category = f.Category,
                    Content = f.Content,
                    CreatedAt = f.CreatedAt
                }).ToListAsync();
        }
    }
}
