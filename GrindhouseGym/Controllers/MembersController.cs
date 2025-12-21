using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using GrindhouseGym.Data;
using GrindhouseGym.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace GrindhouseGym.Controllers
{
    [Authorize] // Secure: Only admins can access
    public class MembersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MembersController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            // FINAL FIX: Include Trainer so names appear in the list
            var applicationDbContext = _context.Members
                                               .Include(m => m.Plan)
                                               .Include(m => m.Trainer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var member = await _context.Members
                .Include(m => m.Plan)
                .Include(m => m.Trainer) // FINAL FIX: Include Trainer on profile card
                .FirstOrDefaultAsync(m => m.MemberId == id);

            if (member == null) return NotFound();

            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            ViewData["PlanId"] = new SelectList(_context.Plans, "PlanId", "Name");
            // FINAL FIX: Load Trainers for the dropdown
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "TrainerId", "FullName");
            return View();
        }

        // POST: Members/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Member member, IFormFile? profileFile)
        {
            if (ModelState.IsValid)
            {
                // IMAGE UPLOAD LOGIC
                if (profileFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/profiles");
                    Directory.CreateDirectory(uploadsFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + profileFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await profileFile.CopyToAsync(fileStream);
                    }

                    member.ProfileImage = uniqueFileName;
                }

                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlanId"] = new SelectList(_context.Plans, "PlanId", "Name", member.PlanId);
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "TrainerId", "FullName", member.TrainerId);
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var member = await _context.Members.FindAsync(id);
            if (member == null) return NotFound();

            ViewData["PlanId"] = new SelectList(_context.Plans, "PlanId", "Name", member.PlanId);
            // FINAL FIX: Load Trainers so the Edit Dropdown isn't empty!
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "TrainerId", "FullName", member.TrainerId);
            return View(member);
        }

        // POST: Members/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Member member, IFormFile? profileFile)
        {
            if (id != member.MemberId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var existingMember = await _context.Members.AsNoTracking().FirstOrDefaultAsync(m => m.MemberId == id);

                    if (profileFile != null)
                    {
                        // 1. Upload New Image
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/profiles");
                        Directory.CreateDirectory(uploadsFolder);
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + profileFile.FileName;
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await profileFile.CopyToAsync(fileStream);
                        }
                        member.ProfileImage = uniqueFileName;
                    }
                    else
                    {
                        // 2. Keep Old Image
                        member.ProfileImage = existingMember.ProfileImage;
                    }

                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.MemberId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlanId"] = new SelectList(_context.Plans, "PlanId", "Name", member.PlanId);
            ViewData["TrainerId"] = new SelectList(_context.Trainers, "TrainerId", "FullName", member.TrainerId);
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var member = await _context.Members
                .Include(m => m.Plan)
                .Include(m => m.Trainer) // Include trainer info before deleting
                .FirstOrDefaultAsync(m => m.MemberId == id);

            if (member == null) return NotFound();

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _context.Members.FindAsync(id);
            if (member != null)
            {
                _context.Members.Remove(member);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.MemberId == id);
        }
    }
}