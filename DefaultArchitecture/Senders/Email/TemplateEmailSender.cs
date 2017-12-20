using DefaultArchitecture.Senders.Email.Interfaces;
using DefaultArchitecture.Services;
using DefaultArchitecture.Views;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;

namespace DefaultArchitecture.Senders.Email
{
    public class TemplateEmailSender<T> : ITemplateEmailSender<T>
        where T : PageModel
    {
        private IViewRenderService renderService;

        public IEmailSender EmailSender { get; set; }
        public T PageModel { get; set; }
        
        public TemplateEmailSender(IViewRenderService renderService)
        {
            this.renderService = renderService;
        }

        public void Send()
        {
            EmailSender.IsBodyHtml = true;
            var templateName = typeof(T).Name;
            EmailSender.Body = renderService.RenderToString(templateName.Substring(templateName.Length - 5), PageModel);
            EmailSender.Send();
        }

        public void SendAsynchronous()
        {
            new Thread(this.Send).Start();
        }
    }
}
