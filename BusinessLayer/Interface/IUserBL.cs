using CommonLayer.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public UserDataModel Register(UserDataModel usermodel);
        public string Login(UserLoginModel userLogin);
        public string ForgetPassword(string Email);



    }
}
