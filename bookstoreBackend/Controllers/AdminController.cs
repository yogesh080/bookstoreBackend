using BusinessLayer.Interface;
using CommonLayer.AdminModel;
using CommonLayer.UserModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace bookstoreBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBL adminBL;
        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }

        [HttpPost("Register")]
        public ActionResult UserRegister(AdminModel adminmodel)
        {
            try
            {
                var user = this.adminBL.Register(adminmodel);
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
    }
}
