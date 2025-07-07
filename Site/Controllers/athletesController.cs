using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Site.Data;
using Site.Models;

namespace Site.Controllers
{
    public class athletesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public athletesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: athletes
        public async Task<IActionResult> Index()
        {
            return View(await _context.athletes.ToListAsync());
        }

        // GET: athletes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var athletes = await _context.athletes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (athletes == null)
            {
                return NotFound();
            }

            return View(athletes);
        }

        // GET: athletes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: athletes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Age,Hight,Sport")] athletes athletes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(athletes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(athletes);
        }

        // GET: athletes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var athletes = await _context.athletes.FindAsync(id);
            if (athletes == null)
            {
                return NotFound();
            }
            return View(athletes);
        }

        // POST: athletes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age,Hight,Sport")] athletes athletes)
        {
            if (id != athletes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(athletes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!athletesExists(athletes.Id))
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
            return View(athletes);
        }

        // GET: athletes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var athletes = await _context.athletes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (athletes == null)
            {
                return NotFound();
            }

            return View(athletes);
        }

        // POST: athletes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var athletes = await _context.athletes.FindAsync(id);
            if (athletes != null)
            {
                _context.athletes.Remove(athletes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool athletesExists(int id)
        {
            return _context.athletes.Any(e => e.Id == id);
        }
    }
}
