using BusinessLayer.Interface;
using CommonLayer.BookModel;
using CommonLayer.UserModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace bookstoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookDetailController : ControllerBase
    {
        private readonly IBookBL bookBL;
        public BookDetailController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }

        [Authorize]
        [HttpPost("AddBook")]
        public ActionResult AddBook(BookDataModel bookmodel)
        {
            try
            {
                var book = this.bookBL.BookCreate(bookmodel);
                if (book != null)
                {
                    return this.Ok(new { success = true, message = "Book added Successfully", data = book });
                }
                return this.BadRequest(new { success = false, message = "Book added fails", data = book });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet("GetAllBooks")]
        public ActionResult GetAllBooks()
        {
            try
            {
                var result = bookBL.GetAllBooks();
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Books Details Fetched Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Books Details Could Not Be Fetched" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }


        [Authorize]
        [HttpPost("UpdateBooks")]
        public ActionResult UpdateBook(int BookId, BookResponseModel bookModel)
        {
            try
            {
                var result = bookBL.UpdateBook(BookId, bookModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Books Updated Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Books Could Not Be Updated" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpDelete("DeleteBook")]
        public ActionResult DeleteBook(int BookId)
        {
            try
            {
                var result = bookBL.DeleteBook(BookId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Books Deleted Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Books Could Not Be Deleted" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpGet("GetBookById")]
        public ActionResult RetriveByBookId(int BookId)
        {
            try
            {
                var result = bookBL.RetrieveBookById(BookId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Book Details Fetched Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Book Details Could Not Be Fetched" });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
