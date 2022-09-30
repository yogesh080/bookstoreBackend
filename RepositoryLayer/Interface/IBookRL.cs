using CommonLayer.BookModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IBookRL
    {
        public BookDataModel BookCreate(BookDataModel bookmodel);
        public List<BookResponseModel> GetAllBooks();
        public BookResponseModel UpdateBook(int BookId, BookResponseModel bookModel);


    }
}
