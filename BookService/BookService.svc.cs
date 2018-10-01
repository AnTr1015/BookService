using System.Collections.Generic;
using System.Linq;
using BookService.Constants;
using BookService.Mapper;

namespace BookService
{
    public class Service1 : IBookService
    {
        private IBookMapper _mapper = new BookMapper();

        public List<Book> GetAllBooks(string sortBy)
        {
            return SortBook(_mapper.GetBooksFromDBResponse(), sortBy);
        }

        public List<Book> GetBookByCategory(string searchValue, string category, string sortBy)
        {
            return SortBook(_mapper.GetBooksFromDBResponse(searchValue, category), sortBy);
        }

        public Book GetBookByID(string bookID)
        {
            return _mapper.GetBookFromDBResponse(bookID);
        }

        public void EditBook(Book book)
        {
            _mapper.EditBookInDBResponse(book);
        }

        public void NewBook(Book book)
        {
            _mapper.NewBookToDBResponse(book);
        }

        public void DeleteBook(Book book)
        {
            _mapper.DeleteBookToDBResponse(book);
        }

        private List<Book> SortBook(List<Book> books, string sortBy)
        {
            switch (sortBy)
            {
                case BookConstants.AUTHOR_DESC:
                    books = books.OrderByDescending(b => b.Author).ToList();
                    break;
                case BookConstants.BOOK_TITLE:
                    books = books.OrderBy(b => b.Title).ToList();
                    break;
                case BookConstants.BOOK_DESC:
                    books = books.OrderByDescending(b => b.Title).ToList();
                    break;
                default:
                    books = books.OrderBy(b => b.Author).ToList();
                    break;
            }

            return books;
        }

        public Pager Pagination(int totalItems, int? page)
        {
            return _mapper.PaginationResponse(totalItems, page);
        }
    }
}
