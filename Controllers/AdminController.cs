using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartFeedBack.Services;
using System;

namespace SmartFeedback.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly AnalyticsService _service;
        public AdminController(AnalyticsService service) => _service = service;

        [HttpGet("analytics")]
        public async Task<IActionResult> Analytics()
            => Ok(await _service.GetAnalyticsAsync());

        [HttpGet("feedback")]
        public async Task<IActionResult> Feedback([FromQuery] string? category, [FromQuery] DateTime? date)
            => Ok(await _service.GetFeedbackByCategoryAndDateAsync(category, date));

        [HttpGet("export")]
        public async Task<IActionResult> Export()
            => Ok(await _service.ExportFeedbackAsync());
    }
}
