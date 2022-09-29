using CommonLayer.AdminModel;
using CommonLayer.UserModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Service
{
    public class AdminRL: IAdminRL
    {
        private readonly IConfiguration configuration;
        private readonly IConfiguration _AppSetting;
        public AdminRL(IConfiguration configuration, IConfiguration _AppSetting)
        {
            this.configuration = configuration;
            this._AppSetting = _AppSetting;
        }

        public AdminModel Register(AdminModel adminmodel)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(configuration["ConnectionString:BookStoreDB"]);
                SqlCommand cmd = new SqlCommand("spAdminRegister", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@AdminName", adminmodel.AdminName);
                cmd.Parameters.AddWithValue("@AdminEmail", adminmodel.AdminEmail);
                cmd.Parameters.AddWithValue("@AdminPassword", adminmodel.AdminPassword);

                connection.Open();
                var result = cmd.ExecuteNonQuery();
                connection.Close();

                if (result != 0)
                {
                    return adminmodel;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }



    }
}
