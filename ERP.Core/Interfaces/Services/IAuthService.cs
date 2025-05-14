using ERP.Core.DTOs.AuthModels;
using ERP.Core.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ERP.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(LoginModel model);
        Task<AuthResult> RegisterAsync(RegisterModel model);
        Task<AuthResult> RefreshTokenAsync(RefreshTokenModel model);
        Task<ApplicationUser?> GetUserAsync(ClaimsPrincipal userClaimsPrincipal);
        Task<AuthResult> LogoutAsync(string username);
        Task<AuthResult> ChangePasswordAsync(string username, ChangePasswordModel model);
    }

    /// <summary>
    /// Authentication result containing token information and status
    /// </summary>
    public class AuthResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public ApplicationUser User { get; set; }
    }
}