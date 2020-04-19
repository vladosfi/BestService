namespace BestService.Web.Controllers
{
    using System.Threading.Tasks;

    using BestService.Common;
    using BestService.Data.Common.Repositories;
    using BestService.Data.Models;
    using BestService.Services.Messaging;
    using BestService.Web.ViewModels.Contacts;
    using Microsoft.AspNetCore.Mvc;

    public class ContactsController : Controller
    {
        private readonly IEmailSender emailSender;
        private readonly IRepository<ContactFormEntry> contactsRepository;

        public ContactsController(
            IEmailSender emailSender,
            IRepository<ContactFormEntry> contactsRepository)
        {
            this.emailSender = emailSender;
            this.contactsRepository = contactsRepository;
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

            var ip = this.HttpContext.Connection.RemoteIpAddress.ToString();
            var contactFormEntry = new ContactFormEntry
            {
                Name = input.FromName,
                Email = input.SendersEmail,
                Title = input.Subject,
                Content = input.Content,
                Ip = ip,
            };

            await this.contactsRepository.AddAsync(contactFormEntry);
            await this.contactsRepository.SaveChangesAsync();

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
