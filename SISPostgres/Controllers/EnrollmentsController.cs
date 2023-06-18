using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SISPostgres.Models;
using System.Web;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace SISPostgres.Controllers
{
   
    public class EnrollmentsController : Controller
    {
        public static int globalsid;
        private readonly ContosoUniversityDataContext _context;

        public EnrollmentsController(ContosoUniversityDataContext context)
        {
            _context = context;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index(int sid)
        {
            if (sid > 0)
            { globalsid = sid; }
            //  HttpContext.Session.SetInt32(SessionKeyName, sid);

            var contosoUniversityDataContext = _context.Enrollments.Include(e => e.Course).Include(e => e.Student).Include(e => e.Term).Where(e => e.Studentid == sid);

            ViewBag.sid = sid;
           
            return View(await contosoUniversityDataContext.ToListAsync());



        }


        public async Task<IActionResult> Report(int sid)
        {
            var contosoUniversityDataContext = _context.Enrollments.Include(e => e.Course).Include(e => e.Student).Include(e => e.Term).Where(e => e.Studentid == sid);
            ViewData["Termid"] = new SelectList(_context.Terms, "Termid", "Termname");
           
            ViewData["Enrollments"] = null;
            return View(await contosoUniversityDataContext.ToListAsync());
        }


        [HttpPost]
        public async Task<IActionResult> Report(UserModel model)
        {
            var selectedValue = model.SelectTeaType;
            ViewBag.TeaType = selectedValue.ToString();
            string studentname="";
            int totalmarks=0;
   var contosoUniversityDataContext = _context.Enrollments.Include(e => e.Course).Include(e => e.Student).Include(e => e.Term ).Where(e => e.Studentid == globalsid).Where(e => e.Term.Termname == selectedValue.ToString());
         
            ViewData["Enrollments"] = contosoUniversityDataContext;
            foreach (var item in contosoUniversityDataContext)
            { 
                studentname = item.Student.Lastname;
                if(item.Marksobtained>0)
                { totalmarks = Convert.ToInt32(totalmarks)  + Convert.ToInt32(item.Marksobtained); }

               
            }

            
            ViewBag.totalmarks = totalmarks;
            ViewBag.studentname = studentname;
            return View(await contosoUniversityDataContext.ToListAsync());
        }


            // GET: Enrollments/Details/5
            public async Task<IActionResult> Details(int? id)
        {


           int sid = globalsid;
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
            int sid = globalsid;
            ViewData["Courseid"] = new SelectList(_context.Courses, "Courseid", "Name");
            ViewData["Studentid"] = new SelectList(_context.Students.Where(e => e.Studentid == sid), "Studentid", "Lastname");
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
            int sid = globalsid;
            if (ModelState.IsValid)
            {
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { sid });
            }
            ViewData["Courseid"] = new SelectList(_context.Courses, "Courseid", "Name", enrollment.Courseid);
            ViewData["Studentid"] = new SelectList(_context.Students.Where(e => e.Studentid == sid), "Studentid", "Lastname", enrollment.Studentid);
            ViewData["Termid"] = new SelectList(_context.Terms, "Termid", "Termname", enrollment.Termid);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            int sid = globalsid;
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
            ViewData["Studentid"] = new SelectList(_context.Students.Where(e => e.Studentid == sid), "Studentid", "Lastname", enrollment.Studentid);
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
            int sid = globalsid;
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
                return RedirectToAction(nameof(Index), new { sid });
            }
            ViewData["Courseid"] = new SelectList(_context.Courses, "Courseid", "Name", enrollment.Courseid);
            ViewData["Studentid"] = new SelectList(_context.Students.Where(e => e.Studentid == sid), "Studentid", "Lastname", enrollment.Studentid);
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
            int sid = globalsid;
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
            return RedirectToAction(nameof(Index), new { sid });
        }

        private bool EnrollmentExists(int id)
        {
          return (_context.Enrollments?.Any(e => e.Enrollmentid == id)).GetValueOrDefault();
        }
    }
}
