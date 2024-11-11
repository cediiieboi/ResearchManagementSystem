using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResearchManagementSystem.Data;
using ResearchManagementSystem.Models;
using ResearchManagementSystem.Services;

namespace ResearchManagementSystem.Controllers
{
    public class AddAccomplishmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserService _userService;

        public AddAccomplishmentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, UserService userService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;

        }

        // GET: AddAccomplishments
        public async Task<IActionResult> Index()
        {
            // Get the current user
            var currentUser = await _userManager.GetUserAsync(User);

            // Fetch roles assigned to the current user
            var userRoles = await _userManager.GetRolesAsync(currentUser);

            // Fetch accomplishments from the database with related entities included
            IQueryable<AddAccomplishment> applicationDbContext = _context.Production
                .Include(a => a.CoLeadResearcher)
                .Include(a => a.LeadResearcher)
                .Include(a => a.Memberone)
                .Include(a => a.Membertwo)
                .Include(a => a.Memberthree)
            .Include(a => a.CreatedBy);



            // Filter accomplishments based on the user's role
            if (userRoles.Contains("Faculty"))
            {
                // If the user is a faculty member, show only their accomplishments
                applicationDbContext = applicationDbContext.Where(a =>
                    a.LeadResearcherId == currentUser.Id ||
                    a.CoLeadResearcherId == currentUser.Id ||
                    a.MemberoneId == currentUser.Id ||
                    a.MembertwoId == currentUser.Id ||
                    a.MemberthreeId == currentUser.Id);
            }
            else if (userRoles.Contains("RMCC"))
            {
                // If the user is RMCC, show all accomplishments where the Notes field contains the current user's ID
                applicationDbContext = applicationDbContext.Where(a =>
                    a.Notes == currentUser.Id ||
                    a.CreatedById == currentUser.Id); // Use Contains to match the Notes field



            }


            // Execute the query and return the result to the view
            var result = await applicationDbContext.ToListAsync();

            // Log the number of records returned for debugging
            Console.WriteLine($"Number of Accomplishments Retrieved: {result.Count}");

            return View(result);
        }








        // GET: AddAccomplishments/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addAccomplishment = await _context.Production
                .Include(a => a.CoLeadResearcher)
                .Include(a => a.LeadResearcher)
                .Include(a => a.Memberone)
                .Include(a => a.Memberthree)
                .Include(a => a.Membertwo)
                .FirstOrDefaultAsync(m => m.ProductionId == id);
            if (addAccomplishment == null)
            {
                return NotFound();
            }

            return View(addAccomplishment);
        }

        // GET: AddAccomplishments/Create

        [Authorize(Roles = "RMCC")]
        public IActionResult Create()
        {
            // Define a list of college options
            var collegeOptions = new List<string>
    {
        "College of Engineering",
        "College of Arts and Sciences",
        "College of Business Administration",
        "College of Education"
        // Add other colleges as needed
    };

            // Pass the college options to the view using ViewBag
            ViewBag.CollegeOptions = new SelectList(collegeOptions);

            // Define a list of college options
            var DepartmentOptions = new List<string>
    {
        "Information Technology",
        "Arts and Sciences",
        "Business Administration",
        "Education"
        // Add other departments as needed
    };

            // Pass the college options to the view using ViewBag
            ViewBag.DevelopmentOptions = new SelectList(DepartmentOptions);

            ViewData["CoLeadResearcherId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["LeadResearcherId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["MemberoneId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["MemberthreeId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["MembertwoId"] = new SelectList(_context.Users, "Id", "Id");

            ViewBag.DevelopmentOptions = new SelectList(DepartmentOptions);

            var researchers = _context.Users.Select(r => new SelectListItem
            {
                Text = $"{r.FirstName} {r.LastName}",  // Concatenate using string interpolation
                Value = r.Id.ToString()  // Keep ID as the value
            }).ToList();

            ViewBag.LeadResearcherId = new SelectList(researchers, "Value", "Text");
            ViewBag.CoLeadResearcherId = new SelectList(researchers, "Value", "Text");
            ViewBag.MemberoneId = new SelectList(researchers, "Value", "Text");
            ViewBag.MembertwoId = new SelectList(researchers, "Value", "Text");
            ViewBag.MemberthreeId = new SelectList(researchers, "Value", "Text");

            return View();
        }

        // POST: AddAccomplishments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductionId,ResearchTitle,LeadResearcherId,CoLeadResearcherId,MemberoneId,MembertwoId,MemberthreeId,College,Department,FundingAgency,FundingAmount,DateStarted,DateCompleted,RequiredFile1Data,RequiredFile2Data,ConditionalFileData,RequiredFile1Name,RequiredFile2Name,ConditionalFileName,IsStudentInvolved,Notes")] AddAccomplishment addAccomplishment, IFormFile? RequiredFile1Data, IFormFile? RequiredFile2Data, IFormFile? ConditionalFileData)
        {
            if (ModelState.IsValid)
            {

                // Get the current user
                var currentUser = await _userManager.GetUserAsync(User);

                // Set the Notes field with the current user's ID
                addAccomplishment.Notes = $" Created by User ID: {currentUser.Id} - User: {currentUser.UserName}";


                // Generate a unique ProductionId (for example, using GUID)
                addAccomplishment.ProductionId = Guid.NewGuid().ToString();



                if (RequiredFile1Data != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await RequiredFile1Data.CopyToAsync(memoryStream);
                        addAccomplishment.RequiredFile1Data = memoryStream.ToArray();
                        addAccomplishment.RequiredFile1Name = RequiredFile1Data.FileName;
                    }
                }



                if (RequiredFile2Data != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await RequiredFile2Data.CopyToAsync(memoryStream);
                        addAccomplishment.RequiredFile2Data = memoryStream.ToArray();
                        addAccomplishment.RequiredFile2Name = RequiredFile2Data.FileName;
                    }
                }

                if (ConditionalFileData != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await ConditionalFileData.CopyToAsync(memoryStream);
                        addAccomplishment.ConditionalFileData = memoryStream.ToArray();
                        addAccomplishment.ConditionalFileName = ConditionalFileData.FileName;
                    }
                }





                _context.Add(addAccomplishment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoLeadResearcherId"] = new SelectList(_context.Users, "Id", "Id", addAccomplishment.CoLeadResearcherId);
            ViewData["LeadResearcherId"] = new SelectList(_context.Users, "Id", "Id", addAccomplishment.LeadResearcherId);
            ViewData["MemberoneId"] = new SelectList(_context.Users, "Id", "Id", addAccomplishment.MemberoneId);
            ViewData["MemberthreeId"] = new SelectList(_context.Users, "Id", "Id", addAccomplishment.MemberthreeId);
            ViewData["MembertwoId"] = new SelectList(_context.Users, "Id", "Id", addAccomplishment.MembertwoId);

            return View(addAccomplishment);
        }
        [Authorize(Roles = "RMCC")]
        // GET: AddAccomplishments/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addAccomplishment = await _context.Production.FindAsync(id);
            if (addAccomplishment == null)
            {
                return NotFound();
            }
            ViewData["CoLeadResearcherId"] = new SelectList(_context.Users, "Id", "Id", addAccomplishment.CoLeadResearcherId);
            ViewData["LeadResearcherId"] = new SelectList(_context.Users, "Id", "Id", addAccomplishment.LeadResearcherId);
            ViewData["MemberoneId"] = new SelectList(_context.Users, "Id", "Id", addAccomplishment.MemberoneId);
            ViewData["MemberthreeId"] = new SelectList(_context.Users, "Id", "Id", addAccomplishment.MemberthreeId);
            ViewData["MembertwoId"] = new SelectList(_context.Users, "Id", "Id", addAccomplishment.MembertwoId);
            return View(addAccomplishment);
        }

        // POST: AddAccomplishments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ProductionId,ResearchTitle,LeadResearcherId,CoLeadResearcherId,MemberoneId,MembertwoId,MemberthreeId,College,Department,FundingAgency,FundingAmount,DateStarted,DateCompleted,RequiredFile1Data,RequiredFile2Data,ConditionalFileData,RequiredFile1Name,RequiredFile2Name,ConditionalFileName,IsStudentInvolved,Notes")] AddAccomplishment addAccomplishment)
        {
            if (id != addAccomplishment.ProductionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(addAccomplishment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddAccomplishmentExists(addAccomplishment.ProductionId))
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
            ViewData["CoLeadResearcherId"] = new SelectList(_context.Users, "Id", "Id", addAccomplishment.CoLeadResearcherId);
            ViewData["LeadResearcherId"] = new SelectList(_context.Users, "Id", "Id", addAccomplishment.LeadResearcherId);
            ViewData["MemberoneId"] = new SelectList(_context.Users, "Id", "Id", addAccomplishment.MemberoneId);
            ViewData["MemberthreeId"] = new SelectList(_context.Users, "Id", "Id", addAccomplishment.MemberthreeId);
            ViewData["MembertwoId"] = new SelectList(_context.Users, "Id", "Id", addAccomplishment.MembertwoId);
            return View(addAccomplishment);
        }

        // GET: AddAccomplishments/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var addAccomplishment = await _context.Production
                .Include(a => a.CoLeadResearcher)
                .Include(a => a.LeadResearcher)
                .Include(a => a.Memberone)
                .Include(a => a.Memberthree)
                .Include(a => a.Membertwo)
                .FirstOrDefaultAsync(m => m.ProductionId == id);
            if (addAccomplishment == null)
            {
                return NotFound();
            }

            return View(addAccomplishment);
        }

        // POST: AddAccomplishments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var addAccomplishment = await _context.Production.FindAsync(id);
            if (addAccomplishment != null)
            {
                _context.Production.Remove(addAccomplishment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddAccomplishmentExists(string id)
        {
            return _context.Production.Any(e => e.ProductionId == id);
        }



        public IActionResult DownloadFile(string id, string fileType)
        {
            // Example: Retrieve file data from the database based on the file ID and fileType
            var accomplishment = _context.Production.Find(id);
            if (accomplishment == null) return NotFound();

            byte[] fileData;
            string fileName;

            switch (fileType)
            {
                case "SOAbstractFileData":
                    fileData = accomplishment.RequiredFile1Data;
                    fileName = accomplishment.RequiredFile1Name;
                    break;
                case "RequiredFile1":
                    fileData = accomplishment.RequiredFile2Data;
                    fileName = accomplishment.RequiredFile2Name;
                    break;
                case "RequiredFile2":
                    fileData = accomplishment.RequiredFile3Data;
                    fileName = accomplishment.RequiredFile3Name;
                    break;
                case "ConditionalFile":
                    fileData = accomplishment.ConditionalFileData;
                    fileName = accomplishment.ConditionalFileName;
                    break;
                default:
                    return BadRequest("Invalid file type specified.");
            }

            if (fileData == null) return NotFound("File data not found.");

            // Return the file to the client
            return File(fileData, "application/octet-stream", fileName);
        }

    }



}