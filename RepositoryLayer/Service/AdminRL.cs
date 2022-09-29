using CommonLayer.AdminModel;
using CommonLayer.UserModel;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

        public string Login(AdminLoginModel adminLogin)
        {
            SqlConnection sqlConnection = new SqlConnection(configuration["ConnectionString:BookStoreDB"]);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("spAdminLogIn", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AdminEmail", adminLogin.AdminEmail);
                    cmd.Parameters.AddWithValue("@AdminPassword", adminLogin.AdminPassword);
                    cmd.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();
                    GetAllUserModel response = new GetAllUserModel();
                    if (reader.Read())
                    {
                        response.UserId = reader["AdminId"] == DBNull.Value ? default : reader.GetInt32("AdminId");
                        response.Email = reader["AdminEmail"] == DBNull.Value ? default : reader.GetString("AdminEmail");
                        response.Password = reader["AdminPassword"] == DBNull.Value ? default : reader.GetString("AdminPassword");
                    }
                    return GenerateSecurityToken(response.Email, response.UserId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public string GenerateSecurityToken(string email, long UserId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this._AppSetting[("JWT:key")]));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("UserId", UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }



    }
}
