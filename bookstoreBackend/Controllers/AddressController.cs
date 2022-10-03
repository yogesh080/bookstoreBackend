using BusinessLayer.Interface;
using CommonLayer.AddressModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using RepositoryLayer.Service;

namespace bookstoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressBL addressBL;

        public AddressController(IAddressBL addressBL)
        {
            this.addressBL=addressBL;
        }
        [Authorize]
        [HttpPost("AddAddress")]
        public ActionResult AddAddress(AddressDataModel addAdress)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                //int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);

                var UserID = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int userId= Convert.ToInt32(UserID);

                var result = addressBL.AddAddress(userId, addAdress);
                if (result == true)
                {
                    return this.Ok(new { success = true, Message = $"Address For UserId : {UserID} Added Sucessfully..." });
                }
                else
                {

                    return this.BadRequest(new { success = false, Message = $"Someting Went Wrong While adding Address for UserId : {UserID} " });
                }

            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetAllAddress(int UserId)
        {
            try
            {
                int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = addressBL.GetAllAddress(UserId);

                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Address Details Fetched Successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Address Details Could Not Be Fetched" });
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        [Authorize]
        [HttpDelete]
        public ActionResult DeleteByAddressId(int AddressId)
        {
            int UserID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId" ).Value);
            var result = addressBL.DeleteByAddressId(AddressId, UserID);
            if (result == true)
            {
                return this.Ok(new { success = true, Message = $"AddressId : {AddressId} Deleted Sucessfully..." });
            }
            return this.BadRequest(new { success = false, message = "Delete fails" });

        }


    }
}
