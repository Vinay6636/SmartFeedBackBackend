using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartFeedBack.Dtos.Feedback;
using SmartFeedBack.Services;

namespace SmartFeedBack.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackService _service;
        public FeedbackController(FeedbackService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> Submit([FromForm] FeedbackCreateDto dto)
            => Ok(await _service.SubmitFeedbackAsync(User, dto));

        [HttpGet("my")]
        public async Task<IActionResult> MyFeedback()
            => Ok(await _service.GetUserFeedbackAsync(User));
    }
}
