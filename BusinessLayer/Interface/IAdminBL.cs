using CommonLayer.AdminModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IAdminBL
    {
        public AdminModel Register(AdminModel adminmodel);
        public string Login(AdminLoginModel adminLogin);


    }
}
