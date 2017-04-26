﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.ViewModels
{


    public class BookLatestViewModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        
    }

    public class BookBorrowedViewModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
    }

    public class BookofTheDayViewModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
    }

    public class HomeViewModel
    {
        public IEnumerable<BookLatestViewModel> BookLatest { get; set; }
        public IEnumerable<BookBorrowedViewModel> BookBorrowed { get; set; }
        public BookofTheDayViewModel BookDay { get; set; }
    }
}