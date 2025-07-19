using Microsoft.AspNetCore.Identity;
using SmartFeedBack.Dtos.Auth;
using SmartFeedBack.Helpers;
using SmartFeedBack.Models;


namespace SmartFeedBack.Services
{
    public class AuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _config;
        private readonly SignInManager<AppUser> _signInManager;
        public AuthService(UserManager<AppUser> userManager, IConfiguration config, SignInManager<AppUser> signInManager)
            => (_userManager, _config, _signInManager) = (userManager, config, signInManager);

        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto dto)
        {
            var user = new AppUser { UserName = dto.UserName, FullName = dto.FullName, Email = dto.Email };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                throw new Exception(string.Join(",", result.Errors.Select(e => e.Description)));
            await _userManager.AddToRoleAsync(user, AppRoles.User);

            return new RegisterResponseDto
            {
                UserName = user.UserName,
                Email = user.Email
            };
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                throw new Exception("Invalid credentials");
            var roles = await _userManager.GetRolesAsync(user);

            return new LoginResponseDto
            {
                Token = JwtHelper.GenerateJWT(user, roles, _config),
                UserName = user.UserName,
                Email = user.Email,
                FullName = user.FullName,
                Roles = roles.ToArray()
            };
        }
    }
}
