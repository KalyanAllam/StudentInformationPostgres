using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SISPostgres.Models;

namespace SISPostgres.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ContosoUniversityDataContext _context;

        public CoursesController(ContosoUniversityDataContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            var contosoUniversityDataContext = _context.Courses.Include(c => c.Class).Include(c => c.Faculty);
            return View(await contosoUniversityDataContext.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Class)
                .Include(c => c.Faculty)
                .FirstOrDefaultAsync(m => m.Courseid == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            ViewData["Classid"] = new SelectList(_context.Classes, "Classid", "Classname");
            ViewData["Facultyid"] = new SelectList(_context.Faculties, "Facultyid", "Facultylastname");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Courseid,Title,Facultyid,Name,Credits,Classid")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Classid"] = new SelectList(_context.Classes, "Classid", "Classname", course.Classid);
            ViewData["Facultyid"] = new SelectList(_context.Faculties, "Facultyid", "Facultylastname", course.Facultyid);
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["Classid"] = new SelectList(_context.Classes, "Classid", "Classname", course.Classid);
            ViewData["Facultyid"] = new SelectList(_context.Faculties, "Facultyid", "Facultylastname", course.Facultyid);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Courseid,Title,Facultyid,Name,Credits,Classid")] Course course)
        {
            if (id != course.Courseid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Courseid))
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
            ViewData["Classid"] = new SelectList(_context.Classes, "Classid", "Classname", course.Classid);
            ViewData["Facultyid"] = new SelectList(_context.Faculties, "Facultyid", "Facultylastname", course.Facultyid);
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Courses == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Class)
                .Include(c => c.Faculty)
                .FirstOrDefaultAsync(m => m.Courseid == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Courses == null)
            {
                return Problem("Entity set 'ContosoUniversityDataContext.Courses'  is null.");
            }
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
          return (_context.Courses?.Any(e => e.Courseid == id)).GetValueOrDefault();
        }
    }
}
