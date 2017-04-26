using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class BookInfo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(13), MinLength(13)]
        [Column(TypeName =("char(13)"))]
        public string ISBN { get; set; }
        [Required]
        [Column(TypeName = ("varchar(200)"))]
        public string Title { get; set; }
        public List<BookAuthorJoiner> Authors { get; set; }
        public List<BookCategoryJoiner> Categories { get; set; }
        public Publisher Publisher { get; set; }
        public DateTime PublicationDate { get; set; }
        [Column(TypeName = ("varchar(50)"))]
        public Language Language { get; set; }
        [Column(TypeName = ("varchar(50)"))]
        public string Country { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TotalBorrowed { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateofImport { get; set; }
        public List<BookCopyDetail> BooksCopy { get; set; }

    }

    public class BookAuthorJoiner
    {
        public string ISBN { get; set; }
        public BookInfo BookInfo { get; set; }
        public int AuthorID { get; set; }
        public Author Author { get; set; }
    }

    public class BookCategoryJoiner
    {
        public string ISBN { get; set; }
        public BookInfo BookInfo { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }

    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookAuthorJoiner> Books { get; set; }
    }

    public enum BookState
    {
        Available,
        Borrowed,
    }

    public class BookCopyDetail
    {
        public string ISBN { get; set; }
        public int CopyNo { get; set; }
        public BookInfo BookInfo { get; set; }
        public string Condition { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public BookState State { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateofImport { get; set; }
    }

    public class Publisher
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookInfo> Books { get; set; }
    }

    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookCategoryJoiner> Books { get; set; }
    }

    public class Language
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookInfo> Books { get; set; }
    }


}
