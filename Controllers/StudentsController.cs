using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentRegistrationAPI.Data;
using StudentRegistrationAPI.Models;

namespace StudentRegistrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(StudentRequest student)
        {
            var data = new Student
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email
            };
            _context.Students.Add(data);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, [FromBody] StudentUpdateDto studentDto)
        {
            try
            {
               

                // Validate the DTO
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Fetch the existing student from the database
                var existingStudent = await _context.Students.FindAsync(id);

                // Check if the student with the specified ID exists
                if (existingStudent == null)
                {
                    return NotFound("Student not found");
                }

                // Update the properties of the existing student
                existingStudent.FirstName = studentDto.FirstName;
                existingStudent.LastName = studentDto.LastName;

                // Mark the entity as modified and save changes
                _context.Entry(existingStudent).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                // Return a success response
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.Error.WriteLine($"Error updating student: {ex}");

                // Return a generic error response
                return StatusCode(500, "An unexpected error occurred while updating the student.");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
