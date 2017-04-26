using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class LibraryContext : DbContext
    {
        public DbSet<BookInfo> BookInfo { get; set; }
        public DbSet<BookAuthorJoiner> BookAuthorJoiner { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<BookCopyDetail> BookCopyDetail { get; set;}
        public DbSet<Category> Category { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<Language> Language { get; set; }

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
                .HasKey(baj => new { baj.ISBN, baj.AuthorName });

            modelBuilder.Entity<BookAuthorJoiner>()
                .HasOne(baj => baj.BookInfo)
                .WithMany(bk => bk.Authors)
                .HasForeignKey(baj => baj.ISBN)
                .HasPrincipalKey(bk => bk.ISBN);

            modelBuilder.Entity<BookAuthorJoiner>()
                .HasOne(baj => baj.Author)
                .WithMany(at => at.Books)
                .HasForeignKey(baj => baj.AuthorName)
                .HasPrincipalKey(at => at.Name);

            //create relationship between bookinfo and category
            modelBuilder.Entity<BookInfo>()
                .HasOne(bk => bk.Category)
                .WithMany(ctg => ctg.Books);

            //create relationship between bookinfo and publisher
            modelBuilder.Entity<BookInfo>()
                .HasOne(bk => bk.Publisher)
                .WithMany(pl => pl.Books);

            //create relationship between bookinfo and language
            modelBuilder.Entity<BookInfo>()
                .HasOne(bk => bk.Language)
                .WithMany(lg => lg.Books);

        }
    }
}
