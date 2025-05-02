using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects.IdentityDtos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServiceAbstraction
{
    public interface IAuthenticationService
    {
        // Login
        // Take Email and Password Then Return Token ,  Email and DisplayName To Client  
        Task<UserDTo> LoginAsync(LoginDTo loginDTo);

        //Register
        //Take Email , Password  , UserName , Display Name And Phone Number Then Return Token
        //, Email and Display Name To Client
        Task<UserDTo> RegisterAsync(RegisterDTo registerDTo);

        //Check Email
        //Take string Email Then Return boolean To Client
        Task<bool> CheckEmailAsync(string Email);

        //Get Current User Address
        //Take string Email Then Return Address[AddressDTo] of Current Logged in User To Client  
        Task<AddressDTo> GetCurrentUserAddressAsync(string Email);

        //Update Current User Address
        // Take Updated Address[AddressDTo] and Email Then Return Address[AddressDTo] after Update To Client  
        Task<AddressDTo> UpdateCurrentUserAddressAsync(AddressDTo addressDTo , string Email);

        //Get Current User
        //Take string Email Then Return Token , Email and Display Name To Client[UserDTo]
        Task<UserDTo> GetCurrentUserAsync(string Email);

    }
}
