using Default.Architecture.Views;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Default.Architecture.Senders.Email.Interfaces;
using Default.Architecture.Services;

namespace Default.Architecture.Senders.Email
{
    public class TemplateEmailSender : ITemplateEmailSender
    {
        private IViewRenderService renderService;

        public IEmailSender EmailSender { get; set; }
        
        public TemplateEmailSender(IViewRenderService renderService)
        {
            this.renderService = renderService;
        }

        public void Send<T>(T model) where T : PageModel
        {
            EmailSender.IsBodyHtml = true;
            var templateName = typeof(T).Name;
            templateName = templateName.Remove(templateName.Length - 5);
            EmailSender.Body = renderService.RenderToString(templateName, model);
            EmailSender.Send();
        }

        public void SendAsync<T>(T model) where T : PageModel
        {
            ThreadStart ts = delegate () { Send(model); };
            Thread th = new Thread(ts);
            th.IsBackground = true;
            th.Start();
        }
    }
}
