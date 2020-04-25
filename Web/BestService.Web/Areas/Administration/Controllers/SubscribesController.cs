namespace BestService.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using BestService.Data;
    using BestService.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class SubscribesController : Controller
    {
        private readonly ApplicationDbContext context;

        public SubscribesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: Administration/Subscribes
        public async Task<IActionResult> Index()
        {
            return this.View(await this.context.Subscribes.ToListAsync());
        }

        // GET: Administration/Subscribes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var subscribe = await this.context.Subscribes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subscribe == null)
            {
                return this.NotFound();
            }

            return this.View(subscribe);
        }

        // GET: Administration/Subscribes/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Subscribes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Email,Ip,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Subscribe subscribe)
        {
            if (ModelState.IsValid)
            {
                this.context.Add(subscribe);
                await this.context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return this.View(subscribe);
        }

        // GET: Administration/Subscribes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var subscribe = await this.context.Subscribes.FindAsync(id);
            if (subscribe == null)
            {
                return this.NotFound();
            }

            return this.View(subscribe);
        }

        // POST: Administration/Subscribes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Email,Ip,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Subscribe subscribe)
        {
            if (id != subscribe.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.context.Update(subscribe);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.SubscribeExists(subscribe.Id))
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

            return this.View(subscribe);
        }

        // GET: Administration/Subscribes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var subscribe = await this.context.Subscribes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subscribe == null)
            {
                return this.NotFound();
            }

            return this.View(subscribe);
        }

        // POST: Administration/Subscribes/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subscribe = await this.context.Subscribes.FindAsync(id);
            this.context.Subscribes.Remove(subscribe);
            await this.context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool SubscribeExists(int id)
        {
            return this.context.Subscribes.Any(e => e.Id == id);
        }
    }
}
