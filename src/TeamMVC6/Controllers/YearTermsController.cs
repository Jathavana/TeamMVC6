using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Authentication.Facebook;
using Microsoft.AspNet.Authentication.Google;
using Microsoft.AspNet.Authentication.MicrosoftAccount;
using Microsoft.AspNet.Authentication.Twitter;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics.Entity;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Dnx.Runtime;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using TeamMVC6.Models;
using TeamMVC6.Services;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Authorization;

namespace TeamMVC6.Controllers
{
    [Authorize(Roles = "Admin")]
    public class YearTermsController : Controller
    {
        private OptionsContext _context { get; set; }

        [FromServices]
        public ILogger<YearTermsController> Logger { get; set; }

        public YearTermsController(OptionsContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.YearTerms.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.yearForms = new SelectList(new List<Object> {
                new { value = 10, text = "Spring" },
                new { value = 20, text = "Summer/Fall" },
                new { value = 30, text = "Winter" } },
                   "value", "text", 2);
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(YearTerm yearTerm)
        {
            if (ModelState.IsValid)
            {
                if (yearTerm.IsDefault == true)
                {
                    var formerActiveYearTerm = (_context.YearTerms
                                             .Where(t => t.IsDefault == true)
                                             .Select(t => t)).First();

                    formerActiveYearTerm.IsDefault = false;
                    await _context.SaveChangesAsync();
                }

                var test = _context.YearTerms.Where(i => i.Year == yearTerm.Year && i.Term == yearTerm.Term).Any();
                if (test)
                {
                    ModelState.AddModelError("", "Duplicate YearTerm.");
                    ViewBag.yearForms = new SelectList(new List<Object> {
                                        new { value = 10, text = "Spring" },
                                        new { value = 20, text = "Summer/Fall" },
                                        new { value = 30, text = "Winter" } },
                                        "value", "text", 2);
                    return View(yearTerm);

                }

                _context.YearTerms.Add(yearTerm);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(yearTerm);
        }

        
        public ActionResult Details(int id)
        {
            YearTerm yearTerm = _context.YearTerms
                .Where(b => b.YearTermId == id)
                .FirstOrDefault();


            if (yearTerm == null)
            {
                Logger.LogInformation("Details: Item not found {0}", id);
                return RedirectToAction("Index");
            }

            var term = yearTerm.Term;
            string season;
            switch (term)
            {
                case 10:
                    season = "Spring";
                    break;
                case 20:
                    season = "Summer / Fall";
                    break;
                case 30:
                    season = "Winter";
                    break;
                default:
                    season = "null";
                    break;
            }
            ViewBag.season = season;
            return View(yearTerm);
        }

        
        /**
        private IEnumerable<SelectListItem> GetSpeakersListItems(int selected = -1)
        {
            var tmp = _context.Speakers.ToList();

            // Create authors list for <select> dropdown
            return tmp
                .OrderBy(s => s.LastName)
                .Select(s => new SelectListItem
                {
                    Text = String.Format("{0}, {1}", s.FirstName, s.LastName),
                    Value = s.SpeakerId.ToString(),
                    Selected = s.SpeakerId == selected
                });
        }**/

        
        public async Task<ActionResult> Edit(int id)
        {
            YearTerm yearTerm = await FindSpeakerAsync(id);
            if (yearTerm == null)
            {
                Logger.LogInformation("Edit: Item not found {0}", id);
                return RedirectToAction("Index");
            }

            ViewBag.yearForms = new SelectList(new List<Object> {
                new { value = 10, text = "Spring" },
                new { value = 20, text = "Summer/Fall" },
                new { value = 30, text = "Winter" } },
                "value", "text", 2);
            return View(yearTerm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, YearTerm yearTerm)
        {
            try
            {
                yearTerm.YearTermId = id;
                _context.YearTerms.Attach(yearTerm);
                _context.Entry(yearTerm).State = EntityState.Modified;
                var test = _context.YearTerms.Where(i => i.Year == yearTerm.Year && i.Term == yearTerm.Term);

                if (test.Any() && test.FirstOrDefault().YearTermId != yearTerm.YearTermId)
                {
                    ModelState.AddModelError("", "Duplicate YearTerm.");
                    ViewBag.yearForms = new SelectList(new List<Object> {
                                        new { value = 10, text = "Spring" },
                                        new { value = 20, text = "Summer/Fall" },
                                        new { value = 30, text = "Winter" } },
                                        "value", "text", 2);
                    return View(yearTerm);

                }

                if (yearTerm.IsDefault == true)
                {
                    var formerActiveYearTerm = (_context.YearTerms
                                             .Where(t => t.IsDefault == true)
                                             .Select(t => t)).First();

                    if (formerActiveYearTerm != yearTerm)
                    {
                        formerActiveYearTerm.IsDefault = false;
                        _context.YearTerms.Attach(formerActiveYearTerm);
                        _context.Entry(formerActiveYearTerm).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes.");
            }
            ViewBag.yearForms = new SelectList(new List<Object> {
                new { value = 10, text = "Spring" },
                new { value = 20, text = "Summer/Fall" },
                new { value = 30, text = "Winter" } },
             "value", "text", 2);

            return View(yearTerm);
        }

        private Task<YearTerm> FindSpeakerAsync(int id)
        {
            return _context.YearTerms.SingleOrDefaultAsync(s => s.YearTermId == id);
        }

        
        [HttpGet]
        [ActionName("Delete")]
        public async Task<ActionResult> ConfirmDelete(int id, bool? retry)
        {
            YearTerm yearTerm = await FindSpeakerAsync(id);
            if (yearTerm == null)
            {
                Logger.LogInformation("Delete: Item not found {0}", id);
                return RedirectToAction("Index");
            }

            var term = yearTerm.Term;
            string season;
            switch (term)
            {
                case 10:
                    season = "Spring";
                    break;
                case 20:
                    season = "Summer / Fall";
                    break;
                case 30:
                    season = "Winter";
                    break;
                default:
                    season = "null";
                    break;
            }
            ViewBag.season = season;

            ViewBag.Retry = retry ?? false;
            ViewBag.Default = false;


            return View(yearTerm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                YearTerm yearTerm = await FindSpeakerAsync(id);
                var choices = _context.Choices.Where(c => c.YearTermId == yearTerm.YearTermId);
                foreach(var choice in choices)
                {
                    _context.Choices.Remove(choice);
                }

                if (yearTerm.IsDefault == true)
                {
                    ModelState.AddModelError("", "Cannot delete the Default term.");
                    ViewBag.Retry = false;
                    ViewBag.Default = true;
                    return View(yearTerm);
                }
                _context.YearTerms.Remove(yearTerm);
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
