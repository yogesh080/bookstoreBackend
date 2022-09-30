﻿using CommonLayer.UserModel;
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
    public class BookRL : IBookRL
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

        public List<BookResponseModel> GetAllBooks()
        {
            List<BookResponseModel> listOfUsers = new List<BookResponseModel>();
            using SqlConnection connection = new SqlConnection(configuration["ConnectionString:BookStoreDB"]);

            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("SPGetAllBook", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        BookResponseModel book = new BookResponseModel();
                        book.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                        book.BookName = reader["BookName"] == DBNull.Value ? default : reader.GetString("BookName");
                        book.Author = reader["Author"] == DBNull.Value ? default : reader.GetString("Author");
                        book.Description = reader["Description"] == DBNull.Value ? default : reader.GetString("Description");
                        book.Quantity = reader["Quantity"] == DBNull.Value ? default : reader.GetInt32("Quantity");
                        book.Price = ((int)(reader["Price"] == DBNull.Value ? default : reader.GetDecimal("Price")));
                        book.Rating = ((decimal)(reader["Rating"] == DBNull.Value ? default : reader.GetDouble("Rating")));
                        book.DiscountedPrice = ((int)(reader["DiscountedPrice"] == DBNull.Value ? default : reader.GetDecimal("DiscountedPrice")));
                        book.RatingCount = reader["RatingCount"] == DBNull.Value ? default : reader.GetInt32("RatingCount");
                        book.BookImage = reader["BookImg"] == DBNull.Value ? default : reader.GetString("BookImg");
                        listOfUsers.Add(book);
                    }
                    return listOfUsers;
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
