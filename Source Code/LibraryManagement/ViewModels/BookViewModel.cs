using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.ViewModels
{
    public class BookItemViewModel
    {
        public string ISBN { get; set; }
        public string ImageURL { get; set; }
        [StringLength(150)]
        public string Title { get; set; }
        public string Author { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM:dd:yyyy}", ApplyFormatInEditMode = false)]
        public DateTime PublicationDate { get; set; }
    }

    public class BookDetailViewModel
    {
        [Required(ErrorMessage = "You must enter ISBN")]
        [StringLength(13, ErrorMessage = "ISBN must containts 13 digits")]
        public string ISBN { get; set; }
        [Required(ErrorMessage ="You must upload an image")]
        public string ImageURL { get; set; }
        [Required(ErrorMessage = "You must enter Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "You must enter the authors")]
        public ICollection<Author> Authors { get; set; }
        [Required(ErrorMessage = "You must enter the categories")]
        public ICollection<Category> Categories { get; set; }
        [Required(ErrorMessage = "Please enter the publisher")]
        public Publisher Publisher { get; set; }
        [Required(ErrorMessage = "You must enter date of publication")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM:dd:yyyy}", ApplyFormatInEditMode = false)]
        public DateTime PublicationDate { get; set; }
        [Required(ErrorMessage = "You must enter language")]
        public Language Language { get; set; }
        [Required(ErrorMessage = "You must enter country")]
        [StringLength(50)]
        public string Country { get; set; }
        [Required(ErrorMessage = "You must enter description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "You must enter the price")]
        public double Price { get; set; }
        public int TotalBorrowed { get; set; }
    }
}
