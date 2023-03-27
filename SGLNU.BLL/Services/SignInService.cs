using SGLNU.BLL.DTO;
using SGLNU.BLL.Interfaces;
using SGLNU.DAL.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SGLNU.BLL.Services
{
    public class SignInService : ISignInService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public SignInService()
        {

        }
        public SignInService(
            SignInManager<AppUser> signInManager,
            IMapper mapper)
        {
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public SignInService(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;

        }

        public async Task<List<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync()
        {
            return (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public bool IsSignedIn(ClaimsPrincipal claimsPrincipal)
        {
            return _signInManager.IsSignedIn(claimsPrincipal);
        }

        public Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return _signInManager.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
        }

        public Task SignInAsync(UserDTO user, bool isPersistent)
        {
            var applicationUser = _mapper.Map<AppUser>(user);
            return _signInManager.SignInAsync(applicationUser, isPersistent: false);
        }

        public Task SignOutAsync()
        {
            return _signInManager.SignOutAsync();
        }
    }
}