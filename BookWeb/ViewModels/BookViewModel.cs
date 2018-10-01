using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookWeb.ViewModels
{
    public class BookViewModel
    {
        public IEnumerable<BookService.Book> Books { get; set; }
        public BookService.Pager Pager { get; set; }
        public string SearchValue { get; set; }
        public string SearchType { get; set; }
    }
}