using _0_Framework.Application.Email;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceHost.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IEmailService emailService;

        public IndexModel(ILogger<IndexModel> logger
            ,IEmailService emailService)
        {
            _logger = logger;
            this.emailService = emailService;
        }

        public void OnGet()
        {
            //emailService.SendEmail("سلام","سلام به پروژه ی نهایی دانشگاهی من خوش آمدید !","Shemuel1226@gmail.com");
        }
    }
}
