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
    public class BookController : Controller
    {
        private readonly LibraryContext _context;
        private const string PATH = "/Data/";
        public BookController(LibraryContext context)
        {
            _context = context;
        }
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Book = _context.Book
                .Include(bk => bk.Authors)
                .ThenInclude(baj => baj.Author)
                .Include(bk => bk.Publisher)
                .Include(bk => bk.Categories)
                .ThenInclude(ctg => ctg.Category)
                .Include(bk => bk.Language)
                .Select(bk => new BookDetailViewModel
                {
                    ISBN = bk.ISBN,
                    Title = bk.Title,
                    Authors = bk.Authors.Select(at => at.Author).ToList(),
                    Categories = bk.Categories.Select(ctg => ctg.Category).ToList(),
                    Country = bk.Country,
                    Language = bk.Language,
                    PublicationDate = bk.PublicationDate,
                    Publisher = bk.Publisher,
                    TotalBorrowed = bk.TotalBorrowed,
                    Description = bk.Description,
                    ImageURL = PATH + bk.ISBN + ".jpg",
                })
                .AsNoTracking()
                .Single(bk => bk.ISBN == id);
                
            if (Book == null)
            {
                return NotFound();
            }
            return View(Book);
        }
    }
}