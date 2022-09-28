using BusinessLayer.Interface;
using CommonLayer.UserModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace bookstoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost("Register")]
        public ActionResult UserRegister(UserDataModel usermodel)
        {
            try
            {
                var user = this.userBL.Register(usermodel);
                if (user != null)
                {
                    return this.Ok(new { success = true, message = "Registration Successfully", data = user });
                }
                return this.BadRequest(new { success = false, message = "Email Already Exits", data = user });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("Login")]
        public IActionResult UserLogin(UserLoginModel userLogin)
        {
            try
            {
                var result = this.userBL.Login(userLogin);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Login Successful", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Login failed " });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("ForgetPassword")]
        public ActionResult ForgetPassword(string Email)

        {
            try
            {
                var result = userBL.ForgetPassword(Email);
                if (result != null)
                {
                    return Ok(new { success = true, message = "REST LINK SEND SUCCESSFULL ", data = result });
                }
                else
                {
                    return BadRequest(new { success = false, message = "REST LINK SEND FAILED" });

                }

            }
            catch (System.Exception)
            {

                throw;

            }
        }
    }
}