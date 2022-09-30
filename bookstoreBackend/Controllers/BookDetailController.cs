using BusinessLayer.Interface;
using CommonLayer.BookModel;
using CommonLayer.UserModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

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
    }
}
