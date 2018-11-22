using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultArchitecture.Senders.Email
{
    public class EmailConfiguration
    {
        public string Name { get; set; }
        public string Sender { get; set; }
        public string Password { get; set; }
        public SMTP SMTP { get; set; }


        public static EmailConfiguration GetFromConfiguration(IConfiguration configuration, string name)
        {
            return configuration.GetSection("Emails").Get<List<EmailConfiguration>>().Find(x => x.Name == name);
        }

    }
}
