using Business.Exceptions;
using Business.Interfaces;
using DefaultArchitecture.Senders.Email;
using DefaultArchitecture.Senders.Email.Interfaces;
using DefaultArchitecture.Validators;
using DefaultArchitecture.Views;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
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
        private ITemplateEmailSender templateEmailSender;
        private IConfiguration configuration;

        public AccountController(IUserServices userServices, 
            ILogger<AccountController> logger,
            ITemplateEmailSender templateEmailSender,
            IConfiguration configuration)
        {
            this.userServices = userServices;
            this.logger = logger;
            this.templateEmailSender = templateEmailSender;
            this.configuration = configuration;
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
                    var userRegistred = this.userServices.Register(user);

                    var pageModel = new AccountCreatedSuccessfullyModel();
                    pageModel.Name = user.Name;
                    pageModel.Email = user.Email;

                    var emailConfig = EmailConfiguration.GetFromConfiguration(configuration, "No Reply");
                    EmailSender emailSender = new EmailSender(emailConfig);
                    emailSender.To = user.Email;

                    templateEmailSender.EmailSender = emailSender;
                    templateEmailSender.SendAsync(pageModel);

                    return Ok(userRegistred);
                }
                catch(UserExistentException ex)
                {
                    logger.LogInformation(ex.Message);
                    return BadRequest(new { ex.Message,  ex.ErrorCode });
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
