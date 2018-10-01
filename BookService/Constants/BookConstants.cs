namespace BookService.Constants
{
    public static class BookConstants
    {
        public const string BOOK_ATTRIBUTE = "book";                 //XML attribute name
        public const string BOOK_ID= "id";                           //XML category "ID"
        public const string BOOK_AUTHOR = "author";                  //XML category "Author"
        public const string BOOK_TITLE = "title";                    //XML category "Title"
        public const string BOOK_PRICE = "price";                    //XML category "Price"
        public const string BOOK_GENRE = "genre";                    //XML category "Genre"
        public const string BOOK_PUBLISH_DATE = "publish_date";      //XML category "publish_date"
        public const string BOOK_DESCRIP = "description";            //XML category "Description"

        public const string BOOK_DESC = "title_desc";                //Title descending
        public const string AUTHOR_DESC = "author_desc";             //Author descending 
        public const string CULTURE_INFO = "en-US";                  //XML culture (decimal convertion)

        public const int PAGE_SIZE = 10;                             //Max amount of items in one page
        public const int PAGE_OFFSET_LEFT = 5;                       //Offset from current page, from the left
        public const int PAGE_OFFSET_RIGHT = 4;                      //Offset from current page, from the right
    }
}