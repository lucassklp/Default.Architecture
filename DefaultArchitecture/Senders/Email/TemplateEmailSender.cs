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
        private IViewRenderService renderService;
        private string viewName;

        public TemplateEmailSender(EmailConfiguration emailConfiguration, PageModel pageModel, string viewName, IViewRenderService renderService) : base(emailConfiguration)
        {
            this.pageModel = pageModel;
            this.renderService = renderService;
            this.viewName = viewName;
        }

        public async override Task Send()
        {
            base.IsBodyHtml = true;
            base.Body = await renderService.RenderToStringAsync(viewName, pageModel);
            await base.Send();
        }

    }
}
