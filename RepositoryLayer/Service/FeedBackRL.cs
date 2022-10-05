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
                    
                    SqlCommand cmd = new SqlCommand("spAddFeedback", connection)
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
            SqlConnection connection = new SqlConnection(configuration["ConnectionString:BookStoreDB"]);

            List<GetFeedbackModel> list = new List<GetFeedbackModel>();
            try
            {
                
                    
                    SqlCommand cmd = new SqlCommand("spGetFeedbackks", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@BookId", BookId);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                    GetFeedbackModel model = new GetFeedbackModel();
                    model.FullName = Convert.ToString(reader["FullName"]);
                    model.Comment = Convert.ToString(reader["Comment"]);
                    model.BookId = Convert.ToInt32(reader["BookId"]);
                    model.TotalRating = Convert.ToInt32(reader["Rating"]);
                    model.FeedbackId = Convert.ToInt32(reader["FeedbackId"]);
                    }
                    return list;
                
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
