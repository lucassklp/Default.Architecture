using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DefaultArchitecture.Views
{
    public class AccountCreatedSuccessfullyModel : PageModel
    {
        public string Name { get; private set; } = "Teste";
        public string Email { get; private set; } = "Oi";

        public AccountCreatedSuccessfullyModel(string name, string email) : base()
        {

        }

        public void OnGet()
        {
        }
    }
}