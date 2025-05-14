using ERP.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ERP.Core.DTOs.AuthModels;

namespace ERP.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);

            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(model);

            if (!result.Success)
                return Unauthorized(new { message = result.Message });

            return Ok(new
            {
                token = result.Token,
                refreshToken = result.RefreshToken,
                message = result.Message
            });
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RefreshTokenAsync(model);

            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new
            {
                token = result.Token,
                refreshToken = result.RefreshToken,
                message = result.Message
            });
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            // Get the current user's username from claims
            var username = User.Identity?.Name;
            
            if (string.IsNullOrEmpty(username))
                return BadRequest(new { message = "User not authenticated" });

            // Invalidate the refresh token
            var result = await _authService.LogoutAsync(username);

            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Get the current user's username from claims
            var username = User.Identity?.Name;
            
            if (string.IsNullOrEmpty(username))
                return BadRequest(new { message = "User not authenticated" });

            var result = await _authService.ChangePasswordAsync(username, model);

            if (!result.Success)
                return BadRequest(new { message = result.Message });

            return Ok(new { message = result.Message });
        }
    }

   
}