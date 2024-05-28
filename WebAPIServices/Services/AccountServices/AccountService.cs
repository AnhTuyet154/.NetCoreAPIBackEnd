using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAPIServices.Dto.Account;
using WebAPIServices.Models;

namespace WebAPIServices.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountService(UserManager<Account> userManager, SignInManager<Account> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<NewAccountDto> RegisterAsync(RegisterDto registerDto)
        {
            if (registerDto == null)
                throw new ArgumentNullException(nameof(registerDto));

            var appUser = new Account
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email
            };

            var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

            if (createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                if (!roleResult.Succeeded)
                    throw new Exception("Failed to add role");

                // Set the default role to "User"
                string userRole = "User";

                return new NewAccountDto
                {
                    UserName = appUser.UserName,
                    Email = appUser.Email,
                    Token = _tokenService.CreateToken(appUser, userRole) // Pass the userRole parameter
                };
            }

            throw new Exception(string.Join(", ", createdUser.Errors));
        }


        public async Task<NewAccountDto> LoginAsync(LoginDto loginDto)
        {
            if (loginDto == null)
                throw new ArgumentNullException(nameof(loginDto));

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if (user == null) throw new UnauthorizedAccessException("Invalid username!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) throw new UnauthorizedAccessException("Username not found and/or password incorrect");

            var roles = await _userManager.GetRolesAsync(user);
            string userRole = roles.FirstOrDefault(); // Lấy vai trò của người dùng

            return new NewAccountDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user, userRole) // Truyền vai trò vào phương thức CreateToken
            };
        }

    }
}
    
