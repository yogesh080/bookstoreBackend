using BusinessLayer.Interface;
using CommonLayer.UserModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL:IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public UserDataModel Register(UserDataModel usermodel)
        {
            try
            {
                return userRL.Register(usermodel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Login(UserLoginModel userLogin)
        {
            try
            {
                return userRL.Login(userLogin);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public string ForgetPassword(string Email)
        {
            try
            {
                return userRL.ForgetPassword(Email);

            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
