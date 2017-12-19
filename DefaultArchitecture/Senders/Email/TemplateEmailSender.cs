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
    public class TemplateEmailSender<T> : EmailSender where T : PageModel
    {
        private T pageModel;
        private IViewRenderService renderService;
        private string viewName;
        
        public TemplateEmailSender(EmailConfiguration emailConfiguration, T pageModel, string viewName, IViewRenderService renderService) : base(emailConfiguration)
        {
            this.pageModel = pageModel;
            this.renderService = renderService;
            this.viewName = viewName;
        }

        public void Send()
        {
            new Thread(delegate ()
            {
                base.IsBodyHtml = true;
                base.Body = renderService.RenderToString(viewName, pageModel);
                base.Send();
            }).Start();
        }
    }
}
