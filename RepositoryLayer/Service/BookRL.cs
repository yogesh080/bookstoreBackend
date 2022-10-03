using CommonLayer.UserModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using CommonLayer.BookModel;
using Microsoft.AspNetCore.SignalR.Protocol;

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
                SqlCommand cmd = new SqlCommand("spCreateBook", connection)
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
                        book.Price = (reader["Price"] == DBNull.Value ? default : reader.GetDecimal("Price"));
                        book.DiscountedPrice = (reader["DiscountedPrice"] == DBNull.Value ? default : reader.GetDecimal("DiscountedPrice"));
                        book.Rating = (reader["Rating"] == DBNull.Value ? default : reader.GetDouble("Rating"));
                        book.RatingCount = reader["RatingCount"] == DBNull.Value ? default : reader.GetInt32("RatingCount");
                        book.BookImage = reader["BookImage"] == DBNull.Value ? default : reader.GetString("BookImage");
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

        public BookResponseModel UpdateBook(int BookId, BookResponseModel bookModel)
        {
            using SqlConnection connection = new SqlConnection(configuration["ConnectionString:BookStoreDB"]);
            try
            {
                
                using (connection)  
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("spUpdateBook", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@BookId", bookModel.BookId);
                    cmd.Parameters.AddWithValue("@BookName", bookModel.BookName);
                    cmd.Parameters.AddWithValue("@Author", bookModel.Author);
                    cmd.Parameters.AddWithValue("@Description", bookModel.Description);
                    cmd.Parameters.AddWithValue("@Quantity", bookModel.Quantity);
                    cmd.Parameters.AddWithValue("@Price", bookModel.Price);
                    cmd.Parameters.AddWithValue("@DiscountedPrice", bookModel.DiscountedPrice);
                    cmd.Parameters.AddWithValue("@Rating", bookModel.Rating);
                    cmd.Parameters.AddWithValue("@RatingCount", bookModel.RatingCount);
                    cmd.Parameters.AddWithValue("@BookImage", bookModel.BookImage);

                    var result = cmd.ExecuteNonQuery();
                    if (result != 0)
                    {
                        return bookModel;
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
            finally
            {
                connection.Close();
   
            }
        }

        public bool DeleteBook(int BookId)
        {
            using SqlConnection connection = new SqlConnection(configuration["ConnectionString:BookStoreDB"]);

            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("spDeleteBook", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@BookId", BookId);

                    var result = command.ExecuteNonQuery();
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
            catch (Exception)
            {

                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public BookResponseModel RetrieveBookById(int BookId)
        {
            using SqlConnection connection = new SqlConnection(configuration["ConnectionString:BookStoreDB"]);

            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("spGetBookByID", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@BookId", BookId);
                    var result = command.ExecuteNonQuery();
                    if (result == null)
                    {
                        return null;
                    }
                    SqlDataReader reader = command.ExecuteReader();
                    BookResponseModel response = new BookResponseModel();
                    while (reader.Read())
                    {
                        response.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                        response.BookName = reader["BookName"] == DBNull.Value ? default : reader.GetString("BookName");
                        response.Author = reader["Author"] == DBNull.Value ? default : reader.GetString("Author");
                        response.Description = reader["Description"] == DBNull.Value ? default : reader.GetString("Description");
                        response.Quantity = reader["Quantity"] == DBNull.Value ? default : reader.GetInt32("Quantity");
                        response.Price = (reader["Price"] == DBNull.Value ? default : reader.GetDecimal("Price"));
                        response.DiscountedPrice = (reader["DiscountedPrice"] == DBNull.Value ? default : reader.GetDecimal("DiscountedPrice"));
                        response.Rating = (reader["Rating"] == DBNull.Value ? default : reader.GetDouble("Rating"));
                        response.RatingCount = reader["RatingCount"] == DBNull.Value ? default : reader.GetInt32("RatingCount");
                        response.BookImage = reader["BookImage"] == DBNull.Value ? default : reader.GetString("BookImage");
                    }
                    return response;
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
