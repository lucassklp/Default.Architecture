using DefaultArchitecture.Services.Exceptions;
using DefaultArchitecture.Services.Interfaces;
using DefaultArchitecture.Validators;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultArchitecture.Controllers
{
    //[Authorize(Roles = "Administrador")]
    [AllowAnonymous]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private IUserServices userServices;
        private ILogger logger;

        public AccountController(IUserServices userServices, ILogger<AccountController> logger)
        {
            this.userServices = userServices;
            this.logger = logger;
        }

        [HttpPost]
        public IActionResult Register([FromBody] User user)
        {
            //Validate the user
            var validation = new RegisterUserValidation();
            var results = validation.Validate(user);
            
            //If if valid, then register
            if (results.IsValid)
            {
                try
                {
                    return Ok(this.userServices.Register(user));
                }
                catch(UserExistentException ex)
                {
                    return BadRequest(new Error<UserExistentException>(ex));
                }
                catch(Exception ex)
                {
                    logger.LogError(ex, ex.Message);
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            else return BadRequest(results.Errors);
        }
    }
}
