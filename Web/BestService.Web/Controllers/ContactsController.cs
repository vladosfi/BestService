namespace BestService.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using AngleSharp.Browser.Dom;
    using BestService.Services.Messaging;
    using BestService.Web.ViewModels.Contacts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    public class ContactsController : Controller
    {
        private const string SenderAppendData = "Sender email: ";
        private const string FromEmail = "sfi@abv.bg";
        private const string ToEmail = "vladosfi@gmail.com";

        private readonly IOptions<ContactsViewModel> appSettings;
        private readonly IEmailSender emailSender;

        public ContactsController(
            IOptions<ContactsViewModel> contactsSettings,
            IEmailSender emailSender)
        {
            this.appSettings = contactsSettings;
            this.emailSender = emailSender;
        }

        public IActionResult Index()
        {
            var model = new ContactsViewModel
            {
                GoogleMapsApyKey = this.appSettings.Value.GoogleMapsApyKey,
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(SendEmailInputModel input)
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
