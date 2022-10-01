using BusinessLayer.Interface;
using CommonLayer.WishListItem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

namespace bookstoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishListBL wishListBL;
        public WishListController(IWishListBL wishListBL)
        {
            this.wishListBL = wishListBL;
        }
        [Authorize]
        [HttpPost("AddToWishList")]
        public ActionResult AddToWishList(WishListPostModel postModel)
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = wishListBL.AddToWishList(UserID, postModel);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Added To WishList Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Could Not Be Added To WishList" });
                }

            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpDelete("DeleteFromWishList")]
        public ActionResult DeleteFromWishList(int WishListId)
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = wishListBL.DeleteFromWishList(UserID, WishListId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Deleted From WishList Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Could Not Be Deleted From WishList" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("GetWishListedItems")]
        public ActionResult GetWishListedItem()
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = wishListBL.GetWishListByUserId(UserID);
                if (result != null)
                {
                    return Ok(new { success = true, message = "WishList Fetched Successfully", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "WishList Could Not Be Fetched" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
