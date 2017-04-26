using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.ViewModels;

namespace LibraryManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly LibraryContext _context;
        public HomeController (LibraryContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeViewModel HomeViewModel = new HomeViewModel();

            IEnumerable<BookLatestViewModel> BookLatest = _context.BookInfo
                .Include(bk => bk.Authors)
                .ThenInclude(baj => baj.Author)
                .OrderByDescending(bk => bk.DateofImport)
                .Select(bk => new BookLatestViewModel
                {
                    Title = bk.Title,
                    Author = string.Join(",", bk.Authors.Select(at => at.Author.Name).ToArray()),
                }).ToList().Take(12);

            IEnumerable<BookBorrowedViewModel> BookMostBorrowed = _context.BookInfo
                .Include(bk => bk.Authors)
                .ThenInclude(baj => baj.Author)
                .Include(bk => bk.Publisher)
                .OrderByDescending(bk => bk.TotalBorrowed)
                .Select(bk => new BookBorrowedViewModel
                {
                    Title = bk.Title,
                    Author = string.Join(",", bk.Authors.Select(at => at.Author.Name).ToArray()),
                    Publisher = bk.Publisher.Name
                }).ToList().Take(2);

            HomeViewModel.BookLatest = BookLatest;
            HomeViewModel.BookBorrowed = BookMostBorrowed;
            HomeViewModel.BookDay = getBookofTheDay();
            return View(HomeViewModel);
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }

        public IActionResult AccessDenied()
        {
            return View("~/Views/Shared/AccessDenied.cshtml");
        }

        private BookofTheDayViewModel getBookofTheDay()
        {
            DateTime DateofCreationNumb = DateTime.Parse(_context.Parameter
                .Where(param => param.ParameterName == "DateofCreationNumb")
                .Select(param => param.Value).
                Single());
            if (!DateTime.Now.Date.Equals(DateofCreationNumb.Date))
            {
                var Param1 = _context.Parameter.Where(param => param.ParameterName == "RadomNumb").Single();
                int randNumb = new Random().Next(0, _context.BookInfo.Count());
                Param1.Value = randNumb.ToString();
                var Param2 = _context.Parameter.Where(param => param.ParameterName == "DateofCreationNumb").Single();
                Param2.Value = DateTime.Now.ToString();
                _context.SaveChanges();
                return getBookofTheDayInfo(randNumb);
            }
            else
            {
                int randNumb = int.Parse(_context.Parameter.Where(param => param.ParameterName == "RadomNumb")
                    .Single().Value);
                return getBookofTheDayInfo(randNumb);
            }

        }

        private BookofTheDayViewModel getBookofTheDayInfo(int randNumb)
        {
            BookofTheDayViewModel bookday = _context.BookInfo
                .Include(bk => bk.Authors)
                .ThenInclude(baj => baj.Author)
                .Include(bk => bk.Publisher)
                .Where(bk => bk.Id == randNumb)
                .Select(bk => new BookofTheDayViewModel
                {
                    Title = bk.Title,
                    Author = string.Join(",", bk.Authors.Select(at => at.Author.Name).ToArray()),
                    Description = bk.Description,
                    Publisher = bk.Publisher.Name
                }).Single();
            return bookday;
        }
    }
}
