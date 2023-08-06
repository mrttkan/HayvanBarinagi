
using HayvanBarinagi.Data;
using HayvanBarinagi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HayvanBarinagi.Controllers
{

    public class SahiplendirmeBasvurulariController : Controller
    {
        private readonly HayvanBarinagiIdentityDbContext _context;

        public SahiplendirmeBasvurulariController(HayvanBarinagiIdentityDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.SahiplendirmeBasvurulari.ToListAsync());
        }
        [Authorize(Roles = "Admin,User")]
        public IActionResult Create(int hayvanId)
        {
            ViewBag.HayvanId = hayvanId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Create([Bind("HayvanId,Ad,Soyad,Telefon,Eposta,Adres,Aciklama,Durum,BasvuruTarihi")] SahiplendirmeBasvurulari basvuru)
        {
            basvuru.BasvuruTarihi = DateTime.Now;
            if (ModelState.IsValid)
            {

                _context.SahiplendirmeBasvurulari.Add(basvuru);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(basvuru);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basvuru = await _context.SahiplendirmeBasvurulari.FindAsync(id);
            if (basvuru == null)
            {
                return NotFound();
            }
            return View(basvuru);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HayvanId,Ad,Soyad,Telefon,Eposta,Adres,Aciklama,Durum,BasvuruTarihi")] SahiplendirmeBasvurulari basvuru)
        {
            if (id != basvuru.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(basvuru);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BasvuruExists(basvuru.Id))
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
            return View(basvuru);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basvuru = await _context.SahiplendirmeBasvurulari
                .FirstOrDefaultAsync(m => m.Id == id);
            if (basvuru == null)
            {
                return NotFound();
            }

            return View(basvuru);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var basvuru = await _context.SahiplendirmeBasvurulari.FindAsync(id);
            _context.SahiplendirmeBasvurulari.Remove(basvuru);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BasvuruExists(int id)
        {
            return _context.SahiplendirmeBasvurulari.Any(e => e.Id == id);
        }
    }
}
