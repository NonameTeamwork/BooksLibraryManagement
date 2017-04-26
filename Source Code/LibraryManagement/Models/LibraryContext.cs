using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class LibraryContext : DbContext
    {
        public DbSet<BookInfo> BookInfo { get; set; }
        public DbSet<BookAuthorJoiner> BookAuthorJoiner { get; set; }
        public DbSet<BookCategoryJoiner> BookCategoryJoiner { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<BookCopyDetail> BookCopyDetail { get; set;}
        public DbSet<Category> Category { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<Parameter> Parameter { get; set; }
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //make column become alternate key (unique in each row)
            modelBuilder.Entity<BookInfo>()
                .HasAlternateKey(bk => bk.ISBN);

            modelBuilder.Entity<Category>()
                .HasAlternateKey(ctg => ctg.Name);

            modelBuilder.Entity<Publisher>()
                .HasAlternateKey(pl => pl.Name);

            modelBuilder.Entity<Author>()
                .HasAlternateKey(at => at.Name);

            modelBuilder.Entity<Language>()
                .HasAlternateKey(lg => lg.Name);

            //initialize dateofimport on add automatically with date on adding
            modelBuilder.Entity<BookInfo>()
                .Property(bk => bk.DateofImport)
                .HasDefaultValueSql("convert(datetime, getdate())");

            //Create composite key for BookCopyDetail (ISBN, CopyNo)
            modelBuilder.Entity<BookCopyDetail>()
                .HasKey(bcd => new { bcd.ISBN, bcd.CopyNo });

            //initialize dateofimport on add automatically with date on adding
            modelBuilder.Entity<BookCopyDetail>()
                .Property(bcd => bcd.DateofImport)
                .HasDefaultValueSql("convert(datetime, getdate())");

            //
            modelBuilder.Entity<BookCopyDetail>()
                .HasOne(bcd => bcd.BookInfo)
                .WithMany(bk => bk.BooksCopy)
                .HasForeignKey(bcd => bcd.ISBN)
                .HasPrincipalKey(bk => bk.ISBN);

            //Initialize state of each book when adding in available
            modelBuilder.Entity<BookCopyDetail>()
                .Property(bcd => bcd.State)
                .HasDefaultValue(BookState.Available);

            //Map many to many between BookInfo and Author
            modelBuilder.Entity<BookAuthorJoiner>()
                .HasKey(baj => new { baj.ISBN, baj.AuthorID });

            modelBuilder.Entity<BookAuthorJoiner>()
                .HasOne(baj => baj.BookInfo)
                .WithMany(bk => bk.Authors)
                .HasForeignKey(baj => baj.ISBN)
                .HasPrincipalKey(bk => bk.ISBN);

            modelBuilder.Entity<BookAuthorJoiner>()
                .HasOne(baj => baj.Author)
                .WithMany(at => at.Books)
                .HasForeignKey(baj => baj.AuthorID)
                .HasPrincipalKey(at => at.Id);

            //Map many to many between BookInfo and Category 
            modelBuilder.Entity<BookCategoryJoiner>()
                .HasKey(bcj => new { bcj.ISBN, bcj.CategoryId });

            modelBuilder.Entity<BookCategoryJoiner>()
                .HasOne(bcj => bcj.BookInfo)
                .WithMany(bk => bk.Categories)
                .HasForeignKey(bcj => bcj.ISBN)
                .HasPrincipalKey(bk => bk.ISBN);

            modelBuilder.Entity<BookCategoryJoiner>()
                .HasOne(bcj => bcj.Category)
                .WithMany(ctg => ctg.Books)
                .HasForeignKey(bcj => bcj.CategoryId)
                .HasPrincipalKey(ctg => ctg.Id);

            //create relationship between bookinfo and publisher
            modelBuilder.Entity<BookInfo>()
                .HasOne(bk => bk.Publisher)
                .WithMany(pl => pl.Books);

            //create relationship between bookinfo and language
            modelBuilder.Entity<BookInfo>()
                .HasOne(bk => bk.Language)
                .WithMany(lg => lg.Books);

            //Each time adding new book the time of borrowed will be initialize to 0
            modelBuilder.Entity<BookInfo>()
                .Property(bk => bk.TotalBorrowed)
                .HasDefaultValue(0);
        }
    }

    public class Parameter
    {
        [Key]
        public int ParameterId { get; set; }
        public string ParameterName { get; set; }
        public Boolean Status { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
