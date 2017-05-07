using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.ViewModels;
using System.IO;

namespace LibraryManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly LibraryContext _context;
        private const string IMAGEPATH = "Data/";
        public HomeController (LibraryContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModels HomeViewModel = new HomeViewModels();
            var BookLatest = await _context.Book
                .Include(bk => bk.Authors)
                .ThenInclude(baj => baj.Author)
                .OrderByDescending(bk => bk.DateofImport)
                .Select(bk => new BookLatestViewModel
                {
                    ImageURL = IMAGEPATH + bk.ISBN + ".jpg",
                    ISBN = bk.ISBN,
                    Title = bk.Title,
                    Author = string.Join(",", bk.Authors.Select(at => at.Author.Name).ToArray()),
                }).Take(12).ToListAsync();

            var BookMostBorrowed = await _context.Book
                .Include(bk => bk.Authors)
                .ThenInclude(baj => baj.Author)
                .Include(bk => bk.Publisher)
                .OrderByDescending(bk => bk.TotalBorrowed)
                .Select(bk => new BookBorrowedViewModel
                {
                    ImageURL = IMAGEPATH + bk.ISBN + ".jpg",
                    ISBN = bk.ISBN,
                    Title = bk.Title,
                    Author = string.Join(",", bk.Authors.Select(at => at.Author.Name).ToArray()),
                    Publisher = bk.Publisher.Name
                }).Take(2).ToListAsync();

            HomeViewModel.BookLatest = BookLatest;
            HomeViewModel.BookBorrowed = BookMostBorrowed;
            HomeViewModel.BookDay = await GetBookofTheDay();
            return View(HomeViewModel);
        }

        private async Task<BookofTheDayViewModel> GetBookofTheDay()
        {
            DateTime DateofCreationNumb = DateTime.Parse( await _context.Parameter
                .Where(param => param.ParameterName == "DateofCreationNumb")
                .Select(param => param.Value)
                .SingleAsync());
            if (!DateTime.Now.Date.Equals(DateofCreationNumb.Date))
            {
                var Param1 = await _context.Parameter.Where(param => param.ParameterName == "RadomNumb").SingleAsync();
                int randNumb = new Random().Next(0, _context.Book.Count() - 1);
                Param1.Value = randNumb.ToString();
                var Param2 = await _context.Parameter.Where(param => param.ParameterName == "DateofCreationNumb").SingleAsync();
                Param2.Value = DateTime.Now.ToString();
                await _context.SaveChangesAsync();
                return await GetBookofTheDayInfo(randNumb);
            }
            else
            {
                int randNumb = int.Parse(_context.Parameter.Where(param => param.ParameterName == "RadomNumb")
                    .Single().Value);
                return await GetBookofTheDayInfo(randNumb);
            }

        }

        private async Task<BookofTheDayViewModel> GetBookofTheDayInfo(int randNumb)
        {
            BookofTheDayViewModel bookday = await _context.Book
                .Include(bk => bk.Authors)
                .ThenInclude(baj => baj.Author)
                .Include(bk => bk.Publisher)
                .Where(bk => bk.Id == randNumb)
                .Select(bk => new BookofTheDayViewModel
                {
                    ImageURL = IMAGEPATH + bk.ISBN + ".jpg",
                    ISBN = bk.ISBN,
                    Title = bk.Title,
                    Author = string.Join(",", bk.Authors.Select(at => at.Author.Name).ToArray()),
                    Description = bk.Description,
                    Publisher = bk.Publisher.Name
                }).SingleAsync();
            return bookday;
        }
    }
}
