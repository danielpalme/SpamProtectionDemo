using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpamProtectionDemo.Data;
using SpamProtectionDemo.Helpers;
using SpamProtectionDemo.Models;

namespace SpamProtectionDemo.Controllers
{
    public class NewsletterMembersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NewsletterMembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NewsletterMembers
        public async Task<IActionResult> Index()
        {
            return View(await _context.NewsletterMembers.ToListAsync());
        }

        // GET: NewsletterMembers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsletterMember = await _context.NewsletterMembers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsletterMember == null)
            {
                return NotFound();
            }

            return View(newsletterMember);
        }

        // GET: NewsletterMembers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NewsletterMembers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SpamProtection]
        public async Task<IActionResult> Create([Bind("Email")] NewsletterMember newsletterMember)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newsletterMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newsletterMember);
        }

        // GET: NewsletterMembers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsletterMember = await _context.NewsletterMembers.FindAsync(id);
            if (newsletterMember == null)
            {
                return NotFound();
            }
            return View(newsletterMember);
        }

        // POST: NewsletterMembers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Email")] NewsletterMember newsletterMember)
        {
            if (id != newsletterMember.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newsletterMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsletterMemberExists(newsletterMember.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(newsletterMember);
        }

        // GET: NewsletterMembers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsletterMember = await _context.NewsletterMembers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (newsletterMember == null)
            {
                return NotFound();
            }

            return View(newsletterMember);
        }

        // POST: NewsletterMembers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var newsletterMember = await _context.NewsletterMembers.FindAsync(id);
            _context.NewsletterMembers.Remove(newsletterMember);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsletterMemberExists(string id)
        {
            return _context.NewsletterMembers.Any(e => e.Id == id);
        }
    }
}