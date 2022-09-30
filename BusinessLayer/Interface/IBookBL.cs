using CommonLayer.BookModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IBookBL
    {
        public BookDataModel BookCreate(BookDataModel bookmodel);
        public List<BookResponseModel> GetAllBooks();
        public BookResponseModel UpdateBook(int BookId, BookResponseModel bookModel);
        public bool DeleteBook(int BookId);
        public BookResponseModel RetrieveBookById(int BookId);





    }
}
