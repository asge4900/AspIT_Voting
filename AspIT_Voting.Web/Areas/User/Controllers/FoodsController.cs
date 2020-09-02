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
using AspIT_Voting.Web.Areas.User.ViewModels;
using Microsoft.AspNetCore.Identity;
using AspIT_Voting.Web.Other;

namespace AspIT_Voting.Web.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "Bruger")]
    public class FoodsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;

        public FoodsController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

        private Task<bool> UsersVote(string userId, int foodId)
        {
            return Task.FromResult(_context.UserFoods.Any(x => x.UserId == userId && x.FoodId == foodId));
        }

        // GET: User/Foods
        public async Task<IActionResult> Index()
        {
            var oldDate = DateTime.Now.AddDays(-12);
            var dates = _context.Foods.Where(l => l.CreationDate < oldDate);
            _context.Foods.RemoveRange(dates);
            _context.SaveChanges();

            var userLoggedIn = await GetCurrentUserAsync();

            var model = new List<FoodViewModel>();

            foreach (var food in _context.Foods)
            {
                var foodviewmodel = new FoodViewModel
                {
                    FoodId = food.FoodId,
                    FoodName = food.FoodName,
                    VoteCount = food.VoteCount
                };

                if (await UsersVote(userLoggedIn.Id, food.FoodId))
                {
                    foodviewmodel.IsThumbsUp = true;
                }
                else
                {
                    foodviewmodel.IsThumbsUp = false;
                }
                model.Add(foodviewmodel);
            }
            return View(model);
        }

        // GET: User/Foods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Foods
                .FirstOrDefaultAsync(m => m.FoodId == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // GET: User/Foods/Create
        public IActionResult Create()
        {
            var date = new Date();

            DayOfWeek monday = DayOfWeek.Monday;
            DayOfWeek tuesday = DayOfWeek.Tuesday;            
            DayOfWeek today = DateTime.Today.DayOfWeek;            
    
            if (date.GetWeekOfYear() % 2 == 0 && DateTime.Now.Hour >= 8 && monday <= today && today <= tuesday && DateTime.Now.Hour <= 15)
            {
                var model = new CreateFoodViewModel();

                var list = model.FoodSuggestionsList = new List<SelectListItem>();

                foreach (var item in _context.FoodSuggestions.Where(acs => !_context.Foods.Select(a => a.FoodName.ToLower()).Contains(acs.FoodSuggestionName.ToLower())).OrderBy(o => o.FoodSuggestionName))
                {
                    list.Add(new SelectListItem { Value = item.FoodSuggestionName, Text = item.FoodSuggestionName });
                }

                return View(model);
            }            
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: User/Foods/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateFoodViewModel model)
        {
            var list = model.FoodSuggestionsList = new List<SelectListItem>();

            if (ModelState.IsValid)
            {
                if (ActivityExists(model.FoodName))
                {
                    ModelState.AddModelError("", "Der eksister allerrede en ret med det navn");

                    foreach (var item in _context.FoodSuggestions.Where(acs => !_context.Foods.Select(a => a.FoodName.ToLower()).Contains(acs.FoodSuggestionName.ToLower())).OrderBy(o => o.FoodSuggestionName))
                    {
                        list.Add(new SelectListItem { Value = item.FoodSuggestionName, Text = item.FoodSuggestionName });
                    }

                    return View(model);
                }               

                if (model.FoodName.Length > 70)
                {
                    ModelState.AddModelError("", "Der kan max være 70 tegn");

                    foreach (var item in _context.FoodSuggestions.Where(acs => !_context.Foods.Select(a => a.FoodName.ToLower()).Contains(acs.FoodSuggestionName.ToLower())).OrderBy(o => o.FoodSuggestionName))
                    {
                        list.Add(new SelectListItem { Value = item.FoodSuggestionName, Text = item.FoodSuggestionName });
                    }

                    return View(model);
                }

                var food = new Food
                {
                    FoodName = model.FoodName,
                    CreationDate = DateTime.Now
                };

                _context.Add(food);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            foreach (var item in _context.FoodSuggestions.Where(acs => !_context.Foods.Select(a => a.FoodName.ToLower()).Contains(acs.FoodSuggestionName.ToLower())).OrderBy(o => o.FoodSuggestionName))
            {
                list.Add(new SelectListItem { Value = item.FoodSuggestionName, Text = item.FoodSuggestionName });
            }

            return View(model);
        }

        // GET: User/Foods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            return View(food);
        }

        // POST: User/Foods/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FoodId,FoodName,VoteCount,CreationDate")] Food food)
        {
            if (id != food.FoodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(food);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodExists(food.FoodId))
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
            return View(food);
        }

        // GET: User/Foods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Foods
                .FirstOrDefaultAsync(m => m.FoodId == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // POST: User/Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<IActionResult> AddVote(string userId, int foodId)
        {
            var userFood = new UserFood
            {
                UserId = userId,
                FoodId = foodId
            };

            _context.Add(userFood);
            await _context.SaveChangesAsync();

            var food = await _context.Foods.FindAsync(foodId);
            if (food == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                food.VoteCount += +1;

                _context.Update(food);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        private async Task<IActionResult> RemoveVote(string userId, int foodId)
        {
            var userFood = await _context.UserFoods.FindAsync(userId, foodId);

            _context.UserFoods.Remove(userFood);
            await _context.SaveChangesAsync();

            var food = await _context.Foods.FindAsync(foodId);
            if (food == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                food.VoteCount += -1;

                _context.Update(food);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public async Task<IActionResult> Vote(List<FoodViewModel> model)
        {
            var userLoggedIn = await GetCurrentUserAsync();


            if (userLoggedIn == null)
            {
                return NotFound();
            }

            var item = model.FirstOrDefault();


            if (item.IsThumbsUp && !(await UsersVote(userLoggedIn.Id, item.FoodId)))
            {
                await AddVote(userLoggedIn.Id, item.FoodId);
            }
            else if (!item.IsThumbsUp && await UsersVote(userLoggedIn.Id, item.FoodId))
            {
                await RemoveVote(userLoggedIn.Id, item.FoodId);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool FoodExists(int id)
        {
            return _context.Foods.Any(e => e.FoodId == id);
        }

        private bool ActivityExists(string name)
        {
            return _context.Foods.Any(e => e.FoodName.ToLower() == name.ToLower());
        }
    }
}
