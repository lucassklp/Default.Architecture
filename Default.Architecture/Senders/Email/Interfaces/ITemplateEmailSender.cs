using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultArchitecture.Senders.Email.Interfaces
{
    public interface ITemplateEmailSender
    {
        IEmailSender EmailSender { get; set; }
        void Send<T>(T model) where T : PageModel;
        void SendAsync<T>(T model) where T : PageModel;
    }
}
