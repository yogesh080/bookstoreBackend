using CommonLayer.CardModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace RepositoryLayer.Service
{
    public class AddCartRL: IAddCartRL
    {
        private readonly IConfiguration configuration;
        private readonly IConfiguration _AppSetting;
        public AddCartRL(IConfiguration configuration, IConfiguration _AppSetting)
        {
            this.configuration = configuration;
            this._AppSetting = _AppSetting;
        }

        public string AddToCart(int UserId, CartPostModel cardModel)
        {
            using SqlConnection connection = new SqlConnection(configuration["ConnectionString:BookStoreDB"]);

            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spAddToCart", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@BookId", cardModel.BookId);
                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.Parameters.AddWithValue("@BookQuantity", cardModel.BookQuantity);

                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return "Succsfull !!!";
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CartRetrieveModel> GetAllCartItems(int UserId)
        {
            using SqlConnection connection = new SqlConnection(configuration["ConnectionString:BookStoreDB"]);

            List<CartRetrieveModel> cartList = new List<CartRetrieveModel>();
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("spRetrieveCart", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", UserId);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        CartRetrieveModel retrieveModel = new CartRetrieveModel();
                        retrieveModel.CartId = reader["CartId"] == DBNull.Value ? default : reader.GetInt32("CartId");
                        retrieveModel.UserId = reader["UserId"] == DBNull.Value ? default : reader.GetInt32("UserId");
                        retrieveModel.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                        retrieveModel.BookName = reader["BookName"] == DBNull.Value ? default : reader.GetString("BookName");
                        retrieveModel.Author = reader["Author"] == DBNull.Value ? default : reader.GetString("Author");
                        retrieveModel.BookQuantity = reader["BookQuantity"] == DBNull.Value ? default : reader.GetInt32("BookQuantity");
                        retrieveModel.Price = reader["Price"] == DBNull.Value ? default : reader.GetDecimal("Price");
                        retrieveModel.DiscountedPrice = (reader["DiscountedPrice"] == DBNull.Value ? default : reader.GetDecimal("DiscountedPrice"));
                        retrieveModel.BookImage = reader["BookImage"] == DBNull.Value ? default : reader.GetString("BookImage");
                        cartList.Add(retrieveModel);
                    }
                    return cartList;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public string DeleteFromCart(int CartId, int UserId)
        {
            using SqlConnection connection = new SqlConnection(configuration["ConnectionString:BookStoreDB"]);

            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spDeleteFromCart", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CartId", CartId);
                    command.Parameters.AddWithValue("@UserId", UserId);

                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return "Succsfully Deleted From Cart";
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string UpdateCart(int UserId, CartUpdateModel cartUpdateModel)
        {
            using SqlConnection connection = new SqlConnection(configuration["ConnectionString:BookStoreDB"]);

            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spUpdateCart", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CartId", cartUpdateModel.CartId);
                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.Parameters.AddWithValue("@BookQuantity", cartUpdateModel.BookQuantity);

                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return "Succsfully Updated The Cart";
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
