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
using AspIT_Voting.Web.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace AspIT_Voting.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminActivitiesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminActivitiesController(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }

        // GET: Admin/AdminActivities
        public async Task<IActionResult> Index()
        {
            //var res = _context.Activities.Zip(userManager.Users, (a, u) => new { Activty = a, User = u });

            var model = new AdminActivityViewModel();

            foreach (var activity in _context.Activities)
            { 
                var adminActivityViewModel = new AdminActivityViewModel
                {
                    ActivityId = activity.ActivityId,
                    ActivityName = activity.ActivityName,
                    CreationDate = activity.CreationDate,
                    VoteCount = activity.VoteCount
                };

                model.Activities.Add(adminActivityViewModel);                
            }           

            foreach (var user in userManager.Users.Where(u => !_context.UserActivities.Select(ua => ua.UserId).Contains(u.Id)).OrderBy(u => u.UserName))
            {
                if (await userManager.IsInRoleAsync(user, "Bruger"))
                {
                    var adminActivityViewModel = new AdminActivityViewModel
                    {
                        UserName = user.UserName
                    };

                    model.Users.Add(adminActivityViewModel);
                }                     
            }            
            return View(model);
        }

        // GET: Admin/AdminActivities/Details/5
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

        // GET: Admin/AdminActivities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = await _context.Activities.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }
            return View(activity);
        }

        // POST: Admin/AdminActivities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActivityId,ActivtyName,VoteCount,CreationDate")] Activity activity)
        {
            if (id != activity.ActivityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(activity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityExists(activity.ActivityId))
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
            return View(activity);
        }

        // GET: Admin/AdminActivities/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Admin/AdminActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activity = await _context.Activities.FindAsync(id);
            _context.Activities.Remove(activity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActivityExists(int id)
        {
            return _context.Activities.Any(e => e.ActivityId == id);
        }
    }
}
