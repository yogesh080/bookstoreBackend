using CommonLayer.FeedBackModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace RepositoryLayer.Service
{
    public class FeedBackRL : IFeedBackRL
    {
        private readonly IConfiguration configuration;
        private readonly IConfiguration _AppSetting;
        public FeedBackRL(IConfiguration configuration, IConfiguration _AppSetting)
        {
            this.configuration = configuration;
            this._AppSetting = _AppSetting;
        }

        public bool AddFeedback(int UserId, FeedbackDataModel addModel)
        {
            
            using SqlConnection connection = new SqlConnection(configuration["ConnectionString:BookStoreDB"]);
            try
            {

                using (connection)
                {
                    
                    SqlCommand cmd = new SqlCommand("spAddFeedbackpro", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@BookId", addModel.BookId);
                    cmd.Parameters.AddWithValue("@TotalRating", addModel.TotalRating);
                    cmd.Parameters.AddWithValue("@Comment", addModel.Comment);
                    connection.Open();
                    int result = cmd.ExecuteNonQuery();
                    connection.Close();

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
        }

        public List<GetFeedbackModel> GetAllFeedbacksByBookId(int BookId)
        {
            using SqlConnection connection = new SqlConnection(configuration["ConnectionString:BookStoreDB"]);

            List<GetFeedbackModel> list = new List<GetFeedbackModel>();
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("spGetFeedback", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@BookId", BookId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        GetFeedbackModel Feedbackdetails = new GetFeedbackModel
                        {
                            FeedbackId = reader["FeedbackId"] == DBNull.Value ? default : reader.GetInt32("FeedbackId"),
                            UserId = reader["UserId"] == DBNull.Value ? default : reader.GetInt32("UserId"),
                            BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId"),
                            TotalRating = reader["TotalRating"] == DBNull.Value ? default : reader.GetDecimal("TotalRating"),
                            Comment = reader["Comment"] == DBNull.Value ? default : reader.GetString("Comment"),
                            FullName = reader["FullName"] == DBNull.Value ? default : reader.GetString("FullName")
                        };
                        list.Add(Feedbackdetails);
                    }
                    return list;
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

        public bool DeleteFeedbackById(int FeedbackId)
        {
            using SqlConnection connection = new SqlConnection(configuration["ConnectionString:BookStoreDB"]);

            
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SPDeleteFeedbackById", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FeedbackId ", FeedbackId);

                    var result = cmd.ExecuteNonQuery();
                    if (result == 0)
                    {
                        return false;
                    }
                    return true;
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
    }
}
