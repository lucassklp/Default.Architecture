using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Default.Architecture.Views
{
    public class AccountCreatedSuccessfullyModel : PageModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        

        public void OnGet()
        {
        }
    }
}