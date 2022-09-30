using BusinessLayer.Interface;
using CommonLayer.BookModel;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class BookBL: IBookBL
    {
        private readonly IBookRL bookRL;
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }
        public BookDataModel BookCreate(BookDataModel bookmodel)
        {
            try
            {
                return bookRL.BookCreate(bookmodel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<BookResponseModel> GetAllBooks()
        {
            try
            {
                return bookRL.GetAllBooks();
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
