using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BarrieJobsApp.Models;

namespace BarrieJobsApp.Controllers
{
    public class ApplicantsController : Controller
    {
        private readonly BarrieJobsContext _context;

        public ApplicantsController(BarrieJobsContext context)
        {
            _context = context;
        }

        // GET: Applicants
        public async Task<IActionResult> Index()
        {
            var barrieJobsContext = _context.Applicants.Include(a => a.JobAppliedNavigation);
            return View(await barrieJobsContext.ToListAsync());
        }

        // GET: Applicants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicants = await _context.Applicants
                .Include(a => a.JobAppliedNavigation)
                .FirstOrDefaultAsync(m => m.ApplicantId == id);
            if (applicants == null)
            {
                return NotFound();
            }

            return View(applicants);
        }

        // GET: Applicants/Create
        public IActionResult Create()
        {
            ViewData["JobApplied"] = new SelectList(_context.JobPostings, "JobId", "Company");
            return View();
        }

        // POST: Applicants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicantId,ApplicantResume,ApplicantLocation,JobApplied")] Applicants applicants)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicants);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobApplied"] = new SelectList(_context.JobPostings, "JobId", "Company", applicants.JobApplied);
            return View(applicants);
        }

        // GET: Applicants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicants = await _context.Applicants.FindAsync(id);
            if (applicants == null)
            {
                return NotFound();
            }
            ViewData["JobApplied"] = new SelectList(_context.JobPostings, "JobId", "Company", applicants.JobApplied);
            return View(applicants);
        }

        // POST: Applicants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicantId,ApplicantResume,ApplicantLocation,JobApplied")] Applicants applicants)
        {
            if (id != applicants.ApplicantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicants);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantsExists(applicants.ApplicantId))
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
            ViewData["JobApplied"] = new SelectList(_context.JobPostings, "JobId", "Company", applicants.JobApplied);
            return View(applicants);
        }

        // GET: Applicants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicants = await _context.Applicants
                .Include(a => a.JobAppliedNavigation)
                .FirstOrDefaultAsync(m => m.ApplicantId == id);
            if (applicants == null)
            {
                return NotFound();
            }

            return View(applicants);
        }

        // POST: Applicants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicants = await _context.Applicants.FindAsync(id);
            _context.Applicants.Remove(applicants);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantsExists(int id)
        {
            return _context.Applicants.Any(e => e.ApplicantId == id);
        }
    }
}
