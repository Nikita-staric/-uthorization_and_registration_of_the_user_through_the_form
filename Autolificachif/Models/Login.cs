using System.ComponentModel.DataAnnotations;

namespace Authentication.Models
{
    public class Login
    {

        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }




        //public string City { get; set; }
        //public string Company { get; set; }
        //public int Year { get; set; }
    }
}
