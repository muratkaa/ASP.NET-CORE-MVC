using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApp.DAL;
using StudentApp.Models;
using StudentApp.Models.DBEntities;

namespace StudentApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentDbContext studentDbContext;

        public StudentController(StudentDbContext studentDbContext)
        {
            this.studentDbContext = studentDbContext;
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(StudentViewModel addStudentRequest)
        {
            var student = new Student()
            {
                Id = addStudentRequest.Id,
                FirstName = addStudentRequest.FirstName,
                LastName = addStudentRequest.LastName,
                Email = addStudentRequest.Email
            };
            await studentDbContext.Students.AddAsync(student);
            await studentDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var students =await studentDbContext.Students.ToListAsync();
            List<StudentViewModel> studentList = new List<StudentViewModel>();

            if (students.Any())
            {
                foreach (var student in students)
                {
                    studentList.Add(new StudentViewModel { Id = student.Id, FirstName = student.FirstName, LastName = student.LastName, Email = student.Email });
                }
            }

            return View(studentList);

        }

        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            var student = await studentDbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (student != null)
            {
                var viewModel = new updateStudentViewmodel()
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Email = student.Email
                };
                return await Task.Run(() => View("View",viewModel));
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult>View(updateStudentViewmodel model)
        {
            var student = await studentDbContext.Students.FindAsync(model.Id);
            if(student != null)
            {
                student.FirstName = model.FirstName;
                student.LastName = model.LastName;
                student.Email = model.Email;

                await studentDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
    
        }


        [HttpPost]
        public async Task<IActionResult> Delete(updateStudentViewmodel model)
        {
            var student = await studentDbContext.Students.FindAsync(model.Id);
            if (student != null)
            {
                studentDbContext.Students.Remove(student);
                await studentDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

    }
}
