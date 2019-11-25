using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspIT_Voting.Web.Data;
using AspIT_Voting.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace AspIT_Voting.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ActivitySuggestionsController : Controller
    {
        private readonly AppDbContext _context;

        public ActivitySuggestionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ActivitySuggestions
        public async Task<IActionResult> Index()
        {
            return View(await _context.ActivitySuggestions.ToListAsync());
        }

        // GET: Admin/ActivitySuggestions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activitySuggestion = await _context.ActivitySuggestions
                .FirstOrDefaultAsync(m => m.ActivitySuggestionId == id);
            if (activitySuggestion == null)
            {
                return NotFound();
            }

            return View(activitySuggestion);
        }

        // GET: Admin/ActivitySuggestions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ActivitySuggestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActivitySuggestionId,ActivtySuggestionName")] ActivitySuggestion model)
        {
            if (ModelState.IsValid)
            {
                if (ActivitySuggestionExists(model.ActivtySuggestionName))
                {
                    ModelState.AddModelError("", "Der eksister allerrede en aktivitet med det navn");
                    return (View(model));
                }

                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Admin/ActivitySuggestions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activitySuggestion = await _context.ActivitySuggestions.FindAsync(id);
            if (activitySuggestion == null)
            {
                return NotFound();
            }
            return View(activitySuggestion);
        }

        // POST: Admin/ActivitySuggestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActivitySuggestionId,ActivtySuggestionName")] ActivitySuggestion activitySuggestion)
        {
            if (id != activitySuggestion.ActivitySuggestionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activitySuggestion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivitySuggestionExists(activitySuggestion.ActivitySuggestionId))
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
            return View(activitySuggestion);
        }

        // GET: Admin/ActivitySuggestions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activitySuggestion = await _context.ActivitySuggestions
                .FirstOrDefaultAsync(m => m.ActivitySuggestionId == id);
            if (activitySuggestion == null)
            {
                return NotFound();
            }

            return View(activitySuggestion);
        }

        // POST: Admin/ActivitySuggestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activitySuggestion = await _context.ActivitySuggestions.FindAsync(id);
            _context.ActivitySuggestions.Remove(activitySuggestion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivitySuggestionExists(int id)
        {
            return _context.ActivitySuggestions.Any(e => e.ActivitySuggestionId == id);
        }

        private bool ActivitySuggestionExists(string name)
        {
            return _context.ActivitySuggestions.Any(e => e.ActivtySuggestionName.ToLower() == name.ToLower());
        }
    }
}
