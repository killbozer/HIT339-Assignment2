using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Assignment2.Data;
using Assignment2.Models;

namespace Assignment2
{
    public class CoachesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CoachesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Coaches
        public async Task<IActionResult> Index()
        {
            return View(await _context.Coach.ToListAsync());
        }

        public ActionResult MyCoach()
        {

            var coach = _userManager.GetUserName(User);
            var myCoach = _context.Coach.Where(m => m.Email == coach);

            return View("Index", myCoach);

        }

        
        [Authorize(Roles = "Coach")]
        public ActionResult MySchedule()
        {
            // get the username of the person logged in (which is the same as the email they used to register)
            var coach = _userManager.GetUserName(User);
            var schedule = _context.Schedule.Where(m => m.CoachEmail == coach);
            // only return entries that have the same email as the person logged in
            return View("MySchedule", schedule);

        }
        
        public IActionResult Schedules(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = _context.ScheduleMembers.Where(m => m.ScheduleId == id);

            return View("Schedule", member);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coach = await _context.Coach.FindAsync(id);
            if (coach == null)
            {
                return NotFound();
            }
            return View(coach);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Email, Biography, PhotoUrl")] Coach coach)
        {
            if (id != coach.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coach);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoachExists(coach.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(MyCoach));
            }
            return View(coach);
        }

        private bool CoachExists(int id)
        {
            return _context.Coach.Any(e => e.Id == id);
        }

    }

}
