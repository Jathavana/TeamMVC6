using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Microsoft.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamMVC6.Models;

namespace TeamMVC6.Controllers
{
    public class ChoicesController : Controller 
    {
        public OptionsContext _context { get; set; }

        [FromServices]
        public ILogger<ChoicesController> Logger { get; set; }

        public ChoicesController(OptionsContext context)
        {
            _context = context;
        }

        // GET: Choices
        public IActionResult Index()
        {
            return View(_context.Choices.ToList());
        }

        // GET: Choices/Create
        public ActionResult Create()
        {
            ViewBag.Items = GetChoicesListItems();

            return View();
        }

        // GET: Choices/Details
        public ActionResult Details(int id)
        {
            Choice choice = _context.Choices
                .Where(b => b.ChoiceId == id)
                .FirstOrDefault();
            if (choice == null)
            {
                Logger.LogInformation("Details: Item not found {0}", id);

                return HttpNotFound();
            }
            return View(choice);
        }

        private IEnumerable<SelectListItem> GetChoicesListItems(int selected = -1)
        {
            var tmp = _context.Choices.ToList();

            return tmp
                .OrderBy(c => c.StudentLastName)
                .Select(c => new SelectListItem
                {
                    Text = String.Format("{0}, {1}", c.StudentFirstName, c.StudentLastName),
                    Value = c.ChoiceId.ToString(),
                    Selected = c.ChoiceId == selected
                });
        }

        // POST: Choices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Choice choice)
        {
            if (ModelState.IsValid)
            {
                _context.Choices.Add(choice);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(choice);
        }

        // GET: Choices/Edit
        public async Task<ActionResult> Edit(int id)
        {
            Choice choice = await FindChoiceAsync(id);
            if (choice == null)
            {
                Logger.LogInformation("Edit: Item not found {0}", id);
                return HttpNotFound();
            }

            ViewBag.Items = GetChoicesListItems(choice.ChoiceId);

            return View(choice);
        }

        // POST: Choices/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Choice choice)
        {
            try
            {
                choice.ChoiceId = id;
                _context.Choices.Attach(choice);
                _context.Entry(choice).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes.");
            }
            return View(choice);
        }

        private Task<Choice> FindChoiceAsync(int id)
        {
            return _context.Choices.SingleOrDefaultAsync(s => s.ChoiceId == id);
        }

        // GET: Choices/Delete
        [HttpGet]
        [ActionName("Delete")]
        public async Task<ActionResult> ConfirmDelete(int id, bool? retry)
        {
            Choice choice = await FindChoiceAsync(id);
            if (choice == null)
            {
                Logger.LogInformation("Delete: Item not found {0}", id);
                return HttpNotFound();
            }
            ViewBag.Retry = retry ?? false;
            return View(choice);
        }

        // POST: Choices/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Choice choice = await FindChoiceAsync(id);
                _context.Choices.Remove(choice);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Delete", new { id = id, retry = true });
            }
            return RedirectToAction("Index");
        }
    }
}
