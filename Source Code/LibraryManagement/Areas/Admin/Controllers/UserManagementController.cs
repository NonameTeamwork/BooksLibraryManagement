using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using LibraryManagement.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace LibraryManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class UserManagementController : Controller
    {
        private readonly LibraryContext _dbcontext;

        public UserManagementController(LibraryContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public IActionResult Index()
        {
            var User = _dbcontext.User.ToList();
            //int page = 1;
            //int pageSize = 10;
            if (User == null)
                return NotFound();
            return View(User);
        }
        public IActionResult Details(string id)
        {
            var user = _dbcontext.User.SingleOrDefault(x => x.Id == id);
            if (user == null)
                return NotFound();

            return View(user);
        }
    }
}