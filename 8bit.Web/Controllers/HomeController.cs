using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EightBit.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;

        public HomeController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(string name, string subject, string email, string message)
        {
            var apiKey = configuration.GetValue<string>("Sendgrid:ApiKey");
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(email, name),
                Subject = subject,
                HtmlContent = "<strong>" + message + "</strong>",
            };
            msg.AddTo(new EmailAddress("anton-putau@eightbitteam.com", "Anton Putau"));
            msg.AddCc(new EmailAddress("anton-laptev@eightbitteam.com", "Anton Laptev"));

            var response = await client.SendEmailAsync(msg);

            return Ok("OK");
        }
    }
}
