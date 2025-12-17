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
using System.IO; // Required for Path
using Microsoft.AspNetCore.Http; // Required for IFormFile

namespace GrindhouseGym.Controllers
{
    [Authorize] // Locks the entire controller. Only logged-in users can enter.
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
            var applicationDbContext = _context.Members.Include(m => m.Plan);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .Include(m => m.Plan)
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            ViewData["PlanId"] = new SelectList(_context.Plans, "PlanId", "Name");
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    // Create folder if not exists
                    Directory.CreateDirectory(uploadsFolder);

                    // Unique filename to prevent overwrite
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
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            ViewData["PlanId"] = new SelectList(_context.Plans, "PlanId", "Name", member.PlanId);
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Member member, IFormFile? profileFile)
        {
            if (id != member.MemberId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Fetch the existing member to preserve data (like the old image if no new one is uploaded)
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
                        member.ProfileImage = uniqueFileName; // Set new image
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
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .Include(m => m.Plan)
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

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
