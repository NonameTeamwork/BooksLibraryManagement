using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
    public class BookRepository
    {
        private readonly LibraryContext _dbContext;

        public BookRepository(LibraryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Author> getAuthors()
        {
            return _dbContext.Author.ToList();
        }

        public List<Category> getCategories()
        {
            return _dbContext.Category.ToList();
        }

        public List<Publisher> getPubishers()
        {
            return _dbContext.Publisher.ToList();
        }

        public List<Language> getLanguages()
        {
            return _dbContext.Language.ToList();
        }
    }
}
