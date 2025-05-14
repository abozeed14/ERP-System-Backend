

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Collection of roles assigned to the user
        /// </summary>
        [NotMapped]
        public ICollection<string> Roles { get; set; } = new HashSet<string>();

        /// <summary>
        /// Refresh token for JWT authentication
        /// </summary>
        public string? RefreshToken { get; set; }

        /// <summary>
        /// Expiry time for the refresh token
        /// </summary>
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
