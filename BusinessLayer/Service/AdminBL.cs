using BusinessLayer.Interface;
using CommonLayer.AdminModel;
using CommonLayer.UserModel;
using RepositoryLayer.Interface;
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
    }
}
