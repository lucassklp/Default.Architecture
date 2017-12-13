using DefaultArchitecture.Views;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace DefaultArchitecture.Senders.Email
{
    public class TemplateEmailSender : EmailSender
    {
        private PageModel pageModel;
        IViewRenderService renderService;
        public TemplateEmailSender(EmailConfiguration emailConfiguration, PageModel pageModel, IViewRenderService renderService) : base(emailConfiguration)
        {
            this.pageModel = pageModel;
            this.renderService = renderService;
        }


        private void a()
        {
            
        }

        public async override Task Send()
        {
            base.IsBodyHtml = true;
            base.Body = await renderService.RenderToStringAsync("AccountCreatedSuccessfully", pageModel);
            int a = 1 + 1;
            //await base.Send();
        }

    }
}
