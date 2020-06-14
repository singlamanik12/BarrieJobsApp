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
    public class JobPostingsController : Controller
    {
        private readonly BarrieJobsContext _context;

        public JobPostingsController(BarrieJobsContext context)
        {
            _context = context;
        }

        // GET: JobPostings
        public async Task<IActionResult> Index()
        {
            return View(await _context.JobPostings.ToListAsync());
        }

        // GET: JobPostings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPostings = await _context.JobPostings
                .FirstOrDefaultAsync(m => m.JobId == id);
            if (jobPostings == null)
            {
                return NotFound();
            }

            return View(jobPostings);
        }

        // GET: JobPostings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobPostings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobId,Company,Description,DurationMonths")] JobPostings jobPostings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobPostings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobPostings);
        }

        // GET: JobPostings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPostings = await _context.JobPostings.FindAsync(id);
            if (jobPostings == null)
            {
                return NotFound();
            }
            return View(jobPostings);
        }

        // POST: JobPostings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobId,Company,Description,DurationMonths")] JobPostings jobPostings)
        {
            if (id != jobPostings.JobId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobPostings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobPostingsExists(jobPostings.JobId))
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
            return View(jobPostings);
        }

        // GET: JobPostings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPostings = await _context.JobPostings
                .FirstOrDefaultAsync(m => m.JobId == id);
            if (jobPostings == null)
            {
                return NotFound();
            }

            return View(jobPostings);
        }

        // POST: JobPostings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobPostings = await _context.JobPostings.FindAsync(id);
            _context.JobPostings.Remove(jobPostings);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobPostingsExists(int id)
        {
            return _context.JobPostings.Any(e => e.JobId == id);
        }
    }
}
