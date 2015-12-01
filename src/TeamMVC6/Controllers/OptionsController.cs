using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using TeamMVC6.Models;
using System.Net;
using Microsoft.Framework.Logging;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Authorization;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TeamMVC6.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OptionsController : Controller
    {
        private OptionsContext _context { get; set; }
        public OptionsController(OptionsContext context)
        {
            _context = context;
        }

        [FromServices]
        public ILogger<OptionsController> Logger { get; set; }

        // GET: Options
        public IActionResult Index()
        {

            /*if (Request.IsAuthenticated && Roles.IsUserInRole("Admin"))
            {
                return View(db.Options.ToList());
            }
            else
            {
                return View("Error");
            }*/
            return View(_context.Options.ToList());
        }

        // GET: Options/Details/5
        
        public ActionResult Details(int? id)
        {
            /*if (Request.IsAuthenticated && Roles.IsUserInRole("Admin"))
            {*/
            Option option = _context.Options
              .Where(b => b.OptionId == id)
              .FirstOrDefault();
            if (option == null)
            {
                Logger.LogInformation("Details: Item not found {0}", id);
                return HttpNotFound();
            }
            return View(option);
            /* }
             else
             {
                 return View("Error");
             }*/

        }

        // GET: Options/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: Options/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Option option)
        {

            if (ModelState.IsValid)
            {
                _context.Options.Add(option);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(option);
        }


        private Task<Option> FindOptionAsync(int id)
        {
            return _context.Options.SingleOrDefaultAsync(s => s.OptionId == id);
        }

        // GET: Options/Edit/5
        public async Task<ActionResult> Edit(int id)
        {

            Option option = await FindOptionAsync(id);
            if (option == null)
            {
                Logger.LogInformation("Edit: Item not found {0}", id);
                return HttpNotFound();
            }

            
            return View(option);
        }


        // POST: Options/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Option option)
        {
            try
            {
                option.OptionId = id;
                _context.Options.Attach(option);
                _context.Entry(option).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes.");
            }
            return View(option);


        }


  
        [HttpGet]
        [ActionName("Delete")]
        public async Task<ActionResult> ConfirmDelete(int id, bool? retry)
        {
            Option option = await FindOptionAsync(id);
            if (option == null)
            {
                Logger.LogInformation("Delete: Item not found {0}", id);
                return HttpNotFound();
            }
            ViewBag.Retry = retry ?? false;
            return View(option);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Option option = await FindOptionAsync(id);
                var choices = _context.Choices.Where(c => 
                c.FirstChoiceOptionId == option.OptionId 
                || c.SecondChoiceOptionId == option.OptionId 
                || c.ThirdChoiceOptionId == option.OptionId
                || c.FourthChoiceOptionId == option.OptionId);
                foreach (var choice in choices)
                {
                    _context.Choices.Remove(choice);
                }
                _context.Options.Remove(option);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Delete", new { id = id, retry = true });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

