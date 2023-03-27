using SGLNU.BLL.DTO;
using SGLNU.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SGLNU.BLL.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> CreateUserAsync(UserDTO user, string password);

        Task<UserDTO> GetByEmailAsync(string email);

        Task AddUserToRoleAsync(AppUser user, string role);

        Task<UserDTO> GetByIdAsync(string id);

        Task<IdentityResult> ConfirmEmailAsync(string userId, string token);

        Task<string> GenerateEmailConfirmationTokenAsync(string userId);

        bool RequireConfirmedAccount();

        string GetUserId(ClaimsPrincipal claims);

    }
}