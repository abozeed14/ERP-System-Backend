using ERP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ERP.Core.Interfaces.Services
{
    public interface IJwtService
    {
        /// <summary>
        /// Generates a JWT token for the specified user
        /// </summary>
        /// <param name="user">The application user</param>
        /// <returns>JWT token string</returns>
        string GenerateJwtToken(ApplicationUser user);

        /// <summary>
        /// Generates a refresh token for the user
        /// </summary>
        /// <returns>Refresh token string</returns>
        string GenerateRefreshToken();

        /// <summary>
        /// Gets principal claims from an expired token
        /// </summary>
        /// <param name="token">The expired JWT token</param>
        /// <returns>ClaimsPrincipal if token is valid but expired, null otherwise</returns>
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

        /// <summary>
        /// Validates a token
        /// </summary>
        /// <param name="token">The JWT token to validate</param>
        /// <returns>True if token is valid, false otherwise</returns>
        bool ValidateToken(string token);
    }
}