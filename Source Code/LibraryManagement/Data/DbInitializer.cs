using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryManagement.Models;

namespace LibraryManagement.Data
{
    public static class DbInitializer
    {
        public static void Initialize(LibraryContext context)
        {
            if (context.BookInfo.Any())
            {
                return;
            }
            var Authors = new Author[]
            {
                new Author { Name = "Bruce Springsteen"},
                new Author { Name = "Shoo Rayner"},
                new Author { Name = "Salvador Dali"},
                new Author { Name = "Scott Pape"},
                new Author { Name = "John Brooks"},
                new Author { Name = "Dale Carnegie"},
                new Author { Name = "E. B. White"},
                new Author { Name = "Graham Shields"},
                new Author { Name = "Lonely Planet"},
                new Author { Name = "Christina Lamb"},
                new Author { Name = "Karen Kearns"},
                new Author { Name = "John Dewey"},
                new Author { Name = "Paula Hawkins"},
                new Author { Name = "Paul Beatty"},
                new Author { Name = "J. K. Rowling"},
            };

            foreach (Author author in Authors)
            {
                context.Author.Add(author);
            }
            context.SaveChanges();


            var Languages = new Language[]
            {
                new Language { Name = "English"},
                new Language { Name = "French"},
                new Language { Name = "Spanish"},
                new Language { Name = "German"},
                new Language { Name = "Latin"},
                new Language { Name = "Italian"},
                new Language { Name = "Chinese"},
                new Language { Name = "Russian"},
                new Language { Name = "Japanese"},
                new Language { Name = "Dutch; Flemish"},
                new Language { Name = "Vietnamese"},
            };

            foreach (Language language in Languages)
            {
                context.Language.Add(language);
            }
            context.SaveChanges();

            var Publishers = new Publisher[]
            {
                new Publisher { Name = "Simon & Schuster Ltd"},
                new Publisher { Name = "Shoo Rayner"},
                new Publisher { Name = "Taschen GmbH"},
                new Publisher { Name = "John Wiley & Sons Australia Ltd"},
                new Publisher { Name = "Hodder & Stoughton General Division"},
                new Publisher { Name = "SIMON & SCHUSTER"},
                new Publisher { Name = "Pearson Education (US)"},
                new Publisher { Name = "Palgrave MacMillan"},
                new Publisher { Name = "Lonely Planet Publications Ltd"},
                new Publisher { Name = "Orion Publishing Co"},
                new Publisher { Name = "Cengage Learning Australia"},
                new Publisher { Name = "Transworld Publishers Ltd"},
                new Publisher { Name = "Oneworld Publications"},
                new Publisher { Name = "Bloomsbury Publishing PLC"},
            };

            foreach (Publisher Publisher in Publishers)
            {
                context.Publisher.Add(Publisher);
            }

            context.SaveChanges();


            var Categories = new Category[]
            {
                new Category { Name = "Art & Photography"},
                new Category { Name = "Education"},
                new Category { Name = "Religion"},
                new Category { Name = "History & Archeology"},
                new Category { Name = "Science & Geography"},
                new Category { Name = "Political Science"},
                new Category { Name = "Business"},
                new Category { Name = "Finance & Law"},
                new Category { Name = "Dictionaries & Languages"},
                new Category { Name = "Science"},
                new Category { Name = "Medical"},
                new Category { Name = "Technology & Engineering"},
                new Category { Name = "Children"},
                new Category { Name = "Magazine"},
                new Category { Name = "Comic/Mangan"},
                new Category { Name = "Health"},
                new Category { Name = "Entertainment"},
                new Category { Name = "Biography"},
                new Category { Name = "Food & drink"},
                new Category { Name = "Travel & Holiday Guides"},
                new Category { Name = "Society & Social Science"},
                new Category { Name = "Fiction"},
                new Category { Name = "Personal Development"},
                new Category { Name = "Home & Garden"},
                new Category { Name = "Poetry & Drama"}
            };

            foreach ( Category category in Categories)
            {
                context.Category.Add(category);
            }

            context.SaveChanges();


            var Books = new BookInfo[]
            {
                new BookInfo
                {
                    ISBN = "9781471157790",
                    Title ="Born to Run",
                    Description ="THE NUMBER ONE BESTSELLER 'Writing about yourself " +
                "is a funny business...But in a project like this, the writer has made one promise, to show the reader " +
                "his mind. In these pages, I've tried to do this.' -Bruce Springsteen, from the pages of Born to Run In " +
                "2009, Bruce Springsteen and the E Street Band performed at the Super Bowl's halftime show. The experience" +
                " was so exhilarating that Bruce decided to write about it. That's how this extraordinary autobiography began." +
                " Over the past seven years, Bruce Springsteen has privately devoted himself to writing the story of his life," +
                " bringing to these pages the same honesty, humour and originality found in his songs",
                    Country ="London, United Kingdom",
                    Language = Languages[0],
                    PublicationDate = DateTime.Parse("2016-12-07"),
                    Price = double.Parse("23.03"),
                    Publisher = Publishers[0]
                },

                new BookInfo
                {
                    ISBN = "9781908944191",
                    Title = "Everyone Can Draw",
                    Description ="If you can make a mark on a piece of paper you can draw!" +
                " If you can write your name... you can draw! Millions of people watch Shoo Rayner's Drawing Tutorials on his" +
                " award-winning YouTube channel - ShooRaynerDrawing. learn to draw with Shoo Rayner too! In this book, Shoo shows" +
                " you how, with a little practice, you can learn the basic shapes and techniques of drawing and soon be creating" +
                " your own, fabulous works of art. Everyone can draw. That means you too!",
                    Country = "United States",
                    Language = Languages[0],
                    PublicationDate = DateTime.Parse("2014-03-27"),
                    Price = double.Parse("6.99"),
                    Publisher = Publishers[1]
                },

                new BookInfo
                {
                    ISBN = "9783836508766",
                    Title="Dali: Les Diners De Gala",
                    Description = "Food and surrealism make perfect bedfellows: sex and lobsters, collage and cannibalism, " +
                    "the meeting of a swan and a toothbrush on a pastry case. The opulent dinner parties thrown by Salvador" +
                    " Dali (1904-1989) and his wife and muse, Gala (1894-1982) were the stuff of legend. Luckily for us, " +
                    "Dali published a cookbook in 1973, Les diners de Gala, which reveals some of the sensual, imaginative," +
                    " and exotic elements that made up their notorious gatherings.This reprint features all 136 recipes over" +
                    " 12 chapters, specially illustrated by Dali, and organized by meal courses, including aphrodisiacs. " +
                    "The illustrations and recipes are accompanied by Dali's extravagant musings on subjects such as dinner" +
                    " conversation: \"The jaw is our best tool to grasp philosophical knowledge\"",
                    Country = "Cologne, Germany",
                    Language = Languages[0],
                    PublicationDate = DateTime.Parse("2016-11-20"),
                    Price = double.Parse("55.5"),
                    Publisher = Publishers[2]
                },

                new BookInfo
                {
                    ISBN = "9780730324218",
                    Title = "The Barefoot Investor : The Only Money Guide You'll Ever Need",
                    Description = "This is the only money guide you'll ever need That's a bold claim, given there are" +
                    " already thousands of finance books on the shelves. So what makes this one different? Well, you won't" +
                    " be overwhelmed with a bunch of 'tips' ? or a strict budget (that you won't follow). You'll get a " +
                    "step-by-step formula: open this account, then do this; call this person, and say this; invest money" +
                    " here, and not there.",
                    Country = "Milton, QLD, Australia",
                    Language = Languages[0],
                    PublicationDate = DateTime.Parse("2016-12-09"),
                    Price = double.Parse("16.32"),
                    Publisher = Publishers[3]
                },

                new BookInfo
                {
                    ISBN = "9781473611528",
                    Title = "Business Adventures : Twelve Classic Tales from the World of Wall Street",
                    Description = "'The best business book I've ever read.' Bill Gates, Wall Street Journal 'The Michael" +
                    " Lewis of his day.' New York Times What do the $350 million Ford Motor Company disaster known as the" +
                    " Edsel, the fast and incredible rise of Xerox, and the unbelievable scandals at General Electric and" +
                    " Texas Gulf Sulphur have in common? ",
                    Country = "London, United Kingdom",
                    Language = Languages[0],
                    PublicationDate = DateTime.Parse("2015-05-14"),
                    Price = double.Parse("11.6"),
                    Publisher = Publishers[4]
                },

                new BookInfo
                {
                    ISBN = "9781439199190",
                    Title = "How to Win Friends and Influence People",
                    Description = "Since its initial publication eighty years ago, How to Win Friends & Influence People " +
                    "has sold over fifteen million copies worldwide. In his book, Carnegie explains that success comes" +
                    " from the ability to communicate effectively with others. He provides relatable analogies and examples," +
                    " and teaches you skills to make people want to be in your company, see things your way, and feel " +
                    "wonderful about it. For more than eighty years his advice has helped thousands of successful people" +
                    " in their business and personal lives.",
                    Country = "New York, United States",
                    Language = Languages[0],
                    PublicationDate = DateTime.Parse("2011-05-17"),
                    Price = double.Parse("6.69"),
                    Publisher = Publishers[5]
                },

                new BookInfo
                {
                    ISBN = "9780205309023",
                    Title = "The Elements of Style",
                    Description = "You know the authors' names. You recognize the title. You've probably used this book yourself. " +
                    "This is The Elements of Style, the classic style manual, now in a fourth edition. A new Foreword by Roger Angell" +
                    " reminds readers that the advice of Strunk & White is as valuable today as when it was first offered.This book's" +
                    " unique tone, wit and charm have conveyed the principles of English style to millions of readers. Use the fourth" +
                    " edition of \"the little book\" to make a big impact with writing.",
                    Country = "New Jersey, United States",
                    Language = Languages[0],
                    PublicationDate = DateTime.Parse("1999-08-02"),
                    Price = double.Parse("7.86"),
                    Publisher = Publishers[6]
                },

                new BookInfo
                {
                    ISBN = "9781137585042",
                    Title = "Cite Them Right : The Essential Referencing Guide",
                    Description = "All assignments need to include correct references: this book shows you how. Cite them right is the" +
                    " renowned guide to referencing and avoiding plagiarism. It is an essential resource for every student and author, " +
                    "providing clear and comprehensive coverage of a crucial part of academic study.",
                    Country = "Basingstoke, United Kingdom",
                    Language = Languages[0],
                    PublicationDate = DateTime.Parse("2016-07-01"),
                    Price = double.Parse("18.45"),
                    Publisher = Publishers[7]
                },

                new BookInfo
                {
                    ISBN = "9781743214404",
                    Title = "Lonely Planet Japanese Phrasebook & Dictionary",
                    Description = "All assignments need to include correct references: this book shows you how. Cite them right is the " +
                    "renowned guide to referencing and avoiding plagiarism. It is an essential resource for every student and author," +
                    " providing clear and comprehensive coverage of a crucial part of academic study.",
                    Country = "Hawthorn, Victoria, Australia",
                    Language = Languages[0],
                    PublicationDate = DateTime.Parse("2015-04-01"),
                    Price = double.Parse("5.47"),
                    Publisher = Publishers[8]
                },

                new BookInfo
                {
                    ISBN = "9781780226583",
                    Title = "I am Malala : The Girl Who Stood Up for Education and Was Shot by the Taliban",
                    Description = "Winner of the 2014 Nobel Peace Prize* When the Taliban took control of the Swat Valley, one girl fought" +
                    " for her right to an education. On Tuesday, 9 October 2012, she almost paid the ultimate price when she was shot in the" +
                    " head at point-blank range. Malala Yousafzai's extraordinary journey has taken her from a remote valley in northern" +
                    " Pakistan to the halls of the United Nations. She has become a global symbol of peaceful protest and is the youngest" +
                    " ever winner of the Nobel Peace Prize. I Am Malala will make you believe in the power of one person's voice to inspire" +
                    " change in the world.",
                    Country = "London, United Kingdom",
                    Language = Languages[0],
                    PublicationDate = DateTime.Parse("2014-10-09"),
                    Price = double.Parse("10.17"),
                    Publisher = Publishers[9]
                },

                new BookInfo
                {
                    ISBN = "9780170364379",
                    Title = "Supporting Education: the Teaching Assistant's Handbook",
                    Description = "This resource provides teaching assistants with an overview of child development and how children learn," +
                    " as well an introduction to the key learning areas for primary school students. Links to a wide range of online resources" +
                    " are included as well as strategies for working with students and teachers.",
                    Country = "South Melbourne, Australia",
                    Language = Languages[0],
                    PublicationDate = DateTime.Parse("2016-06-23"),
                    Price = double.Parse("63.16"),
                    Publisher = Publishers[10]
                },

                new BookInfo
                {
                    ISBN = "9780684838281",
                    Title = "Experience and Education",
                    Description = "Experience and Educationis the best concise statement on education ever published by John Dewey, the man" +
                    " acknowledged to be the pre-eminent educational theorist of the twentieth century. Written more than two decades after" +
                    " Democracy and Education(Dewey's most comprehensive statement of his position in educational philosophy), this book" +
                    " demonstrates how Dewey reformulated his ideas as a result of his intervening experience with the progressive schools and" +
                    " in the light of the criticisms his theories had received. Analysing both \"traditional\" and \"progressive\" education, " +
                    "Dr. Dewey here insists that neither the old nor the new education is adequate and that each is miseducative because neither" +
                    " of them applies the principles of a carefully developed philosophy of experience",
                    Country = "New York, United States",
                    Language = Languages[0],
                    PublicationDate = DateTime.Parse("2008-07-31"),
                    Price = double.Parse("7.34"),
                    Publisher = Publishers[5],
                },

                new BookInfo
                {
                    ISBN = "9780552779777",
                    Title = "The Girl on the Train",
                    Description = "Rachel catches the same commuter train every morning. She knows it will wait at the same signal each time, " +
                    "overlooking a row of back gardens.",
                    Country = "London, United Kingdom",
                    Language = Languages[0],
                    PublicationDate = DateTime.Parse("2016-05-05"),
                    Price = double.Parse("9.54"),
                    Publisher = Publishers[11]
                },

                new BookInfo
                {
                    ISBN = "9781786070159",
                    Title = "The Sellout",
                    Description = "Simon Schama, Financial Times 'The longer you stare at Beatty's pages, the smarter you'll get.' Guardian '" +
                    "The most badass first 100 pages of an American novel I've read.' New York Times A biting satire about a young man's isolated" +
                    " upbringing and the race trial that sends him to the Supreme Court, The Sellout showcases a comic genius at the top of his game.",
                    Country = "London, United Kingdom",
                    Language = Languages[0],
                    PublicationDate = DateTime.Parse("2016-11-04"),
                    Price = double.Parse("12.66"),
                    Publisher = Publishers[12]
                },

                new BookInfo
                {
                    ISBN = "9780747599876",
                    Title = "The Tales of Beedle the Bard",
                    Description = "Rowling since the publication of Harry Potter and the Deathly Hallows. The Tales of Beedle the Bard played a crucial" +
                    " role in assisting Harry, with his friends Ron and Hermione, to finally defeat Lord Voldemort. Fans will be thrilled to have this" +
                    " opportunity to read the tales in full.",
                    Country = "London, United Kingdom",
                    Language = Languages[0],
                    PublicationDate = DateTime.Parse("2008-12-02"),
                    Publisher = Publishers[13]
                }

            };

            foreach (BookInfo book in Books)
            {
                context.BookInfo.Add(book);
            }
            context.SaveChanges();


            var BookAuthorJoiners = new BookAuthorJoiner[]
            {
                new BookAuthorJoiner()
                {
                    BookInfo = Books[0],
                    Author = Authors[0],
                },

                new BookAuthorJoiner()
                {
                    BookInfo = Books[1],
                    Author = Authors[1],
                },

                new BookAuthorJoiner()
                {
                    BookInfo = Books[2],
                    Author = Authors[2],
                },

                new BookAuthorJoiner()
                {
                    BookInfo = Books[3],
                    Author = Authors[3],
                },

                new BookAuthorJoiner()
                {
                    BookInfo = Books[4],
                    Author = Authors[4],
                },

                new BookAuthorJoiner()
                {
                    BookInfo = Books[5],
                    Author = Authors[5],
                },

                new BookAuthorJoiner()
                {
                    BookInfo = Books[6],
                    Author = Authors[6],
                },

                new BookAuthorJoiner()
                {
                    BookInfo = Books[7],
                    Author = Authors[7],
                },

                new BookAuthorJoiner()
                {
                    BookInfo = Books[8],
                    Author = Authors[8],
                },

                new BookAuthorJoiner()
                {
                    BookInfo = Books[9],
                    Author = Authors[9],
                },

                new BookAuthorJoiner()
                {
                    BookInfo = Books[10],
                    Author = Authors[10],
                },

                new BookAuthorJoiner()
                {
                    BookInfo = Books[11],
                    Author = Authors[11],
                },

                new BookAuthorJoiner()
                {
                    BookInfo = Books[12],
                    Author = Authors[12],
                },

                new BookAuthorJoiner()
                {
                    BookInfo = Books[13],
                    Author = Authors[13],
                },

                new BookAuthorJoiner()
                {
                    BookInfo = Books[14],
                    Author = Authors[14],
                },

            };

            foreach (BookAuthorJoiner bookauthorjoiner in BookAuthorJoiners)
            {
                context.BookAuthorJoiner.Add(bookauthorjoiner);
            }
            context.SaveChanges();


            var BookCopiesDetail = new BookCopyDetail[]
            {
                new BookCopyDetail
                {
                    BookInfo = Books[0],
                    Condition = "Brand new",
                    CopyNo = 0
                },

                new BookCopyDetail
                {
                    BookInfo = Books[1],
                    Condition = "Old",
                    CopyNo = 0,
                },

                new BookCopyDetail
                {
                    BookInfo = Books[0],
                    Condition = "Brand new",
                    CopyNo = 1
                },

                new BookCopyDetail
                {
                    BookInfo = Books[2],
                    Condition = "Old",
                    CopyNo = 0
                },

                new BookCopyDetail
                {
                    BookInfo = Books[10],
                    Condition = "Brand new",
                    CopyNo = 0
                },

                new BookCopyDetail
                {
                    BookInfo = Books[14],
                    Condition = "Brand new",
                    CopyNo = 0
                },

                new BookCopyDetail
                {
                    BookInfo = Books[11],
                    Condition = "Brand new",
                    CopyNo = 0
                },

                new BookCopyDetail
                {
                    BookInfo = Books[2],
                    Condition = "Old",
                    CopyNo = 1
                },

                new BookCopyDetail
                {
                    BookInfo = Books[4],
                    Condition = "Brand new",
                    CopyNo = 0,
                },

                new BookCopyDetail
                {
                    BookInfo = Books[9],
                    Condition = "Brand new",
                    CopyNo = 0
                },

                new BookCopyDetail
                {
                    BookInfo = Books[10],
                    Condition = "Old",
                    CopyNo = 1
                },

                new BookCopyDetail
                {
                    BookInfo = Books[12],
                    Condition = "Brand new",
                    CopyNo = 0
                },

                new BookCopyDetail
                {
                    BookInfo = Books[13],
                    Condition = "Brand new",
                    CopyNo = 0
                },

                new BookCopyDetail
                {
                    BookInfo = Books[3],
                    Condition = "Old",
                    CopyNo = 0
                },

                new BookCopyDetail
                {
                    BookInfo = Books[6],
                    Condition = "Old",
                    CopyNo = 0
                },

                new BookCopyDetail
                {
                    BookInfo = Books[8],
                    Condition = "Old",
                    CopyNo = 0
                },

                new BookCopyDetail
                {
                    BookInfo = Books[7],
                    Condition = "Old",
                    CopyNo = 0
                },

                new BookCopyDetail
                {
                    BookInfo = Books[4],
                    Condition = "Old",
                    CopyNo = 1
                },

                new BookCopyDetail
                {
                    BookInfo = Books[5],
                    Condition = "Old",
                    CopyNo = 0
                },

                new BookCopyDetail
                {
                    BookInfo = Books[1],
                    Condition = "Old",
                    CopyNo = 1
                },

                new BookCopyDetail
                {
                    BookInfo = Books[10],
                    Condition = "Old",
                    CopyNo = 2
                },

                new BookCopyDetail
                {
                    BookInfo = Books[14],
                    Condition = "Old",
                    CopyNo = 1
                },

                new BookCopyDetail
                {
                    BookInfo = Books[11],
                    Condition = "Old",
                    CopyNo = 1
                },

            };

            foreach ( BookCopyDetail bookcopydetail in BookCopiesDetail)
            {
                context.BookCopyDetail.Add(bookcopydetail);
            }
            context.SaveChanges();

            var BookCategoriesJoiner = new BookCategoryJoiner[]
            {
                new BookCategoryJoiner
                {
                    BookInfo = Books[0],
                    Category = Categories[0]
                },

                new BookCategoryJoiner
                {
                    BookInfo = Books[1],
                    Category = Categories[0]
                },

                new BookCategoryJoiner
                {
                    BookInfo = Books[2],
                    Category = Categories[0]
                },

                new BookCategoryJoiner
                {
                    BookInfo = Books[3],
                    Category = Categories[6]
                },

                new BookCategoryJoiner
                {
                    BookInfo = Books[4],
                    Category = Categories[6]
                },

                new BookCategoryJoiner
                {
                    BookInfo = Books[5],
                    Category = Categories[6]
                },

                new BookCategoryJoiner
                {
                    BookInfo = Books[6],
                    Category = Categories[8]
                },

                new BookCategoryJoiner
                {
                    BookInfo = Books[7],
                    Category = Categories[8]
                },

                new BookCategoryJoiner
                {
                    BookInfo = Books[8],
                    Category = Categories[8]
                },

                new BookCategoryJoiner
                {
                    BookInfo = Books[9],
                    Category = Categories[1]
                },

                new BookCategoryJoiner
                {
                    BookInfo = Books[10],
                    Category = Categories[1]
                },

                new BookCategoryJoiner
                {
                    BookInfo = Books[11],
                    Category = Categories[1]
                },

                new BookCategoryJoiner
                {
                    BookInfo = Books[12],
                    Category = Categories[21]
                },

                new BookCategoryJoiner
                {
                    BookInfo = Books[13],
                    Category = Categories[21]
                },

                new BookCategoryJoiner
                {
                    BookInfo = Books[14],
                    Category = Categories[21]
                },

            };

            foreach (BookCategoryJoiner bookcategoriesjoiner in BookCategoriesJoiner)
            {
                context.BookCategoryJoiner.Add(bookcategoriesjoiner);
            }
            context.SaveChanges();

            int count = context.BookInfo.Count() - 1;
            int randomnumb = new Random().Next(0, count);

            var Parameters = new Parameter[]
            {
                new Parameter
                {
                    ParameterName = "RadomNumb",
                    Status = true,
                    Type = "int",
                    Value = randomnumb.ToString(),
                },

                new Parameter
                {
                    ParameterName = "DateofCreationNumb",
                    Status = true,
                    Type = "Datetime",
                    Value = DateTime.Now.ToString()
                }
            };

            foreach (Parameter param in Parameters)
            {
                context.Parameter.Add(param);
            }
            context.SaveChanges();
        }
    }
}
