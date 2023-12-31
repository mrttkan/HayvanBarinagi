﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HayvanBarinagi.Models;
using Microsoft.AspNetCore.Authorization;
using HayvanBarinagi.Data;


namespace HayvanBarinagi.Controllers
{
    public class HayvanlarController : Controller
    {
        private readonly HayvanBarinagiIdentityDbContext _context;

        public HayvanlarController(HayvanBarinagiIdentityDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var hayvanlar = await _context.Hayvanlar.ToListAsync();
            return View(hayvanlar);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Tur,Ad,ResimUrl,Aciklama")] Hayvan hayvan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hayvan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hayvan);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Hayvanlar == null)
            {
                return NotFound();
            }

            var hayvan = await _context.Hayvanlar.FindAsync(id);
            if (hayvan == null)
            {
                return NotFound();
            }
            return View(hayvan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tur,Ad,ResimUrl,Aciklama")] Hayvan hayvan)
        {
            if (id != hayvan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hayvan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HayvanExists(hayvan.Id))
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
            return View(hayvan);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Hayvanlar == null)
            {
                return NotFound();
            }

            var hayvan = await _context.Hayvanlar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hayvan == null)
            {
                return NotFound();
            }

            return View(hayvan);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Hayvanlar == null)
            {
                return Problem("HayvanBarinagiContext.Hayvanlar' değeri boş(null) olamaz .");
            }
            var hayvan = await _context.Hayvanlar.FindAsync(id);
            if (hayvan != null)
            {
                _context.Hayvanlar.Remove(hayvan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HayvanExists(int id)
        {
            return (_context.Hayvanlar?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
