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
    public class FoodSuggestionsController : Controller
    {
        private readonly AppDbContext _context;

        public FoodSuggestionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/FoodSuggestions
        public async Task<IActionResult> Index()
        {
            return View(await _context.FoodSuggestions.ToListAsync());
        }

        // GET: Admin/FoodSuggestions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodSuggestion = await _context.FoodSuggestions
                .FirstOrDefaultAsync(m => m.FoodSuggestionId == id);
            if (foodSuggestion == null)
            {
                return NotFound();
            }

            return View(foodSuggestion);
        }

        // GET: Admin/FoodSuggestions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/FoodSuggestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FoodSuggestionId,FoodSuggestionName")] FoodSuggestion model)
        {
            if (ModelState.IsValid)
            {

                if (FoodSuggestionExists(model.FoodSuggestionName))
                {
                    ModelState.AddModelError("", "Der eksister allerrede en ret med det navn");
                    return View(model);
                }

                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Admin/FoodSuggestions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodSuggestion = await _context.FoodSuggestions.FindAsync(id);
            if (foodSuggestion == null)
            {
                return NotFound();
            }
            return View(foodSuggestion);
        }

        // POST: Admin/FoodSuggestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FoodSuggestionId,FoodSuggestionName")] FoodSuggestion foodSuggestion)
        {
            if (id != foodSuggestion.FoodSuggestionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodSuggestion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodSuggestionExists(foodSuggestion.FoodSuggestionId))
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
            return View(foodSuggestion);
        }

        // GET: Admin/FoodSuggestions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodSuggestion = await _context.FoodSuggestions
                .FirstOrDefaultAsync(m => m.FoodSuggestionId == id);
            if (foodSuggestion == null)
            {
                return NotFound();
            }

            return View(foodSuggestion);
        }

        // POST: Admin/FoodSuggestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foodSuggestion = await _context.FoodSuggestions.FindAsync(id);
            _context.FoodSuggestions.Remove(foodSuggestion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodSuggestionExists(int id)
        {
            return _context.FoodSuggestions.Any(e => e.FoodSuggestionId == id);
        }

        private bool FoodSuggestionExists(string name)
        {
            return _context.FoodSuggestions.Any(e => e.FoodSuggestionName.ToLower() == name.ToLower());
        }
    }
}
