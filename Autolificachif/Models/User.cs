using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Models
{
    public class User
    {


        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }

    //    UserProfile profile2 = db.UserProfiles.FirstOrDefault(p => p.User.Login == "login2");
    //if(profile2!=null)
    //{
    //    profile2.Name = "Alice II";
    //    db.Entry(profile2).State = EntityState.Modified;
    //    db.SaveChanges();
    //}
    //void Method()
    //    {
    //        using (SqlDbContext  gg=new SqlDbContext)
    //        {
    //            User arrau = new User { Email = "dfdfd", Password = "dfdfdf" };
    //            User arrau2 = new User { Email = "dfdfd", Password = "dfdfdf" };
    //            gg.Users.AddRange(new List<User> { arrau, arrau2 });
    //            gg.SaveChanges();
    //            Role nn = new Role { Name = "dfdf", Id = 1 };
    //            Role nn2 = new Role { Name = "dfdf", Id = 1 };
    //            gg.Users.AddRange(new List<Role> { nn, nn2 });
    //            gg.SaveChanges();
    //            foreach (User dd in gg.Users.Incude("Profile").ToList())
    //            {


    //            }

               
    //        }
    //        using (SqlDbContext gg = new SqlDbContext)
    //        {
    //            User arrau = gg.Users.FirstOrDefault();
    //            arrau.Password = "34343";
    //            gg.Entry(arrau).State= EntityState.Modified;
    //        }

        //public string City { get; set; }
        //public string Company { get; set; }
        //public int Year { get; set; }
    }
    //Данные классы связаны отношением один-ко-многим, то есть один пользователь может иметь только одну роль, а к одной роли могут принадлежать несколько пользователей.
   
    
    
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Userss { get; set; }
        public Role()
        {
            Userss = new List<User>();
        }
    }
}
