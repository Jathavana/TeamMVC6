using Microsoft.AspNet.Http;
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
            var choices = _context
                .Choices
                .Include(y => y.YearTerm)
                .Include(c => c.FirstChoiceOption)
                .Include(c => c.SecondChoiceOption)
                .Include(c => c.ThirdChoiceOption)
                .Include(c => c.FourthChoiceOption)
                .Where(c => c.YearTermId == _context.YearTerms
                .Where(y => y.IsDefault == true)
                .Select(y => y.YearTermId)
                .FirstOrDefault());

            return View(choices.ToList());
        }

        // GET: Choices/Create
        public ActionResult Create()
        {
            ViewBag.FirstChoiceOptionId = new SelectList(_context.Options.Where(c => c.IsActive == true), "OptionId", "Title");
            ViewBag.FourthChoiceOptionId = new SelectList(_context.Options.Where(c => c.IsActive == true), "OptionId", "Title");
            ViewBag.SecondChoiceOptionId = new SelectList(_context.Options.Where(c => c.IsActive == true), "OptionId", "Title");
            ViewBag.ThirdChoiceOptionId = new SelectList(_context.Options.Where(c => c.IsActive == true), "OptionId", "Title");

            return View();
        }

        // GET: Choices/Details
        public ActionResult Details(int id)
        {

            Choice choice = _context.Choices
                .Include(c => c.FirstChoiceOption)
                .Include(c => c.SecondChoiceOption)
                .Include(c => c.ThirdChoiceOption)
                .Include(c => c.FourthChoiceOption)
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
            var defaultTermId = _context.YearTerms.Where(c => c.IsDefault == true).First().YearTermId;
            var checkStudentId = _context.Choices.Where(c => c.StudentId == choice.StudentId);

            if (checkStudentId.Where(c => c.YearTermId == defaultTermId).Count() != 0)
            {
                ViewBag.FirstChoiceOptionId = new SelectList(_context.Options.Where(c => c.IsActive == true), "OptionId", "Title", choice.FirstChoiceOptionId);
                ViewBag.FourthChoiceOptionId = new SelectList(_context.Options.Where(c => c.IsActive == true), "OptionId", "Title", choice.FourthChoiceOptionId);
                ViewBag.SecondChoiceOptionId = new SelectList(_context.Options.Where(c => c.IsActive == true), "OptionId", "Title", choice.SecondChoiceOptionId);
                ViewBag.ThirdChoiceOptionId = new SelectList(_context.Options.Where(c => c.IsActive == true), "OptionId", "Title", choice.ThirdChoiceOptionId);
                ModelState.AddModelError(string.Empty, "You have already registered for this term");

                return View(choice);
            }
            if (choice.FirstChoiceOptionId == choice.SecondChoiceOptionId
               || choice.FirstChoiceOptionId == choice.ThirdChoiceOptionId
               || choice.FirstChoiceOptionId == choice.FourthChoiceOptionId
               || choice.SecondChoiceOptionId == choice.ThirdChoiceOptionId
               || choice.SecondChoiceOptionId == choice.FourthChoiceOptionId
               || choice.ThirdChoiceOptionId == choice.FourthChoiceOptionId)
            {
                ViewBag.FirstChoiceOptionId = new SelectList(_context.Options.Where(c => c.IsActive == true), "OptionId", "Title", choice.FirstChoiceOptionId);
                ViewBag.FourthChoiceOptionId = new SelectList(_context.Options.Where(c => c.IsActive == true), "OptionId", "Title", choice.FourthChoiceOptionId);
                ViewBag.SecondChoiceOptionId = new SelectList(_context.Options.Where(c => c.IsActive == true), "OptionId", "Title", choice.SecondChoiceOptionId);
                ViewBag.ThirdChoiceOptionId = new SelectList(_context.Options.Where(c => c.IsActive == true), "OptionId", "Title", choice.ThirdChoiceOptionId);
                ModelState.AddModelError(string.Empty, "Cannot have duplicate selections!");

                return View(choice);
            }
            choice.YearTermId = defaultTermId;
            choice.SelectionDate = DateTime.Now;
            _context.Choices.Add(choice);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
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
            ViewBag.FirstChoiceOptionId = new SelectList(_context.Options.Where(c => c.IsActive == true), "OptionId", "Title");
            ViewBag.FourthChoiceOptionId = new SelectList(_context.Options.Where(c => c.IsActive == true), "OptionId", "Title");
            ViewBag.SecondChoiceOptionId = new SelectList(_context.Options.Where(c => c.IsActive == true), "OptionId", "Title");
            ViewBag.ThirdChoiceOptionId = new SelectList(_context.Options.Where(c => c.IsActive == true), "OptionId", "Title");
            //ViewBag.YearTermId = new SelectList(_context.YearTerms, "YearTermId", "YearTermId");

            return View(choice);
        }

        // POST: Choices/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Choice choice)
        {
            if (_context.YearTerms.Where(c => c.IsDefault).Count() != 0)
            {
                choice.YearTermId = _context.YearTerms.Where(c => c.IsDefault == true).First().YearTermId;
            }
            try
            {
                if (choice.FirstChoiceOptionId == choice.SecondChoiceOptionId
                       || choice.FirstChoiceOptionId == choice.ThirdChoiceOptionId
                       || choice.FirstChoiceOptionId == choice.FourthChoiceOptionId
                       || choice.SecondChoiceOptionId == choice.ThirdChoiceOptionId
                       || choice.SecondChoiceOptionId == choice.FourthChoiceOptionId
                       || choice.ThirdChoiceOptionId == choice.FourthChoiceOptionId)
                {
                    ViewBag.FirstChoiceOptionId = new SelectList(_context.Options.Where(c => c.IsActive == true), "OptionId", "Title", choice.FirstChoiceOptionId);
                    ViewBag.FourthChoiceOptionId = new SelectList(_context.Options.Where(c => c.IsActive == true), "OptionId", "Title", choice.FourthChoiceOptionId);
                    ViewBag.SecondChoiceOptionId = new SelectList(_context.Options.Where(c => c.IsActive == true), "OptionId", "Title", choice.SecondChoiceOptionId);
                    ViewBag.ThirdChoiceOptionId = new SelectList(_context.Options.Where(c => c.IsActive == true), "OptionId", "Title", choice.ThirdChoiceOptionId);
                    ModelState.AddModelError(string.Empty, "Cannot have duplicate selections!");

                    return View(choice);
                }
                var studentId = _context.Choices.Where(c => c.ChoiceId == id).Select(c => c.StudentId).FirstOrDefault();
                choice.ChoiceId = id;
                choice.StudentId = studentId;
                choice.SelectionDate = DateTime.Now;
                _context.Choices.Attach(choice);
                _context.Entry(choice).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes.");
                Console.WriteLine(ex);
                
            }
            ViewBag.FirstChoiceOptionId = new SelectList(_context.Options.Where(c => c.IsActive == true), "OptionId", "Title");
            ViewBag.FourthChoiceOptionId = new SelectList(_context.Options.Where(c => c.IsActive == true), "OptionId", "Title");
            ViewBag.SecondChoiceOptionId = new SelectList(_context.Options.Where(c => c.IsActive == true), "OptionId", "Title");
            ViewBag.ThirdChoiceOptionId = new SelectList(_context.Options.Where(c => c.IsActive == true), "OptionId", "Title");
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
            Choice choice = _context.Choices
               .Include(c => c.FirstChoiceOption)
               .Include(c => c.SecondChoiceOption)
               .Include(c => c.ThirdChoiceOption)
               .Include(c => c.FourthChoiceOption)
               .Where(b => b.ChoiceId == id)
               .FirstOrDefault();
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
