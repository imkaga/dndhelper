using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using dndhelper.Data;
using dndhelper.Models;

namespace dndhelper.Controllers
{
    public class CharacterGeneratorsController : Controller
    {
        private readonly dndhelperContext _context;

        public CharacterGeneratorsController(dndhelperContext context)
        {
            _context = context;
        }

        // GET: CharacterGenerators
        public async Task<IActionResult> Index()
        {
              return _context.CharacterGenerator != null ? 
                          View(await _context.CharacterGenerator.ToListAsync()) :
                          Problem("Entity set 'dndhelperContext.CharacterGenerator'  is null.");
        }

        // GET: CharacterGenerators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CharacterGenerator == null)
            {
                return NotFound();
            }

            var characterGenerator = await _context.CharacterGenerator
                .FirstOrDefaultAsync(m => m.Id == id);
            if (characterGenerator == null)
            {
                return NotFound();
            }

            return View(characterGenerator);
        }

        // GET: CharacterGenerators/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CharacterGenerators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Race,Strength,Dexterity,Intelligence,Luck")] CharacterGenerator characterGenerator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(characterGenerator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(characterGenerator);
        }

        // GET: CharacterGenerators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CharacterGenerator == null)
            {
                return NotFound();
            }

            var characterGenerator = await _context.CharacterGenerator.FindAsync(id);
            if (characterGenerator == null)
            {
                return NotFound();
            }
            return View(characterGenerator);
        }

        // POST: CharacterGenerators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Race,Strength,Dexterity,Intelligence,Luck")] CharacterGenerator characterGenerator)
        {
            if (id != characterGenerator.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(characterGenerator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharacterGeneratorExists(characterGenerator.Id))
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
            return View(characterGenerator);
        }

        // GET: CharacterGenerators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CharacterGenerator == null)
            {
                return NotFound();
            }

            var characterGenerator = await _context.CharacterGenerator
                .FirstOrDefaultAsync(m => m.Id == id);
            if (characterGenerator == null)
            {
                return NotFound();
            }

            return View(characterGenerator);
        }

        // POST: CharacterGenerators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CharacterGenerator == null)
            {
                return Problem("Entity set 'dndhelperContext.CharacterGenerator'  is null.");
            }
            var characterGenerator = await _context.CharacterGenerator.FindAsync(id);
            if (characterGenerator != null)
            {
                _context.CharacterGenerator.Remove(characterGenerator);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharacterGeneratorExists(int id)
        {
          return (_context.CharacterGenerator?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
