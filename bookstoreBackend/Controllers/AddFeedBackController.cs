using BusinessLayer.Interface;
using CommonLayer.FeedBackModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System;
using System.Linq;

namespace bookstoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddFeedBackController : ControllerBase
    {
        private readonly IFeedBackBL feedBackBL;
        public AddFeedBackController(IFeedBackBL feedBackBL)
        {
            this.feedBackBL = feedBackBL;
        }

        [Authorize]
        [HttpPost("AddFeedback")]
        public IActionResult AddFeedback(FeedbackDataModel addModel)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                var result = this.feedBackBL.AddFeedback(UserId, addModel);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, Message = $"You have already added Feedback for BookId : {addModel.BookId}!!" });
                }
                return this.Ok(new { success = true, Message = $"Feedback added for BookId : {addModel.BookId} Added Sucessfully..." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
