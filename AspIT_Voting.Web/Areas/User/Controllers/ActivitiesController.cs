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

namespace AspIT_Voting.Web.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "Bruger")]
    public class ActivitiesController : Controller
    {
        private readonly AppDbContext _context;

        public ActivitiesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: User/Activities
        public async Task<IActionResult> Index()
        {
            return View(await _context.Activities.ToListAsync());
        }

        // GET: User/Activities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities
                .FirstOrDefaultAsync(m => m.ActivityId == id);
            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }


        // GET: User/Activities/Create
        [HttpGet]
        public IActionResult Create()
        {

            var model = new CreateActivityViewModel();

            var list = model.ActivitySuggestionsList = new List<SelectListItem>(); 

            foreach (var item in _context.ActivitySuggestions)
            {
                list.Add(new SelectListItem { Value = item.ActivtySuggestionName, Text = item.ActivtySuggestionName });                
            }
          
            return View(model);
        }

        // POST: User/Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateActivityViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (ActivityExists(model.ActivityName))
                {
                    ModelState.AddModelError("", "Der eksister allerrede en aktivitet med det navn");
                    return (View(model));
                }

                var activity = new Activity
                {
                    ActivityName = model.ActivityName
                };

                _context.Add(activity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var list = model.ActivitySuggestionsList = new List<SelectListItem>();

            foreach (var item in _context.ActivitySuggestions)
            {
                list.Add(new SelectListItem { Value = item.ActivtySuggestionName, Text = item.ActivtySuggestionName });
            }

            return View(model);
        }        

        private bool ActivityExists(string name)
        {
            return _context.Activities.Any(e => e.ActivityName.ToLower() == name.ToLower());
        }
    }
}
