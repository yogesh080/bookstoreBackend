using BusinessLayer.Interface;
using CommonLayer.AdminModel;
using CommonLayer.UserModel;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class AdminBL:IAdminBL
    {
        private readonly IAdminRL adminRL;
        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }
        public AdminModel Register(AdminModel adminmodel)
        {
            try
            {
                return adminRL.Register(adminmodel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Login(AdminLoginModel adminLogin)
        {
            try
            {
                return adminRL.Login(adminLogin);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
