using System.ComponentModel.DataAnnotations;

namespace Authentication.Models
{
    public class Regist
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [MinLength(6, ErrorMessage = "Пароль должен содержать не менее 6 символов")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]

        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }

        public int UserId { get; set; }
        //public User User { get; set; } я ее убрал и прошел валидацию 


        //public string City { get; set; }
        //public string Company { get; set; }
        //public int Year { get; set; }
    }


}
