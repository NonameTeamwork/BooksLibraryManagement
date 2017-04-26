using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using LibraryManagement.Models;

namespace LibraryManagement.Migrations
{
    [DbContext(typeof(LibraryContext))]
    partial class LibraryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LibraryManagement.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("Author");
                });

            modelBuilder.Entity("LibraryManagement.Models.BookAuthorJoiner", b =>
                {
                    b.Property<string>("ISBN");

                    b.Property<int>("AuthorID");

                    b.HasKey("ISBN", "AuthorID");

                    b.HasIndex("AuthorID");

                    b.ToTable("BookAuthorJoiner");
                });

            modelBuilder.Entity("LibraryManagement.Models.BookCategoryJoiner", b =>
                {
                    b.Property<string>("ISBN");

                    b.Property<int>("CategoryId");

                    b.HasKey("ISBN", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("BookCategoryJoiner");
                });

            modelBuilder.Entity("LibraryManagement.Models.BookCopyDetail", b =>
                {
                    b.Property<string>("ISBN");

                    b.Property<int>("CopyNo");

                    b.Property<string>("Condition");

                    b.Property<DateTime>("DateofImport")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("convert(datetime, getdate())");

                    b.Property<int>("State")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.HasKey("ISBN", "CopyNo");

                    b.ToTable("BookCopyDetail");
                });

            modelBuilder.Entity("LibraryManagement.Models.BookInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Country")
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("DateofImport")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("convert(datetime, getdate())");

                    b.Property<string>("Description");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("char(13)")
                        .HasMaxLength(13);

                    b.Property<int?>("LanguageId");

                    b.Property<double>("Price");

                    b.Property<DateTime>("PublicationDate");

                    b.Property<int?>("PublisherId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<int>("TotalBorrowed")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("PublisherId");

                    b.ToTable("BookInfo");
                });

            modelBuilder.Entity("LibraryManagement.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("LibraryManagement.Models.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("Language");
                });

            modelBuilder.Entity("LibraryManagement.Models.Parameter", b =>
                {
                    b.Property<int>("ParameterId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ParameterName");

                    b.Property<bool>("Status");

                    b.Property<string>("Type");

                    b.Property<string>("Value");

                    b.HasKey("ParameterId");

                    b.ToTable("Parameter");
                });

            modelBuilder.Entity("LibraryManagement.Models.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("Publisher");
                });

            modelBuilder.Entity("LibraryManagement.Models.BookAuthorJoiner", b =>
                {
                    b.HasOne("LibraryManagement.Models.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LibraryManagement.Models.BookInfo", "BookInfo")
                        .WithMany("Authors")
                        .HasForeignKey("ISBN")
                        .HasPrincipalKey("ISBN")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LibraryManagement.Models.BookCategoryJoiner", b =>
                {
                    b.HasOne("LibraryManagement.Models.Category", "Category")
                        .WithMany("Books")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LibraryManagement.Models.BookInfo", "BookInfo")
                        .WithMany("Categories")
                        .HasForeignKey("ISBN")
                        .HasPrincipalKey("ISBN")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LibraryManagement.Models.BookCopyDetail", b =>
                {
                    b.HasOne("LibraryManagement.Models.BookInfo", "BookInfo")
                        .WithMany("BooksCopy")
                        .HasForeignKey("ISBN")
                        .HasPrincipalKey("ISBN")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LibraryManagement.Models.BookInfo", b =>
                {
                    b.HasOne("LibraryManagement.Models.Language", "Language")
                        .WithMany("Books")
                        .HasForeignKey("LanguageId");

                    b.HasOne("LibraryManagement.Models.Publisher", "Publisher")
                        .WithMany("Books")
                        .HasForeignKey("PublisherId");
                });
        }
    }
}
