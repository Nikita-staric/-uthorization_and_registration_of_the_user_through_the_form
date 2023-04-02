using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace Authentication.Models
{

    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }



      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            string adminRoreName = "admin";
            string userRoleName = "user";


            string adminEmail = "admin@mail.ru";
            string adminPassword = "123456";

            Role adminRole = new Role { Id = 1, Name = adminRoreName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            User adminUser = new User { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };
            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            base.OnModelCreating(modelBuilder);
          
            //Для инициализации базы данных в методе OnModelCreating() добавляются в бд две роли и один пользователь - администратора.
        }


    }
}
    
