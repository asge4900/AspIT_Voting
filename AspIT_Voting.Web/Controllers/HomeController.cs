using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AspIT_Voting.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Data;
using AspIT_Voting.Web.Data;
using System.Linq;

namespace AspIT_Voting.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly AppDbContext context;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (signInManager.IsSignedIn(User) && User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [AllowAnonymous]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Result()
        {
            return View();
        }

        public JsonResult VisualizeActivityResult()
        {
            List<object> iData = new List<object>();
            //Creating sample data  
            DataTable dt = new DataTable();
            dt.Columns.Add("ActivityName", System.Type.GetType("System.String"));
            dt.Columns.Add("VoteCount", System.Type.GetType("System.Int32"));

            DataRow dr = null;

            foreach (var activity in context.Activities.Where(a => a.VoteCount > 0))
            {
                dr = dt.NewRow();
                dr["ActivityName"] = activity.ActivityName;
                dr["VoteCount"] = activity.VoteCount;
                dt.Rows.Add(dr);
            }

            //Looping and extracting each DataColumn to List<Object>  
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }
            //Source data returned as JSON  
            return Json(iData);
        }

        public JsonResult VisualizeFoodResult()
        {
            List<object> iData = new List<object>();
            //Creating sample data  
            DataTable dt = new DataTable();
            dt.Columns.Add("FoodName", System.Type.GetType("System.String"));
            dt.Columns.Add("VoteCount", System.Type.GetType("System.Int32"));

            DataRow dr = null;

            foreach (var food in context.Foods.Where(f => f.VoteCount > 0))
            {
                dr = dt.NewRow();
                dr["FoodName"] = food.FoodName;
                dr["VoteCount"] = food.VoteCount;
                dt.Rows.Add(dr);
            }

            //Looping and extracting each DataColumn to List<Object>  
            foreach (DataColumn dc in dt.Columns)
            {
                List<object> x = new List<object>();
                x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                iData.Add(x);
            }
            //Source data returned as JSON  
            return Json(iData);
        }
    }
}
