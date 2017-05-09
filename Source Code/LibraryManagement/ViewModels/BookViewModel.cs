using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.ViewModels
{
    public class BookItemViewModel
    {
        public string ISBN { get; set; }
        public string ImageURL { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string PublicationDate { get; set; }
    }

    public class BookDetailViewModel
    {
        public string ISBN { get; set; }
        public string ImageURL { get; set; }
        public string Title { get; set; }
        public ICollection<Author> Authors { get; set; }
        public ICollection<Category> Categories { get; set; }
        public Publisher Publisher { get; set; }
        public DateTime PublicationDate { get; set; }
        public Language Language { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int TotalBorrowed { get; set; }
    }
}
