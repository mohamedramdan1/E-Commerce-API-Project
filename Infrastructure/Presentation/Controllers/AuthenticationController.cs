using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityDtos;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager _serviceManager) : ApiBaseController
    {
        // Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTo>> Login(LoginDTo loginDTo)
        {
            var User = await _serviceManager.AuthenticationService.LoginAsync(loginDTo);
            return Ok(User);
        }

        // Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTo>> Register(RegisterDTo registerDTo)
        {
            var User = await _serviceManager.AuthenticationService.RegisterAsync(registerDTo);
            return Ok(User);
        }

        // Check Email
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var Result = await _serviceManager.AuthenticationService.CheckEmailAsync(email);
            return Ok(Result);
        }

        // Get Current User 
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<bool>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Result = await _serviceManager.AuthenticationService.GetCurrentUserAsync(email!);
            return Ok(Result);
        }

        // Get Current User Address
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDTo>> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Result = await _serviceManager.AuthenticationService.GetCurrentUserAddressAsync(email!);
            return Ok(Result);
        }

        // Update Current User Address
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDTo>> UpdateCurrentUserAddress(AddressDTo addressDTo)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var Result = await _serviceManager.AuthenticationService.UpdateCurrentUserAddressAsync(addressDTo ,email!);
            return Ok(Result);
        }

    }
}
