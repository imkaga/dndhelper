using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using dndhelper.Data;
using dndhelper.Models;
using Microsoft.AspNetCore.Authorization;

namespace dndhelper.Controllers
{
    [Authorize]
    public class CharacterGeneratorsController : Controller
    {
        private readonly dndhelperContext _context;

        public CharacterGeneratorsController(dndhelperContext context)
        {
            _context = context;
        }

        // GET: CharacterGenerator/Index
        public IActionResult Index()
        {
            // Pobierz identyfikator użytkownika
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            // Pobierz postacie przypisane do bieżącego użytkownika
            var characterGenerators = _context.CharacterGenerator.Where(c => c.UserId == userId).ToList();

            return View(characterGenerators);
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
            // Pobierz identyfikator użytkownika
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var characterGenerator = new CharacterGenerator
            {
                UserId = userId,
            };

            return View(characterGenerator);
        }

        // POST: CharacterGenerators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Race,Strength,Dexterity,Intelligence,Luck,UserId")] CharacterGenerator characterGeneratorModel)
        {
            if (ModelState.IsValid)
            {
                // Pobranie identyfikatora użytkownika
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                // Przypisanie użytkownika do nowo utworzonej postaci
                characterGeneratorModel.UserId = userId;

                _context.Add(characterGeneratorModel);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(characterGeneratorModel);
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
                    _context.Add(characterGenerator);
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
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            // Sprawdź, czy postać istnieje dla danego użytkownika
            return _context.CharacterGenerator.Any(e => e.Id == id && e.UserId == userId);
        }
    }
}