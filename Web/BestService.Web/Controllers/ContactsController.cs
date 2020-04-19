namespace BestService.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using AngleSharp.Browser.Dom;
    using BestService.Services.Messaging;
    using BestService.Web.ViewModels.Contacts;
    using Microsoft.AspNetCore.Mvc;

    public class ContactsController : Controller
    {
        private const string SenderAppendData = "Sender email: ";
        private const string FromEmail = "sfi@abv.bg";
        private const string ToEmail = "vladosfi@gmail.com";

        private readonly IEmailSender emailSender;

        public ContactsController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(ContactsInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            var content = SenderAppendData + input.SendersEmail + Environment.NewLine + input.HtmlContent;

            await this.emailSender.SendEmailAsync(FromEmail, input.FromName, ToEmail, input.Subject, content);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
