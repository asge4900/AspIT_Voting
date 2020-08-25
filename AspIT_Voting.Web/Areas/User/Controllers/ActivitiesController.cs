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

namespace AspIT_Voting.Web.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "Bruger")]
    public class ActivitiesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;

        public ActivitiesController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

        private Task<bool> UsersVote(string userId, int activityId)
        {
            return Task.FromResult(_context.UserActivities.Any(x => x.UserId == userId && x.ActivityId == activityId));
        }

        // GET: User/Activities
        public async Task<IActionResult> Index()
        {
            var oldDate = DateTime.Now.AddDays(-12);
            var dates = _context.Activities.Where(l => l.CreationDate < oldDate);
            _context.Activities.RemoveRange(dates);
            _context.SaveChanges();

            var userLoggedIn = await GetCurrentUserAsync();

            var model = new List<ActivityViewModel>();

            foreach (var activity in _context.Activities)
            {
                var activityviewmodel = new ActivityViewModel
                {
                    ActivityId = activity.ActivityId,
                    ActivtyName = activity.ActivityName,
                    VoteCount = activity.VoteCount
                };

                if (await UsersVote(userLoggedIn.Id, activity.ActivityId))
                {
                    activityviewmodel.IsThumbsUp = true;
                }
                else
                {
                    activityviewmodel.IsThumbsUp = false;
                }
                model.Add(activityviewmodel);
            }
            return View(model);
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

            foreach (var item in _context.ActivitySuggestions.Where(acs => !_context.Activities.Select(a => a.ActivityName.ToLower()).Contains(acs.ActivitySuggestionName.ToLower())).OrderBy(o => o.ActivitySuggestionName))
            {
                list.Add(new SelectListItem { Value = item.ActivitySuggestionName, Text = item.ActivitySuggestionName });
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
            var list = model.ActivitySuggestionsList = new List<SelectListItem>();          

            if (ModelState.IsValid)
            {
                if (ActivityExists(model.ActivityName))
                {
                    ModelState.AddModelError("", "Der eksister allerrede en aktivitet med det navn");

                    foreach (var item in _context.ActivitySuggestions.Where(acs => !_context.Activities.Select(a => a.ActivityName.ToLower()).Contains(acs.ActivitySuggestionName.ToLower())).OrderBy(o => o.ActivitySuggestionName))
                    {
                        list.Add(new SelectListItem { Value = item.ActivitySuggestionName, Text = item.ActivitySuggestionName });
                    }

                    return View(model);
                }

                var activity = new Activity
                {
                    ActivityName = model.ActivityName,
                    CreationDate = DateTime.Now
                };

                _context.Add(activity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            foreach (var item in _context.ActivitySuggestions.Where(acs => !_context.Activities.Select(a => a.ActivityName.ToLower()).Contains(acs.ActivitySuggestionName.ToLower())).OrderBy(o => o.ActivitySuggestionName))
            {
                list.Add(new SelectListItem { Value = item.ActivitySuggestionName, Text = item.ActivitySuggestionName });
            }

            return View(model);
        }

        private async Task<IActionResult> AddVote(string userId, int activityId)
        {

            var userActivity = new UserActivity
            {
                UserId = userId,
                ActivityId = activityId
            };

            _context.Add(userActivity);
            await _context.SaveChangesAsync();

            var activity = await _context.Activities.FindAsync(activityId);
            if (activity == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                activity.VoteCount += +1;

                _context.Update(activity);

                await _context.SaveChangesAsync();
                return NoContent();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<IActionResult> RemoveVote(string userId, int activityId)
        {
            var userActivity = await _context.UserActivities.FindAsync(userId, activityId);

            _context.UserActivities.Remove(userActivity);
            await _context.SaveChangesAsync();

            var activity = await _context.Activities.FindAsync(activityId);
            if (activity == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                activity.VoteCount += -1;

                _context.Update(activity);

                await _context.SaveChangesAsync();
                return NoContent();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Vote(List<ActivityViewModel> model)
        {
            var userLoggedIn = await GetCurrentUserAsync();


            if (userLoggedIn == null)
            {
                return NotFound();
            }

            var item = model.FirstOrDefault();


            if (item.IsThumbsUp && !(await UsersVote(userLoggedIn.Id, item.ActivityId)))
            {
                await AddVote(userLoggedIn.Id, item.ActivityId);
            }
            else if (!item.IsThumbsUp && await UsersVote(userLoggedIn.Id, item.ActivityId))
            {
                await RemoveVote(userLoggedIn.Id, item.ActivityId);
            }          

            return RedirectToAction(nameof(Index));
        }

        private bool ActivityExists(string name)
        {
            return _context.Activities.Any(e => e.ActivityName.ToLower() == name.ToLower());
        }
    }
}
