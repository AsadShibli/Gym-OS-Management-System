using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GrindhouseGym.Data;
using GrindhouseGym.Models;
using Microsoft.AspNetCore.Authorization;

namespace GrindhouseGym.Controllers
{
    [Authorize]
    public class TrainersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment; // 1. Inject File Service

        public TrainersController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Trainers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Trainers.ToListAsync());
        }

        // GET: Trainers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var trainer = await _context.Trainers
                .Include(t => t.Members)
                .ThenInclude(m => m.Plan)
                .FirstOrDefaultAsync(m => m.TrainerId == id);

            if (trainer == null) return NotFound();

            return View(trainer);
        }

        // GET: Trainers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trainers/Create (UPDATED FOR IMAGE)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Trainer trainer, IFormFile? profileFile)
        {
            if (ModelState.IsValid)
            {
                if (profileFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/trainers");
                    Directory.CreateDirectory(uploadsFolder); // Ensure folder exists
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + profileFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await profileFile.CopyToAsync(fileStream);
                    }
                    trainer.ProfileImage = uniqueFileName;
                }

                _context.Add(trainer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }

        // GET: Trainers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer == null) return NotFound();
            return View(trainer);
        }

        // POST: Trainers/Edit/5 (UPDATED FOR IMAGE)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Trainer trainer, IFormFile? profileFile)
        {
            if (id != trainer.TrainerId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Fetch existing to keep old photo if no new one is uploaded
                    var existingTrainer = await _context.Trainers.AsNoTracking().FirstOrDefaultAsync(t => t.TrainerId == id);

                    if (profileFile != null)
                    {
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/trainers");
                        Directory.CreateDirectory(uploadsFolder);
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + profileFile.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await profileFile.CopyToAsync(fileStream);
                        }
                        trainer.ProfileImage = uniqueFileName;
                    }
                    else
                    {
                        // Keep old image
                        trainer.ProfileImage = existingTrainer.ProfileImage;
                    }

                    _context.Update(trainer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Trainers.Any(e => e.TrainerId == trainer.TrainerId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }

        // GET: Trainers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var trainer = await _context.Trainers.FirstOrDefaultAsync(m => m.TrainerId == id);
            if (trainer == null) return NotFound();
            return View(trainer);
        }

        // POST: Trainers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);
            if (trainer != null)
            {
                _context.Trainers.Remove(trainer);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}