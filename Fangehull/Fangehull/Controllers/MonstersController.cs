using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fangehull.Data;
using Fangehull.Models;
using Microsoft.IdentityModel.Tokens;

namespace Fangehull.Controllers
{
    public class MonstersController : Controller
    {
        private readonly FangehullContext _context;

        public MonstersController(FangehullContext context)
        {
            _context = context;
        }

        // GET: Monsters
        public async Task<IActionResult> Index(string monsterDifficulty, string searchString)
        {
            IQueryable<string> difficultyQuery = _context.Monster.Select(m => m.Difficulty.ToString());

            var monsters = _context.Monster.Select(m => m);

            if (!String.IsNullOrEmpty(searchString))
            {
                monsters = monsters.Where(s => s.MonsterName!.Contains(searchString));
            }

            if(Enum.TryParse<Difficulties>(monsterDifficulty, out Difficulties difficulty))
            {
                monsters = monsters.Where(x => x.Difficulty == difficulty);
            }

            var monsterDifficultyVM = new MonsterDifficultyViewModel
            {
                Difficulties = new SelectList(await difficultyQuery.Distinct().ToListAsync()),
                Monsters = await monsters.ToListAsync()
            };

            return View(monsterDifficultyVM);
        }

        // GET: Monsters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Monster == null)
            {
                return NotFound();
            }

            var monster = await _context.Monster
                .FirstOrDefaultAsync(m => m.Id == id);
            if (monster == null)
            {
                return NotFound();
            }

            return View(monster);
        }

        // GET: Monsters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Monsters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MonsterName,MaxHealtPoints,Dificulty")] Monster monster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(monster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(monster);
        }

        // GET: Monsters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Monster == null)
            {
                return NotFound();
            }

            var monster = await _context.Monster.FindAsync(id);
            if (monster == null)
            {
                return NotFound();
            }
            return View(monster);
        }

        // POST: Monsters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MonsterName,MaxHealtPoints,Dificulty")] Monster monster)
        {
            if (id != monster.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonsterExists(monster.Id))
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
            return View(monster);
        }

        // GET: Monsters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Monster == null)
            {
                return NotFound();
            }

            var monster = await _context.Monster
                .FirstOrDefaultAsync(m => m.Id == id);
            if (monster == null)
            {
                return NotFound();
            }

            return View(monster);
        }

        // POST: Monsters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Monster == null)
            {
                return Problem("Entity set 'FangehullContext.Monster'  is null.");
            }
            var monster = await _context.Monster.FindAsync(id);
            if (monster != null)
            {
                _context.Monster.Remove(monster);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MonsterExists(int id)
        {
          return _context.Monster.Any(e => e.Id == id);
        }
    }
}
