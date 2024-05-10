using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Web.Models;
using StudentManagement.Web.Models.Entities;
using StudentManagement.Web.Models.Validations;
using StudentManagement.Web.Persistence;

namespace StudentManagement.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel model)
        {
            AddStudentViewModelValidator validator = new AddStudentViewModelValidator();

            var validatorResults = validator.Validate(model);

            if (validatorResults.IsValid)
            {
                var student = new Student
                {
                    Name = model.Name,
                    Email = model.Email,
                    Phone = model.Phone,
                    isSubscribed = model.isSubscribed
                };

                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await _context.Students.ToListAsync();

            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await _context.Students.FindAsync(id);

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student model)
        {
            var student = await _context.Students.FindAsync(model.Id);

            if (student is not null) 
            {
                student.Name = model.Name;
                student.Email = model.Email;
                student.Phone = model.Phone;
                student.isSubscribed = model.isSubscribed;

                _context.Students.Update(student);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("List", "Student");
        }

        [HttpPost]
        public async Task<IActionResult> Remove(Student model)
        {
            var student = await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (student is not null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("List", "Student");
        }
    }
}
