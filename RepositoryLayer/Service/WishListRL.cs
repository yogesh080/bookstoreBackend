using CommonLayer.WishListItem;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Service
{
    public class WishListRL:IWishListRL
    {
        private readonly IConfiguration configuration;
        private readonly IConfiguration _AppSetting;
        public WishListRL(IConfiguration configuration, IConfiguration _AppSetting)
        {
            this.configuration = configuration;
            this._AppSetting = _AppSetting;
        }
        public string AddToWishList(int UserId, WishListPostModel postModel)
        {
            using SqlConnection connection = new SqlConnection(configuration["ConnectionString:BookStoreDB"]);

            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spAddToWishlList", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.Parameters.AddWithValue("@BookId", postModel.BookId);
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return "Added To WishList";
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

        public string DeleteFromWishList(int UserId, int WishListId)
        {
            using SqlConnection connection = new SqlConnection(configuration["ConnectionString:BookStoreDB"]);

            try
            {
                using (connection)
                {
                    SqlCommand command = new SqlCommand("spDeleteFromWishList", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.Parameters.AddWithValue("@WishListId", WishListId);
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return "Deleted From WishList";
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
        public List<WishListResponseModel> GetWishListByUserId(int UserId)
        {
            using SqlConnection connection = new SqlConnection(configuration["ConnectionString:BookStoreDB"]);

            List<WishListResponseModel> wishListedItems = new List<WishListResponseModel>();
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("spGetWishList", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserId", UserId);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        WishListResponseModel wishListModel = new WishListResponseModel();
                        wishListModel.WishListId = reader["WishListId"] == DBNull.Value ? default : reader.GetInt32("WishListId");
                        wishListModel.UserId = reader["UserId"] == DBNull.Value ? default : reader.GetInt32("UserId");
                        wishListModel.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                        wishListModel.BookName = reader["BookName"] == DBNull.Value ? default : reader.GetString("BookName");
                        wishListModel.Author = reader["Author"] == DBNull.Value ? default : reader.GetString("Author");
                        wishListModel.Price = (reader["Price"] == DBNull.Value ? default : reader.GetDecimal("Price"));
                        wishListModel.DiscountedPrice = (reader["DiscountedPrice"] == DBNull.Value ? default : reader.GetDecimal("DiscountedPrice"));
                        wishListModel.BookImage = reader["BookImage"] == DBNull.Value ? default : reader.GetString("BookImage");
                        wishListedItems.Add(wishListModel);
                    }
                    return wishListedItems;
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

    }
}
