using LibraryManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
    public class UserRepository
    {
        private readonly Person _person;
        public UserRepository(UserManager<Person> userManager, IHttpContextAccessor httpContextAccessor)
        {
            var task = userManager.GetUserAsync(httpContextAccessor.HttpContext.User);
            task.Wait();
            if (task.IsCompleted)
                _person = task.Result;
        }

        public string GetUserName()
        {
            return _person.UserName;
        }
        public string GetEmail()
        {
            return _person.Email;
        }
        public string GetFirstName()
        {
            return _person.FirstName;
        }
        public string GetLastName()
        {
            return _person.LastName;
        }
        public string GetFullName()
        {
            return _person.GetFullName;
        }

    }
}
