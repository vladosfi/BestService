namespace BestService.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using BestService.Data;
    using BestService.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class CompaniesController : Controller
    {
        private readonly ApplicationDbContext context;

        public CompaniesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: Administration/Companies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.context.Companies.Include(c => c.Category).Include(c => c.User);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Companies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var company = await this.context.Companies
                .Include(c => c.Category)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return this.NotFound();
            }

            return this.View(company);
        }

        // GET: Administration/Companies/Create
        public IActionResult Create()
        {
            this.ViewData["CategoryId"] = new SelectList(this.context.Categories, "Id", "Description");
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id");
            return this.View();
        }

        // POST: Administration/Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,LogoImage,OfficialSite,UserId,CategoryId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Company company)
        {
            if (this.ModelState.IsValid)
            {
                this.context.Add(company);
                await this.context.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["CategoryId"] = new SelectList(this.context.Categories, "Id", "Description", company.CategoryId);
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", company.UserId);
            return this.View(company);
        }

        // GET: Administration/Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var company = await this.context.Companies.FindAsync(id);
            if (company == null)
            {
                return this.NotFound();
            }
            this.ViewData["CategoryId"] = new SelectList(this.context.Categories, "Id", "Description", company.CategoryId);
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", company.UserId);
            return this.View(company);
        }

        // POST: Administration/Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,LogoImage,OfficialSite,UserId,CategoryId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Company company)
        {
            if (id != company.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.context.Update(company);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.CompanyExists(company.Id))
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

            this.ViewData["CategoryId"] = new SelectList(this.context.Categories, "Id", "Description", company.CategoryId);
            this.ViewData["UserId"] = new SelectList(this.context.Users, "Id", "Id", company.UserId);
            return this.View(company);
        }

        // GET: Administration/Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var company = await this.context.Companies
                .Include(c => c.Category)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return this.NotFound();
            }

            return this.View(company);
        }

        // POST: Administration/Companies/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = await this.context.Companies.FindAsync(id);
            company.IsDeleted = true;
            this.context.Companies.Update(company);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool CompanyExists(int id)
        {
            return this.context.Companies.Any(e => e.Id == id);
        }
    }
}
