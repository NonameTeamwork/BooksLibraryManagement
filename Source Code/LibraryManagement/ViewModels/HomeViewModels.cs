using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.ViewModels
{


    public class BookLatestViewModel
    {
        public string ISBN { get; set; }
        public string ImageURL { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }

    public class BookBorrowedViewModel
    {
        public string ISBN { get; set; }
        public string ImageURL { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
    }

    public class BookofTheDayViewModel
    {
        public string ISBN { get; set; }
        public string ImageURL { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
    }

    public class HomeViewModels
    {
        public List<BookLatestViewModel> BookLatest { get; set; }
        public List<BookBorrowedViewModel> BookBorrowed { get; set; }
        public BookofTheDayViewModel BookDay { get; set; }
    }
}
