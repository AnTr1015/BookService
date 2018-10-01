using System.Collections.Generic;
using BookService.Constants;

namespace BookService.Mapper
{
    public interface IBookMapper
    {
        /// <summary>
        /// Getting books from database using query
        /// </summary>
        /// <param name="searchValue">Search value from client</param>
        /// <param name="category">Category value from client</param>
        /// <returns></returns>
        List<Book> GetBooksFromDBResponse(string searchValue = null, string category = null);

        /// <summary>
        /// Get all the books from xml file
        /// </summary>
        /// <returns></returns>
        List<Book> GetAllBooksResponse();

        /// <summary>
        /// Get a single book by ID
        /// </summary>
        /// <param name="bookID">ID from client</param>
        /// <returns></returns>
        Book GetBookFromDBResponse(string bookID);

        /// <summary>
        /// Recieve a book from client and manage the changes and save it to the XML file
        /// </summary>
        /// <param name="book"></param>
        void EditBookInDBResponse(Book book);

        /// <summary>
        /// Create a new book and put it in the database.
        /// </summary>
        /// <param name="book">New book</param>
        void NewBookToDBResponse(Book book);

        void DeleteBookToDBResponse(Book book);

        /// <summary>
        /// Calculate the pagination of the site, depending on total item in the DB
        /// </summary>
        /// <param name="totalItems">Item that is going to be displayed</param>
        /// <param name="page">Current selected page</param>
        /// <param name="pageSize">The amount of item that is going to be displayed on each page</param>
        /// <returns></returns>
        Pager PaginationResponse(int totalItems, int? page, int pageSize = BookConstants.PAGE_SIZE);
    }
}