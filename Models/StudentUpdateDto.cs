using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationAPI.Models
{
    public class StudentUpdateDto
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
    }

}
