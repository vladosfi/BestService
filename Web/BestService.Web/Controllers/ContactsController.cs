namespace BestService.Web.Controllers
{
    using System.Threading.Tasks;

    using BestService.Common;
    using BestService.Services.Messaging;
    using BestService.Web.ViewModels.Contacts;
    using Microsoft.AspNetCore.Mvc;

    public class ContactsController : Controller
    {
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
        public async Task<IActionResult> Index(ContactsInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.emailSender.SendEmailAsync(
                GlobalConstants.SystemSenderMail,
                input.FromName,
                input.SendersEmail,
                input.Subject,
                input.Content);

            this.TempData["Success"] = true;

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
