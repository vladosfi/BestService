namespace BestService.Web.Controllers
{
    using BestService.Web.ViewModels.Contacts;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    public class ContactsController : Controller
    {
        private readonly IOptions<ContactsViewModel> appSettings;

        public ContactsController(IOptions<ContactsViewModel> contactsSettings)
        {
            this.appSettings = contactsSettings;
        }

        public IActionResult Index()
        {
            var model = new ContactsViewModel
            {
                ApyKey = this.appSettings.Value.ApyKey,
            };

            return this.View(model);
        }
    }
}
