using BusinessLayer.Interface;
using CommonLayer.OrderModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace bookstoreBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBL orderBL;
        public OrderController(IOrderBL orderBL)
        {
            this.orderBL = orderBL;
        }

        [HttpPost("AddOrder")]
        public IActionResult AddOrder(OrderDataModel postModel)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = int.Parse(userId);

                var result = this.orderBL.AddOrder(postModel);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, Message = " Order placed failed" });
                }
                return this.Ok(new { success = true, Message = "Order placed Sucessfully..." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetAllOrder")]
        public IActionResult GetAllOrder()
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = int.Parse(userId);

                var result = this.orderBL.GetAllOrder(UserId);

                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Order Get Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Order Not Found  " });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
