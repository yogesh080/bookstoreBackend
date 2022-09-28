using CommonLayer.UserModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public UserDataModel Register(UserDataModel usermodel);
        public string Login(UserLoginModel userLogin);
        public string ForgetPassword(string Email);


    }
}
