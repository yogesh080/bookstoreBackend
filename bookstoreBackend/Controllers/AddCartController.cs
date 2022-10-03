using CommonLayer.CardModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using BusinessLayer.Interface;

namespace bookstoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddCartController : ControllerBase
    {
        private readonly IAddCartBL addcartBL;
        public AddCartController(IAddCartBL addcartBL)
        {
            this.addcartBL = addcartBL;
        }

        [Authorize]
        [HttpPost("AddToCart")]
        public ActionResult AddToCart(CartPostModel cartModel)
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = addcartBL.AddToCart(UserID, cartModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Book Added To Cart Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Book Not Added To Cart" });

                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpGet("RetrieveCartItems")]
        public ActionResult GetAllCartItems()
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = addcartBL.GetAllCartItems(UserID);
                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Cart Items Fetched Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Cart Items Could Not Be Fetched" });

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpPost("DeleteFromCart")]
        public ActionResult DeleteFromCart(int CartId)
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = addcartBL.DeleteFromCart(CartId, UserID);
                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Book Deleted From Cart Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Book Could Not Deleted From Cart" });

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPost("UpdateCart")]
        public ActionResult UpdateCart(CartUpdateModel cartUpdateModel)
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = addcartBL.UpdateCart(UserID, cartUpdateModel);
                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Cart Updated Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Cart Could Not Be Updated" });

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
