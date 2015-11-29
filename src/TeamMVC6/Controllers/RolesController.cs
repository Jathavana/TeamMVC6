using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using TeamMVC6.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;

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
            if (id == null)
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
            }

            //IdentityRole role = _context.Roles.Where(c => c.Id == id).FirstOrDefault();
            IdentityRole role = await _roleManager.FindByIdAsync(id);

            ViewBag.usersInRole = _userManager.GetUsersInRoleAsync(role.Name.ToString());

            if (role == null)
            {
                return HttpNotFound();
            }

            //New attempt
            var userDictionary = new Dictionary<string, string>();
            if (_context.Users.Count() > 0)
            {
                foreach (var user in _context.Users)
                {
                    userDictionary[user.Id] = user.UserName;
                }
            }
            ViewBag.UserDictionary = userDictionary;

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(role).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        public IActionResult Delete(string id)
        {
            IdentityRole role = _context.Roles.Where(c => c.Id == id).FirstOrDefault();
            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(string id)
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

            var test = await _userManager.GetUsersInRoleAsync(role.Name);
            if (test.Count > 0)
            {
                ViewBag.InUse = "Please remove users from this role before deleting it. Goodnight sweet prince.";
                return View(role);
            }

            _context.Roles.Remove(role);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> RemoveUserFromRole(string roleId, string userId)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(roleId);
            ApplicationUser user = await _userManager.FindByIdAsync(userId);

            if (await _userManager.IsInRoleAsync(user, role.Name))
            {
                ApplicationUser testicle = _userManager.Users.Where(c => c.Id == userId).First();
                await _userManager.RemoveFromRoleAsync(testicle, role.Name);
                await _context.SaveChangesAsync();
            }

            ViewBag.confirm = "User removed from Role!";
            return RedirectToAction("Edit", "Roles", new { id = role.Id });
        }

        public ActionResult AddUserToRole(string roleId)
        {
            IdentityRole role = _context.Roles.Where(c => c.Id == roleId).First();
            List<SelectListItem> ddlDataSource = new List<SelectListItem>();
            var result = _context.Users.OrderBy(c => c.Id);
            if (result.Count() > 0)
            {
                foreach (var user in result)
                {
                    ddlDataSource.Add(new SelectListItem
                    {
                        Text = user.UserName,
                        Value = user.Id,
                    });
                }
            }
            ViewBag.Users = ddlDataSource;

            return View(role);
        }

        public async Task<ActionResult> AddUserToRoleSubmit(string ddlUsers, string roleId)
        {
            var role = _context.Roles.Where(c => c.Id == roleId).First();

            List<SelectListItem> ddlDataSource = new List<SelectListItem>();
            var result = _context.Users.OrderBy(c => c.Id);
            if (result.Count() > 0)
            {
                foreach (var user in result)
                {
                    ddlDataSource.Add(new SelectListItem
                    {
                        Text = user.UserName,
                        Value = user.Id,
                    });
                }
            }
            ViewBag.Users = ddlDataSource;

            var user1 = _userManager.Users.Where(c => c.Id == ddlUsers).First();
            var isit = await _userManager.IsInRoleAsync(user1, role.Name);
            if (isit == true)
            {
                ViewBag.InUse = "This user is already a member of the Role";
                return View("AddUserToRole", role);
            }
            

            _context.Roles.Where(c => c.Name == role.Name).First().Users.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<String>
            {
                RoleId = role.Id,
                UserId = ddlUsers
            });
            _context.SaveChanges();

            ViewBag.confirm = "User added to Role!";
            return RedirectToAction("Edit", "Roles", new { id = role.Id });
        }
    }
}
