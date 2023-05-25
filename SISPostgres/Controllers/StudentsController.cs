using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SISPostgres.Models;

namespace SISPostgres.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ContosoUniversityDataContext _context;
        public static int globalsecid;
        public StudentsController(ContosoUniversityDataContext context)
        {
            _context = context;
        }

        
         

        // GET: Students
        public async Task<IActionResult> Index(int secid)
        {
            if(secid>0)
            { globalsecid = secid; }
          
           
            var contosoUniversityDataContext = _context.Students.Include(s => s.Class).Include(s => s.Section).Where(s => s.Sectionid == secid);
          

            return View(await contosoUniversityDataContext.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Class)
                .Include(s => s.Section)
                .FirstOrDefaultAsync(m => m.Studentid == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            int secid = globalsecid;
            ViewData["Classid"] = new SelectList(_context.Classes, "Classid", "Classname");
            ViewData["Sectionid"] = new SelectList(_context.Sections.Where(e => e.Sectionid == secid), "Sectionid", "Classsectionname");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Studentid,Classid,Lastname,Firstname,Phoneno,Email,Sectionid,Rollno")] Student student)
        {
            int secid = globalsecid;
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { secid });
            }
            ViewData["Classid"] = new SelectList(_context.Classes, "Classid", "Classname", student.Classid);
            ViewData["Sectionid"] = new SelectList(_context.Sections.Where(e => e.Sectionid == secid), "Sectionid", "Classsectionname", student.Sectionid);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            int secid = globalsecid;
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["Classid"] = new SelectList(_context.Classes, "Classid", "Classname", student.Classid);
            ViewData["Sectionid"] = new SelectList(_context.Sections, "Sectionid", "Classsectionname", student.Sectionid);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Studentid,Classid,Lastname,Firstname,Phoneno,Email,Sectionid,Rollno")] Student student)
        {
            int secid = globalsecid;
            if (id != student.Studentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Studentid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index),new { secid });
            }
            ViewData["Classid"] = new SelectList(_context.Classes, "Classid", "Classname", student.Classid);
            ViewData["Sectionid"] = new SelectList(_context.Sections, "Sectionid", "Classsectionname", student.Sectionid);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Class)
                .Include(s => s.Section)
                .FirstOrDefaultAsync(m => m.Studentid == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int secid = globalsecid;
            if (_context.Students == null)
            {
                return Problem("Entity set 'ContosoUniversityDataContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),new { secid });
        }

        private bool StudentExists(int id)
        {
          return (_context.Students?.Any(e => e.Studentid == id)).GetValueOrDefault();
        }
    }
}
