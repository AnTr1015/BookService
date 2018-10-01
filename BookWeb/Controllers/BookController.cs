using BookWeb.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace BookWeb.Controllers
{
    public class BookController : Controller
    {
        BookService.BookServiceClient bookServ = new BookService.BookServiceClient();

        public ActionResult Index(string searchValue, string searchType, string sortBy, int? currentPage)
        {
            ViewBag.AuthorSortParm = String.IsNullOrEmpty(sortBy) ? "author_desc" : "";
            ViewBag.TitleSortParm = sortBy == "title" ? "title_desc" : "title";

            var books = bookServ.GetBookByCategory(searchValue, searchType, sortBy);
            var pager = bookServ.Pagination(books.Count(), currentPage);

            var viewModel = new BookViewModel
            {
                Books = books.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize), //Depends on page, skip elements and take elements of 'PageSize'
                Pager = pager,
                SearchValue = searchValue,
                SearchType = searchType
            };

            return View(viewModel);
        }
        public ActionResult Detail(string id)
        {
            return View(bookServ.GetBookByID(id));
        }
        public ActionResult EditBook(string id)
        {
            return View("BookForm", bookServ.GetBookByID(id));
        }
        public ActionResult NewBook()
        {
            return View("BookForm", new BookService.Book());
        }

        [HttpPost]
        public ActionResult SaveBook(BookService.Book book)
        {
            validateEditedBook(book);                                                            //Validate edited book

            if (book != null && ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(book.ID))
                    bookServ.NewBook(book);
                else
                    bookServ.EditBook(book);

                return RedirectToAction("Index", "Book");
            }

            return View("BookForm", book);
        }

        [HttpPost]
        public ActionResult DeleteBook(BookService.Book book)
        {
            bookServ.DeleteBook(book);
            return RedirectToAction("Index", "Book");
        }

        public void validateEditedBook(BookService.Book book)
        {
            if (string.IsNullOrEmpty(book.Author))
                ModelState.AddModelError("Author", "Please enter the name of the Author");
            if (string.IsNullOrEmpty(book.Title))
                ModelState.AddModelError("Title", "Please enter the name of the book");
            if (book.Genre == null)
                ModelState.AddModelError("Genre", "Please enter the genre of the book");
            if (book.Price == 0)
                ModelState.AddModelError("Price", "Please enter the price of the book");
            if (string.IsNullOrEmpty(book.Description))
                book.Description = "";

            ModelState.Remove("PublishDate");
            ModelState.Remove("Description");
        }
    }
}