namespace BestService.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using BestService.Data;
    using BestService.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class ContactFormEntriesController : Controller
    {
        private readonly ApplicationDbContext context;

        public ContactFormEntriesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: Administration/ContactFormEntries
        public async Task<IActionResult> Index()
        {
            return this.View(await this.context.ContactFormEntries.ToListAsync());
        }

        // GET: Administration/ContactFormEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var contactFormEntry = await this.context.ContactFormEntries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactFormEntry == null)
            {
                return this.NotFound();
            }

            return this.View(contactFormEntry);
        }

        // GET: Administration/ContactFormEntries/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/ContactFormEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email,Title,Content,Ip,Id,CreatedOn,ModifiedOn")] ContactFormEntry contactFormEntry)
        {
            if (this.ModelState.IsValid)
            {
                this.context.Add(contactFormEntry);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(contactFormEntry);
        }

        // GET: Administration/ContactFormEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var contactFormEntry = await context.ContactFormEntries.FindAsync(id);
            if (contactFormEntry == null)
            {
                return this.NotFound();
            }

            return this.View(contactFormEntry);
        }

        // POST: Administration/ContactFormEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Email,Title,Content,Ip,Id,CreatedOn,ModifiedOn")] ContactFormEntry contactFormEntry)
        {
            if (id != contactFormEntry.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.context.Update(contactFormEntry);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.ContactFormEntryExists(contactFormEntry.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(contactFormEntry);
        }

        // GET: Administration/ContactFormEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var contactFormEntry = await this.context.ContactFormEntries
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactFormEntry == null)
            {
                return this.NotFound();
            }

            return this.View(contactFormEntry);
        }

        // POST: Administration/ContactFormEntries/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactFormEntry = await this.context.ContactFormEntries.FindAsync(id);
            this.context.ContactFormEntries.Remove(contactFormEntry);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool ContactFormEntryExists(int id)
        {
            return this.context.ContactFormEntries.Any(e => e.Id == id);
        }
    }
}
