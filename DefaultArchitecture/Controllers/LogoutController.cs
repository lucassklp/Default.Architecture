using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultArchitecture.Controllers
{
    [Route("api/[controller]")]
    public class LogoutController : Controller
    {
        [HttpGet]
        public object Get()
        {
            return new { nome = "HUEHUEH" };
        }

        [HttpPost]
        public object Post()
        {
            return new { Msg = "Successfully Logout" };
        }
    }
}
