using SGLNU.BLL.DTO;
using SGLNU.BLL.Interfaces;
using SGLNU.DAL.Enteties;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace SGLNU.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserService()
        {

        }
        public UserService(
            UserManager<AppUser> userManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public UserService(
            UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<IdentityResult> CreateUserAsync(UserDTO user, string password)
        {
            var applicationUser = _mapper.Map<AppUser>(user);
            applicationUser.UserName = user.Email;
            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            if (existingUser != null && !(await _userManager.IsEmailConfirmedAsync(existingUser)))
            {
                await _userManager.DeleteAsync(existingUser);
            }

            var result = await _userManager.CreateAsync(applicationUser, password);

            if (result.Succeeded)
            {
                await AddUserToRoleAsync(applicationUser, "NotAppliedMember");
            }

            return result;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<UserDTO> GetByEmailAsync(string email)
        {
            var appLicationUser = await _userManager.FindByEmailAsync(email);

            if (appLicationUser != null)
            {
                return _mapper.Map<UserDTO>(appLicationUser);
            }
            else
            {
                var message = $"User with email {email} couldn`t be found.";
                return null;
            }
        }

        public bool RequireConfirmedAccount()
        {
            return _userManager.Options.SignIn.RequireConfirmedAccount;
        }

        public async Task AddUserToRoleAsync(AppUser user, string role)
        {
            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<UserDTO> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public string GetUserId(ClaimsPrincipal claims)
        {
            return _userManager.GetUserId(claims);
        }
    }
}