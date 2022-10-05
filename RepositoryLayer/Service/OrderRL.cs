using CommonLayer.OrderModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace RepositoryLayer.Service
{
    public class OrderRL : IOrderRL
    {
        private readonly IConfiguration configuration;
        private readonly IConfiguration _AppSetting;
        public OrderRL(IConfiguration configuration, IConfiguration _AppSetting)
        {
            this.configuration = configuration;
            this._AppSetting = _AppSetting;
        }

        public bool AddOrder( OrderDataModel postModel)
        {
            using SqlConnection connection = new SqlConnection(configuration["ConnectionString:BookStoreDB"]);
            try
            {
                using (connection)
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("spAddOrder", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@UserId", postModel.UserId);
                    cmd.Parameters.AddWithValue("@BookId", postModel.BookId);
                    cmd.Parameters.AddWithValue("@BookQuantity", postModel.Quantity);
                    cmd.Parameters.AddWithValue("@AddressId", postModel.AddressId);
                    int result = cmd.ExecuteNonQuery();

                    if (result != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }



        }

        public List<GetOrderModel> GetAllOrder(int userId)
        {
            SqlConnection connection = new SqlConnection(configuration["ConnectionString:BookStoreDB"]);

            List<GetOrderModel> order = new List<GetOrderModel>();
            try
            {
                SqlCommand cmd = new SqlCommand("spGetOrders", connection);
                cmd.CommandType= CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", userId);

                GetOrderModel orderModel = new GetOrderModel();
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    orderModel.OrdersId = Convert.ToInt32(reader["OrderId"]);
                    orderModel.UserId = Convert.ToInt32(reader["UserId"]);
                    orderModel.BookId = Convert.ToInt32(reader["BookId"]);
                    orderModel.AddressId = Convert.ToInt32(reader["AddressId"]);
                    orderModel.TotalPrice = Convert.ToInt32(reader["TotalPrice"]);
                    orderModel.Quantity = Convert.ToInt32(reader["BookQuantity"]);
                    orderModel.OrderDate = Convert.ToDateTime(reader["OrderDate"]);
                    orderModel.BookName = reader["BookName"].ToString();
                    orderModel.Author = reader["Author"].ToString();
                    orderModel.BookImage = reader["BookImage"].ToString();

                    order.Add(orderModel);
                }
                connection.Close();

                if (order.Count > 0)
                {
                    return order;
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

