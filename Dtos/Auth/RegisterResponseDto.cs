namespace SmartFeedBack.Dtos.Auth
{
    public class RegisterResponseDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; } = "Registration successful";
    }
}
