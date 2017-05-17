using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using LibraryManagement.Areas.Admin.ViewModels;

namespace LibraryManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class DashboardController : Controller
    {
        private readonly LibraryContext _dbcontext;
        public DashboardController(LibraryContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public IActionResult Index()
        {
            ViewBag.Users = _dbcontext.User.Count().ToString();
            ViewBag.Books = _dbcontext.Book.Count().ToString();
            var BookStatus = _dbcontext.BookStatus.Single(bs => bs.Name == "Available");
            ViewBag.AvailBooks = _dbcontext.BookCopyDetail.Where(bk => bk.Status == BookStatus).Count();
            return View();
        }

        [HttpPost]
        public IActionResult QuickSendEmail(QuickEmailViewModels model)
        {
            return RedirectToAction("Index");
        }
    }
}