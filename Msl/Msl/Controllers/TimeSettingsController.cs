﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Msl.Data;
using Msl.Models;

namespace Msl.Controllers
{

    public class TimeSettingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TimeSettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TimeSettings
        public async Task<IActionResult> Index()
        {
            return View(await _context.timeSetting.ToListAsync());
        }

        

        // GET: TimeSettings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeSetting = await _context.timeSetting.FindAsync(id);
            if (timeSetting == null)
            {
                return NotFound();
            }
            return View(timeSetting);
        }

        // POST: TimeSettings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InTime,OutTime")] TimeSetting timeSetting)
        {
            if (id != timeSetting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timeSetting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeSettingExists(timeSetting.Id))
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
            return View(timeSetting);
        }

       

        private bool TimeSettingExists(int id)
        {
            return _context.timeSetting.Any(e => e.Id == id);
        }
    }
}
