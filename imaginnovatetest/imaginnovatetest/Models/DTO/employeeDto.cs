using System.ComponentModel.DataAnnotations;
namespace imaginnovatetest.Models.DTO
{
    public class employeeDto
    {
        public int Empid { get; set; }

        [RegularExpression(@"^([a-zA-Z ]+)$", ErrorMessage = "Please enter characters only")]
        public string? Firstname { get; set; }
        [RegularExpression(@"^([a-zA-Z ]+)$", ErrorMessage = "Please enter characters only")]
        public string? Lastname { get; set; }
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Please enter valid e-mail address.")]
        public string? Email { get; set; }
        [RegularExpression(@"^([0-9]{10,15})$", ErrorMessage = "Invalid phonenumber.")]
        public string? Phonenumber { get; set; }
        public DateTime doj { get; set; }
        public decimal? Salary { get; set; }
    }
}
