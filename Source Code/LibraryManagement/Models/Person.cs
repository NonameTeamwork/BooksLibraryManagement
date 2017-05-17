using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    //public class ApplicationUser : IdentityUser
    //{
    //    public Person Person { get; set; }
    //    public string PersonId { get; set; }
    //}
    public abstract class Person : IdentityUser
    {
        //[Key]
        //public int Id { get; set; }
        //public ApplicationUser AppUser { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public DateTime StartDate { get; set; }
        public string HomeTown { get; set; }

        public string GetFullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public static string GenerateId(LibraryContext context, string role)
        {
            string YearDigitPair = (DateTime.UtcNow.Year % 100).ToString();
            string roleId = null, countId = "";
            switch (role)
            {
                case "user":
                    roleId = "00";
                    countId = context.User.Count().ToString();
                    break;
                case "admin":
                    roleId = "11";
                    countId = context.Admin.Count().ToString();
                    break;
            }

            for (int i = countId.Length; i < 4; i++)
            {
                countId = "0" + countId;
            }
            return YearDigitPair + roleId + countId;
        }

    }

    public class User : Person
    {
        public UserStatus Status { get; set; }
    }

    public class Admin : Person
    {
        public AdminStatus Status { get; set; }
    }
}
