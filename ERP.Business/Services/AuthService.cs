using ERP.Core.DTOs.AuthModels;
using ERP.Core.Entities;
using ERP.Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;


namespace ERP.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _configuration;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtService jwtService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _configuration = configuration;
        }

        public async Task<AuthResult> LoginAsync(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username) ?? 
                       await _userManager.FindByEmailAsync(model.Username);

            if (user == null)
            {
                return new AuthResult
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
            {
                return new AuthResult
                {
                    Success = false,
                    Message = "Invalid credentials"
                };
            }

            // Get user roles and add to the user object
            var roles = await _userManager.GetRolesAsync(user);
            user.Roles = roles.ToList();

            // Generate tokens
            var token = _jwtService.GenerateJwtToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            // Save refresh token to user
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(
                Convert.ToInt32(_configuration["JWT:RefreshTokenValidityInDays"]));
            await _userManager.UpdateAsync(user);

            return new AuthResult
            {
                Success = true,
                Message = "Authentication successful",
                Token = token,
                RefreshToken = refreshToken,
                User = user
            };
        }

        public async Task<AuthResult> RegisterAsync(RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username) != null ||
                            await _userManager.FindByEmailAsync(model.Email) != null;

            if (userExists)
            {
                return new AuthResult
                {
                    Success = false,
                    Message = "User already exists"
                };
            }

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                
                return new AuthResult
                {
                    Success = false,
                    Message = $"User creation failed: {string.Join(", ", result.Errors.Select(e => e.Description))}"
                };
            }

            return new AuthResult
            {
                Success = true,
                Message = "User created successfully",
                User = user
            };
        }

        public async Task<AuthResult> RefreshTokenAsync(RefreshTokenModel model)
        {
            // Validate token
            ClaimsPrincipal principal;
            try
            {
                principal = _jwtService.GetPrincipalFromExpiredToken(model.Token);
            }
            catch (Exception ex)
            {
                return new AuthResult
                {
                    Success = false,
                    Message = $"Invalid token: {ex.Message}"
                };
            }

            // Get user from token
            var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new AuthResult
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            // Validate refresh token
            if (user.RefreshToken != model.RefreshToken || 
                user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return new AuthResult
                {
                    Success = false,
                    Message = "Invalid or expired refresh token"
                };
            }

            // Get user roles
            var roles = await _userManager.GetRolesAsync(user);
            user.Roles = roles.ToList();

            // Generate new tokens
            var newToken = _jwtService.GenerateJwtToken(user);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            // Update refresh token
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(
                Convert.ToInt32(_configuration["JWT:RefreshTokenValidityInDays"]));
            await _userManager.UpdateAsync(user);

            return new AuthResult
            {
                Success = true,
                Message = "Token refreshed successfully",
                Token = newToken,
                RefreshToken = newRefreshToken,
                User = user
            };
        }

        public async Task<ApplicationUser?> GetUserAsync(ClaimsPrincipal userClaimsPrincipal)
        {
            var userIdentifier = userClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userIdentifier)) return null;


            return await _userManager.FindByNameAsync(userIdentifier);
        }

        public async Task<AuthResult> LogoutAsync(string username)
        {
            // Find user by username or email
            var user = await _userManager.FindByNameAsync(username) ?? 
                       await _userManager.FindByEmailAsync(username);

            if (user == null)
            {
                return new AuthResult
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            // Invalidate refresh token
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            return new AuthResult
            {
                Success = true,
                Message = "Logged out successfully"
            };
        }

        public async Task<AuthResult> ChangePasswordAsync(string username, ChangePasswordModel model)
        {
            // Find user by username or email
            var user = await _userManager.FindByNameAsync(username) ?? 
                       await _userManager.FindByEmailAsync(username);

            if (user == null)
            {
                return new AuthResult
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            // Verify current password
            if (!await _userManager.CheckPasswordAsync(user, model.CurrentPassword))
            {
                return new AuthResult
                {
                    Success = false,
                    Message = "Current password is incorrect"
                };
            }

            // Change password
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                return new AuthResult
                {
                    Success = false,
                    Message = $"Password change failed: {string.Join(", ", result.Errors.Select(e => e.Description))}"
                };
            }

            // Invalidate refresh token to force re-login with new credentials
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            return new AuthResult
            {
                Success = true,
                Message = "Password changed successfully. Please login again with your new password."
            };
        }
    }
}