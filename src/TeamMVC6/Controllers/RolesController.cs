using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using TeamMVC6.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net;
using Microsoft.AspNet.Identity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamMVC6.Controllers
{
    public class RolesController : Controller
    {
        private ApplicationDbContext _context { get; set; }
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public RolesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_context.Roles.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(IdentityRole role)
        {
            if (role.Name == null)
            {
                ModelState.AddModelError("Name", "Name of new user role cannot be empty.");
            }

            if (ModelState.IsValid)
            {
                _context.Roles.Add(role);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(role);
        }

        public async Task<IActionResult> Edit(string id)
        {
            //IdentityRole role = _context.Roles
            //    .Where(c => c.Id == id)
            //    .FirstOrDefault();

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
            //}

            //IdentityRole role = _context.Roles.Where(c => c.Id == id).FirstOrDefault();
            IdentityRole role = await _roleManager.FindByIdAsync(id);

            ViewBag.test = role.Name;
            //if (role == null)
            //{
            //    return HttpNotFound();
            //}

            //New attempt
            //var userDictionary = new Dictionary<string, string>();
            //if (_context.Users.Count() > 0)
            //{
            //    foreach (var user in _context.Users)
            //    {
            //        userDictionary[user.Id] = user.UserName;
            //    }
            //}
            //ViewBag.UserDictionary = userDictionary;

            return View();
        }

        public IActionResult Delete(string id)
        {
            IdentityRole role = _context.Roles.Where(c => c.Id == id).FirstOrDefault();
            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(string id)
        {
            IdentityRole role = _context.Roles.Where(c => c.Id == id).FirstOrDefault();

            if (role.Name == "Student")
            {
                ViewBag.InUse = "There must always be a Student Role.";
                return View(role);
            }

            if (role.Name == "Admin")
            {
                ViewBag.InUse = "There must always be an Administrator Role.";
                return View(role);
            }

            _context.Roles.Remove(role);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
