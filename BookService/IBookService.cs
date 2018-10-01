using System.Collections.Generic;
using System.ServiceModel;

namespace BookService
{
    [ServiceContract]
    public interface IBookService
    {
        /// <summary>
        /// Get all the books from XML file and return a list of books. 
        /// </summary>
        /// <param name="sortBy">string for checking sort order</param>
        /// <returns></returns>
        [OperationContract]
        List<Book> GetAllBooks(string sortBy);

        /// <summary>
        /// Get all the books depending on which category is chosen.
        /// </summary>
        /// <param name="searchValue">Search value that is recieved from the search box in client</param>
        /// <param name="category">A string value used to check which category was chosen</param>
        /// <returns></returns>
        [OperationContract]
        List<Book> GetBookByCategory(string searchValue, string category, string sortBy); 

        /// <summary>
        /// Get a single book by ID and returns it.
        /// </summary>
        /// <param name="ID">Client book ID</param>
        /// <returns></returns>
        [OperationContract]
        Book GetBookByID(string ID); 

        /// <summary>
        /// Get the edited book from controller and update the xml file.
        /// </summary>
        /// <param name="book">Edited book</param>
        [OperationContract]
        void EditBook(Book book);

        /// <summary>
        /// Create a new book and put it in the database.
        /// </summary>
        /// <param name="book">New book</param>
        [OperationContract]
        void NewBook(Book book);

        /// <summary>
        /// Delete book from xml file
        /// </summary>
        /// <param name="book">Book to delete</param>
        [OperationContract]
        void DeleteBook(Book book);

        /// <summary>
        /// Call for pagination in the mapper class to calculate paging
        /// </summary>
        /// <param name="totalItems">Total items that is going to be displayed</param>
        /// <param name="page">Current selected page</param>
        /// <returns></returns>
        [OperationContract]
        Pager Pagination(int totalItems, int? page);
    }
}
