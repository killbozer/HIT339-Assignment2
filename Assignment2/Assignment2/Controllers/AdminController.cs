using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Assignment2.Data;
using Assignment2.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment2.Controllers
{
    public class AdminController : Controller
    {

        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
             
        }

        public async Task<ActionResult> Members()
        {


            return View(await _context.ScheduleMembers.ToListAsync());

        }


    }
}