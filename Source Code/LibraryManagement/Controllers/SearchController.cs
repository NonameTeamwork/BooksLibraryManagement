using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using LibraryManagement.Models;
using LibraryManagement.ViewModels;

namespace LibraryManagement.Controllers
{
    public class SearchController : Controller
    {
        private readonly LibraryContext _context;
        private const string IMAGEPATH = "Data/";
        public SearchController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(string searchString,
            string currentFilter,
            string sortOrder,
            int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.currentFilter = searchString;
            var Books = _context.Book
                .Include(bk => bk.Authors)
                .Select(bk => new BookItemViewModel
                {
                    ISBN = bk.ISBN,
                    ImageURL = IMAGEPATH + bk.ISBN + ".jpg",
                    Title = bk.Title,
                    Author = string.Join(",", bk.Authors.Select(at => at.Author.Name).ToArray()),
                    PublicationDate = bk.PublicationDate.ToString(),
                });
            if (!String.IsNullOrEmpty(searchString))
            {
                Books = Books.Where(bk => bk.ISBN.Equals(searchString) ||
                bk.Title.ToLower().Contains(searchString.ToLower()) ||
                bk.Author.ToLower().Contains(searchString.ToLower()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    Books = Books.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    Books = Books.OrderBy(s => s.PublicationDate);
                    break;
                case "date_desc":
                    Books = Books.OrderByDescending(s => s.PublicationDate);
                    break;
                default:
                    Books = Books.OrderBy(s => s.Title);
                    break;
            }

            int pageSize = 20;
            return View(PaginatedList<BookItemViewModel>.Create(Books.AsNoTracking(), page ?? 1, pageSize));
        }


    }
}
