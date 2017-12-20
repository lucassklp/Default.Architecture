using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultArchitecture.Senders.Email.Interfaces
{
    public interface ITemplateEmailSender<T> : ISender, ISenderAsynchronous
        where T : PageModel
    {
    }
}
