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
using LibraryManagement.Areas.Admin.ViewModels;

namespace LibraryManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class BookManagementController : Controller
    {
        private readonly LibraryContext _dbcontext;

        private const string PATH = "/Data/";

        public BookManagementController(LibraryContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<IActionResult> Index(string sortOrder,
            string currentFilter,
            string searchString,
            int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParam = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.NumbBorrowParam = sortOrder == "Borrow" ? "borrow_desc" : "Borrow";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var book = _dbcontext.Book
                .Select(bk => new IndexBookViewModel
                {
                    ISBN = bk.ISBN,
                    Title = bk.Title,
                    Publisher = bk.Publisher.Name,
                    TotalBorrowed = bk.TotalBorrowed,
                    DateofImport = bk.DateofImport
                });

            if (!String.IsNullOrEmpty(searchString))
            {
                book = book.Where(bk => bk.Title.ToLower().Contains(searchString.ToLower()) ||
                                        bk.ISBN.Equals(searchString) ||
                                        bk.Publisher.ToLower().Contains(searchString.ToLower()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    book = book.OrderByDescending(bk => bk.Title);
                    break;
                case "Date":
                    book = book.OrderBy(bk => bk.DateofImport);
                    break;
                case "date_desc":
                    book = book.OrderByDescending(bk => bk.DateofImport);
                    break;
                case "Borrow":
                    book = book.OrderByDescending(bk => bk.TotalBorrowed);
                    break;
                case "borrow_desc":
                    book = book.OrderBy(bk => bk.TotalBorrowed);
                    break;
                default:
                    book = book.OrderBy(bk => bk.Title);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<IndexBookViewModel>.CreateAsync(book.AsNoTracking(), page ?? 1, pageSize));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookViewModel model)
        {
            //If create book is exist in database then add new copy with id no = number of copy of this book in database
            if (_dbcontext.Book.Any(bk => bk.ISBN == model.ISBN))
            {
                var noofBook = _dbcontext.BookCopyDetail.Where(bcd => bcd.ISBN == model.ISBN).Count() - 1;
                var bookCopy = new BookCopyDetail
                {
                    ISBN = model.ISBN,
                    CopyNo = noofBook,
                    Status = _dbcontext.BookStatus.Single(bs => bs.Name == "Available"),
                    Condition = model.Condition,
                    
                };
                if (model.DateofImport != null)
                    bookCopy.DateofImport = model.DateofImport;
                _dbcontext.BookCopyDetail.Add(bookCopy);
                await _dbcontext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                // Create new book
                var newBook = new Book
                {
                    ISBN = model.ISBN,
                    Title = model.Title,
                    PublicationDate = model.PublicationDate,
                    Country = model.Country,
                    Description = model.Description,
                    Price = model.Price,
                };
                if (model.DateofImport != null)
                    newBook.DateofImport = model.DateofImport;

                //Map relationship between new book and author
                foreach (var author in model.Authors)
                {

                    var anAuthor = await _dbcontext.Author.SingleOrDefaultAsync(at => at.Name == author);
                    if (anAuthor == null)
                    {
                        var newAuthor = new Author
                        {
                            Name = author,
                        };
                        await _dbcontext.Author.AddAsync(newAuthor);

                        var authorJoiner = new BookAuthorJoiner
                        {
                            Author = newAuthor,
                            BookInfo = newBook,
                        };
                        await _dbcontext.BookAuthorJoiner.AddAsync(authorJoiner);
                    }
                    else
                    {
                        var authorJoiner = new BookAuthorJoiner
                        {
                            Author = anAuthor,
                            BookInfo = newBook,
                        };
                        await _dbcontext.BookAuthorJoiner.AddAsync(authorJoiner);
                    }
                }

                //Map relationship between new book and category
                foreach (var category in model.Categories)
                {
                    var aCategory = await _dbcontext.Category.SingleOrDefaultAsync(ctg => ctg.Name == category);
                    if (aCategory == null)
                    {
                        var newCategory = new Category
                        {
                            Name = category,
                        };
                        await _dbcontext.Category.AddAsync(newCategory);

                        var categoryJoiner = new BookCategoryJoiner
                        {
                            Category = newCategory,
                            BookInfo = newBook,
                        };
                        await _dbcontext.BookCategoryJoiner.AddAsync(categoryJoiner);
                    }
                    else
                    {
                        var categoryJoiner = new BookCategoryJoiner
                        {
                            Category = aCategory,
                            BookInfo = newBook,
                        };
                        await _dbcontext.BookCategoryJoiner.AddAsync(categoryJoiner);
                    }
                }

                //Add new copy to book copy detail with id no = 0
                _dbcontext.BookCopyDetail.Add(new BookCopyDetail
                {
                    BookInfo = newBook,
                    Condition = model.Condition,
                    CopyNo = 0,
                    Status = await _dbcontext.BookStatus.SingleOrDefaultAsync(bs => bs.Name == "Available"),
                });
                await _dbcontext.Book.AddAsync(newBook);

                var publisher = await _dbcontext.Publisher.SingleOrDefaultAsync(pl => pl.Name == model.Publisher);
                if (publisher == null)
                {
                    var newPublisher = new Publisher
                    {
                        Name = model.Publisher,
                    };
                    await _dbcontext.Publisher.AddAsync(newPublisher);
                    newBook.Publisher = newPublisher;
                }
                else
                    newBook.Publisher = publisher;


                var language = await _dbcontext.Language.SingleOrDefaultAsync(lg => lg.Name == model.Language);
                if (language == null)
                {
                    var newLanguage = new Language
                    {
                        Name = model.Language,

                    };
                    await _dbcontext.Language.AddAsync(newLanguage);
                    newBook.Language = newLanguage;
                }
                else
                    newBook.Language = language;

                await _dbcontext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }


        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Book = _dbcontext.Book
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
        public async Task<IActionResult> Delete(string id)
        {
            return RedirectToAction("Details", new { id = id });
        }
        public async Task<IActionResult> DeleteBookAbsolutely(string id)
        {
            if (id == null)
                return NotFound();

            var book = await _dbcontext.Book.SingleOrDefaultAsync(x => x.ISBN == id);

            if (book == null)
                return NotFound();
            _dbcontext.Book.Remove(book);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> EditView(string id)
        {
            var Book = _dbcontext.Book
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
                return NotFound();

            //ViewBag.AuthorsList = new MultiSelectList(_dbcontext.Author.ToList(),"Id","Name");
            //ViewBag.CategoriesList = new MultiSelectList(_dbcontext.Category.ToList().OrderBy(x => x.Name), "Id", "Name");
            //ViewBag.PublisherList = new MultiSelectList(_dbcontext.Publisher.ToList(), "Id", "Name");

            var Authors = await _dbcontext.Author.Select(at => at.Name).ToListAsync();
            ViewBag.AuthorsList = new MultiSelectList(Authors, "Name");
            var Categories = await _dbcontext.Category.Select(ctg => ctg.Name).ToListAsync();
            ViewBag.CategoriesList = new MultiSelectList(Categories, "Name");
            var Publisher = await _dbcontext.Publisher.Select(ctg => ctg.Name).ToListAsync();
            ViewBag.PublishersList = new List<String>(Publisher);


            return View(Book);
        }
    }
}