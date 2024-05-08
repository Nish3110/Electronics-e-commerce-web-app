using System.ComponentModel.DataAnnotations;

namespace Product_Management_System.Models
{
    public class User
    {
        public int User_Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
        public int Gender { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits and contain only digits")]
        public string Phonenumber { get; set; }
        public string Password { get; set; }
        public int? Is_Deleted { get; set; }

        public int? User_Type { get; set; }


    }
}
