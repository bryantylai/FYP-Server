using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApolloAPI.Authorization;
using ApolloAPI.Data.Client.Form;
using ApolloAPI.Data.Utility;
using ApolloAPI.Models;
using ApolloAPI.Services;

namespace ApolloAPI.Controllers.Client
{
    [RoutePrefix("api/client/auth")]
    public class AuthController : AbstractController
    {
        [Route("register")]
        [HttpPost]
        public ServerMessage Register([FromBody] RegistrationForm registrationForm)
        {
            if (authService.ValidateForm(registrationForm))
            {
                if (!authService.CheckForDuplicate(registrationForm))
                {
                    if (authService.RegisterUser(registrationForm))
                    {
                        return new ServerMessage() { IsError = false };
                    }

                    return new ServerMessage() { IsError = true, Message = "An unknown error has occured." };
                }

                return new ServerMessage() { IsError = true, Message = "The existing username or email already exists." };
            }

            return new ServerMessage() { IsError = true, Message = "There is empty fields in the Registration form." };
        }

        [Route("login")]
        [HttpPost]
        public LoginMessage Login([FromBody] LoginForm loginForm)
        {
            if (authService.ValidateForm(loginForm))
            {
                if (authService.LoginUser(loginForm))
                {
                    return authService.CheckIfNewAccount(loginForm.Username) ? new LoginMessage() { IsError = false, NewAccount = true } : new LoginMessage() { IsError = false, NewAccount = false };
                }
                else
                {
                    return new LoginMessage() { IsError = true, Message = "Useranme or Password entered is incorrect", NewAccount = false };
                }
            }

            return new LoginMessage() { IsError = true, Message = "There is empty fields in the Login form.", NewAccount = false };
        }
    }
}
