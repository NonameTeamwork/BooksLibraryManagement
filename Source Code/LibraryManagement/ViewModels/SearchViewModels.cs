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
}
