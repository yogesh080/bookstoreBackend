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
    }
}
