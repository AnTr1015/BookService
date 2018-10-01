using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Xml.Linq;
using BookService.Constants;

namespace BookService.Mapper
{
    public class BookMapper : IBookMapper
    {
        public List<Book> GetBooksFromDBResponse(string searchValue, string category)
        {
            if (searchValue != null && category != null)
            {
                return (from book in GetDb().Descendants(BookConstants.BOOK_ATTRIBUTE)
                        where (book.Element(category).Value.IndexOf(searchValue, StringComparison.OrdinalIgnoreCase)) >= 0 
                        select new Book()
                        {
                            ID = book.Attribute(BookConstants.BOOK_ID).Value,
                            Author = book.Element(BookConstants.BOOK_AUTHOR).Value,
                            Title = book.Element(BookConstants.BOOK_TITLE).Value,
                            Genre = book.Element(BookConstants.BOOK_GENRE).Value,
                            Price = Convert.ToDecimal(book.Element(BookConstants.BOOK_PRICE).Value, new CultureInfo(BookConstants.CULTURE_INFO)),
                            PublishDate = Convert.ToDateTime(book.Element(BookConstants.BOOK_PUBLISH_DATE).Value),
                            Description = book.Element(BookConstants.BOOK_DESCRIP).Value
                        }).ToList();
            }

            else
            {
                return GetAllBooksResponse();
            }
        }

        public List<Book> GetAllBooksResponse()
        {
            return (from book in GetDb().Descendants(BookConstants.BOOK_ATTRIBUTE)
                    select new Book()
                    {
                        ID = book.Attribute(BookConstants.BOOK_ID).Value,
                        Author = book.Element(BookConstants.BOOK_AUTHOR).Value,
                        Title = book.Element(BookConstants.BOOK_TITLE).Value,
                        Genre = book.Element(BookConstants.BOOK_GENRE).Value,
                        Price = Convert.ToDecimal(book.Element(BookConstants.BOOK_PRICE).Value, new CultureInfo(BookConstants.CULTURE_INFO)),
                        PublishDate = Convert.ToDateTime(book.Element(BookConstants.BOOK_PUBLISH_DATE).Value),
                        Description = book.Element(BookConstants.BOOK_DESCRIP).Value
                    }).ToList();
        }

        public Book GetBookFromDBResponse(string bookID)
        {
            return (from book in GetAllBooksResponse()
                    where book.ID.Equals(bookID)
                    select book).SingleOrDefault();
        }

        public void EditBookInDBResponse(Book editedBook)
        {
            XDocument db = GetDb();
            
            var bookElement = (from bookInDB in db.Descendants(BookConstants.BOOK_ATTRIBUTE)
                               where bookInDB.Attribute(BookConstants.BOOK_ID).Value.Equals(editedBook.ID)
                               select bookInDB).SingleOrDefault();

            bookElement.SetElementValue(BookConstants.BOOK_AUTHOR, editedBook.Author);
            bookElement.SetElementValue(BookConstants.BOOK_TITLE, editedBook.Title);
            bookElement.SetElementValue(BookConstants.BOOK_PRICE, editedBook.Price);
            bookElement.SetElementValue(BookConstants.BOOK_PUBLISH_DATE, editedBook.PublishDate.ToShortDateString());
            bookElement.SetElementValue(BookConstants.BOOK_GENRE, editedBook.Genre);
            bookElement.SetElementValue(BookConstants.BOOK_DESCRIP, editedBook.Description);

            db.Save(Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", "Books.xml"));
        }

        public void NewBookToDBResponse(Book book)
        {
            XDocument db = GetDb();
            bool checkIDExist;
            int newBookID = 1;

            //Check if id '1' exist in the xml file.
            checkIDExist = (from bookInDB in db.Descendants(BookConstants.BOOK_ATTRIBUTE)
                            where bookInDB.Attribute(BookConstants.BOOK_ID).Value.Equals(newBookID.ToString())
                            select book).Any();

            //Otherwise, set it to the max value + 1.
            if (checkIDExist)
            {
                newBookID = ((from bookInDB in db.Descendants(BookConstants.BOOK_ATTRIBUTE)
                              select (int?)bookInDB.Attribute(BookConstants.BOOK_ID)).Max() ?? 0) + 1;
            }

            db.Root.Add(new XElement(BookConstants.BOOK_ATTRIBUTE, new XAttribute(BookConstants.BOOK_ID, (newBookID).ToString()),
                              new XElement(BookConstants.BOOK_AUTHOR, book.Author),
                              new XElement(BookConstants.BOOK_TITLE, book.Title),
                              new XElement(BookConstants.BOOK_PRICE, book.Price),
                              new XElement(BookConstants.BOOK_PUBLISH_DATE, book.PublishDate),
                              new XElement(BookConstants.BOOK_GENRE, book.Genre),
                              new XElement(BookConstants.BOOK_DESCRIP, book.Description)));


            db.Save(Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", "Books.xml"));
        }

        public void DeleteBookToDBResponse(Book book)
        {
            XDocument db = GetDb();

            db.Root.Elements()
                .Where(x => x.Attribute(BookConstants.BOOK_ID).Value.Equals(book.ID))
                .Remove();

            db.Save(Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", "Books.xml"));
        }

        private XDocument GetDb()                                                                                           //Load the xml file
        {
            return (XDocument.Load(Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data", "Books.xml")));
        }

        public Pager PaginationResponse(int totalItems, int? page, int pageSize = BookConstants.PAGE_SIZE)
        {
            Pager pager = new Pager();

            //Calculate total, start and end page.
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);                                    //Calculate total pages, by dividing items with maximum capacity for a page.
            int currentPage = page != null ? (int)page : 1;                                                                 //Set the current page, set to 1 if it's null.
            int startPage = currentPage - BookConstants.PAGE_OFFSET_LEFT;                                                   //offset from the selected page, from the left.
            int endPage = currentPage + BookConstants.PAGE_OFFSET_RIGHT;                                                    //offset from the selected page, from the right.

            if (startPage <= 0)                                                                                             //Calculate the last page that is going to be displayed in the pagination.
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }

            if (endPage > totalPages)                                                                                       //Check if endPage is bigger than totalPages, if it's true set the endpage to totalPage (total pages of all items)
            {                                                                                                               //and startPage to the page 10 away from totalPage.
                endPage = totalPages;

                if (endPage > 10)
                {
                    startPage = (endPage - 9);
                }
            }

            pager.TotalItems = totalItems;
            pager.CurrentPage = currentPage;
            pager.StartPage = startPage;
            pager.EndPage = endPage;
            pager.TotalPages = totalPages;
            pager.PageSize = pageSize;

            return pager;
        }
    }
}