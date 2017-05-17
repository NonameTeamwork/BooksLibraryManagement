using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.ViewModels
{
    public class IndexBookViewModel
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM:dd:yyyy}", ApplyFormatInEditMode = false)]
        public DateTime DateofImport { get; set; }
        public int TotalBorrowed { get; set; }
    }
    public class CreateBookViewModel
    {
        [Required(ErrorMessage = "You must enter ISBN")]
        [StringLength(13, ErrorMessage = "ISBN must containts 13 digits")]
        public string ISBN { get; set; }
        [Required(ErrorMessage = "You must enter Title")]
        [StringLength(150)]
        public string Title { get; set; }
        [Required(ErrorMessage = "You must enter the authors")]
        public List<string> Authors { get; set; }
        [Required(ErrorMessage = "You must enter the categories")]
        public List<string> Categories { get; set; }
        [Required(ErrorMessage = "Please enter the publisher")]
        public string Publisher { get; set; }
        [Required(ErrorMessage ="You must enter date of publication")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM:dd:yyyy}", ApplyFormatInEditMode = true)]
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
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM:dd:yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateofImport { get; set; }
        [Required(ErrorMessage = "You must enter book condition")]
        public string Condition { get; set; }
    }
}
