using CommonLayer.UserModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using CommonLayer.BookModel;

namespace RepositoryLayer.Service
{
    public class BookRL:IBookRL
    {
        private readonly IConfiguration configuration;
        private readonly IConfiguration _AppSetting;
        public BookRL(IConfiguration configuration, IConfiguration _AppSetting)
        {
            this.configuration = configuration;
            this._AppSetting = _AppSetting;
        }

        public BookDataModel BookCreate(BookDataModel bookmodel)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(configuration["ConnectionString:BookStoreDB"]);
                SqlCommand cmd = new SqlCommand("spBookCreate", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@BookName", bookmodel.BookName);
                cmd.Parameters.AddWithValue("@Author", bookmodel.Author);
                cmd.Parameters.AddWithValue("@Description", bookmodel.Description);
                cmd.Parameters.AddWithValue("@Quantity", bookmodel.Quantity);
                cmd.Parameters.AddWithValue("@Price", bookmodel.Price);
                cmd.Parameters.AddWithValue("@DiscountedPrice", bookmodel.DiscountedPrice);
                cmd.Parameters.AddWithValue("@Rating", bookmodel.Rating);
                cmd.Parameters.AddWithValue("@RatingCount", bookmodel.RatingCount);
                cmd.Parameters.AddWithValue("@BookImage", bookmodel.BookImage);

                connection.Open();
                var result = cmd.ExecuteNonQuery();
                connection.Close();

                if (result != 0)
                {
                    return bookmodel;
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
