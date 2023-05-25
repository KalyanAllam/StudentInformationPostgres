using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SISPostgres.Models;
using System.Web;

namespace SISPostgres.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly ContosoUniversityDataContext _context;

        public EnrollmentsController(ContosoUniversityDataContext context)
        {
            _context = context;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index(int sid)
        {

           // HttpContext.Session.SetInt32("sid", sid);

             var contosoUniversityDataContext = _context.Enrollments.Include(e => e.Course).Include(e => e.Student).Include(e => e.Term).Where(e => e.Studentid == sid);
            return View(await contosoUniversityDataContext.ToListAsync());
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {


       //     int sid = HttpContext.Session.GetInt32("sid").Value;
            if (id == null || _context.Enrollments == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .Include(e => e.Term)
                .FirstOrDefaultAsync(m => m.Enrollmentid == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // GET: Enrollments/Create
        public IActionResult Create()
        {
            ViewData["Courseid"] = new SelectList(_context.Courses, "Courseid", "Name");
            ViewData["Studentid"] = new SelectList(_context.Students, "Studentid", "Lastname");
            ViewData["Termid"] = new SelectList(_context.Terms, "Termid", "Termname");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Enrollmentid,Courseid,Studentid,Termid,Marks,Marksobtained,Grade")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Courseid"] = new SelectList(_context.Courses, "Courseid", "Name", enrollment.Courseid);
            ViewData["Studentid"] = new SelectList(_context.Students, "Studentid", "Lastname", enrollment.Studentid);
            ViewData["Termid"] = new SelectList(_context.Terms, "Termid", "Termname", enrollment.Termid);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Enrollments == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["Courseid"] = new SelectList(_context.Courses, "Courseid", "Name", enrollment.Courseid);
            ViewData["Studentid"] = new SelectList(_context.Students, "Studentid", "Lastname", enrollment.Studentid);
            ViewData["Termid"] = new SelectList(_context.Terms, "Termid", "Termname", enrollment.Termid);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Enrollmentid,Courseid,Studentid,Termid,Marks,Marksobtained,Grade")] Enrollment enrollment)
        {
            if (id != enrollment.Enrollmentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.Enrollmentid))
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
            ViewData["Courseid"] = new SelectList(_context.Courses, "Courseid", "Name", enrollment.Courseid);
            ViewData["Studentid"] = new SelectList(_context.Students, "Studentid", "Lastname", enrollment.Studentid);
            ViewData["Termid"] = new SelectList(_context.Terms, "Termid", "Termname", enrollment.Termid);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Enrollments == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollments
                .Include(e => e.Course)
                .Include(e => e.Student)
                .Include(e => e.Term)
                .FirstOrDefaultAsync(m => m.Enrollmentid == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Enrollments == null)
            {
                return Problem("Entity set 'ContosoUniversityDataContext.Enrollments'  is null.");
            }
            var enrollment = await _context.Enrollments.FindAsync(id);
            if (enrollment != null)
            {
                _context.Enrollments.Remove(enrollment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrollmentExists(int id)
        {
          return (_context.Enrollments?.Any(e => e.Enrollmentid == id)).GetValueOrDefault();
        }
    }
}
