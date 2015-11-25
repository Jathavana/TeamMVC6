using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TeamMVC6.Models;

namespace TeamMVC6.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext _context { get; set; }
        private UserManager<ApplicationUser> _userManager;

        [FromServices]
        public ILogger<UsersController> Logger { get; set; }

        public UsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Users
        public IActionResult Index()
        {
            return View(_context.Users.ToList());
        }

        // GET: Details
        public async Task<ActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                Logger.LogInformation("Details: Item not found {0}", id);
                return HttpNotFound();
            }

            return View(user);
        }

        // GET: Edit
        public async Task<ActionResult> Edit (string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                Logger.LogInformation("Details: Item not found {0}", id);
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit (string id, ApplicationUser applicationUser)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if(applicationUser.LockoutEnabled == true)
                {
                    user.LockoutEnd = DateTime.UtcNow.AddYears(100);
                }
                else
                {
                    user.LockoutEnd = DateTime.UtcNow;
                }

                user.LockoutEnabled = applicationUser.LockoutEnabled;

                await _userManager.UpdateAsync(user);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes.");
            }

            return View(applicationUser);
        }
    }
}
