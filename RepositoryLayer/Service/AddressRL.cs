using CommonLayer.AddressModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text;

namespace RepositoryLayer.Service
{
    public class AddressRL: IAddressRL
    {
        private readonly IConfiguration configuration;
        private readonly IConfiguration _AppSetting;
        public AddressRL(IConfiguration configuration, IConfiguration _AppSetting)
        {
            this.configuration = configuration;
            this._AppSetting = _AppSetting;
        }
        public bool AddAddress(int UserId, AddressDataModel addAddress)
        {

            try
            {
                using SqlConnection connection = new SqlConnection(configuration["ConnectionString:BookStoreDB"]);

                SqlCommand command = new SqlCommand("spAddAddress", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                {
                    
                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.Parameters.AddWithValue("@AddressType", addAddress.AddressType);
                    command.Parameters.AddWithValue("@FullAddress", addAddress.FullAddress);
                    command.Parameters.AddWithValue("@City", addAddress.City);
                    command.Parameters.AddWithValue("@State", addAddress.State);

                    connection.Open();

                    int result = command.ExecuteNonQuery();

                    connection.Close();

                    if (result > 0)
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

        }

        public List<GetAddressModel> GetAllAddress(int UserId)
        {
            List<GetAddressModel> list = new List<GetAddressModel>();
            using SqlConnection connection = new SqlConnection(configuration["ConnectionString:BookStoreDB"]);

            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("GetAllAddressSP", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.AddWithValue("@UserId", UserId);
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        GetAddressModel addressdetails = new GetAddressModel
                        {
                            AddressId = reader["AddressId"] == DBNull.Value ? default : reader.GetInt32("AddressId"),
                            UserId = UserId,
                            FullName = reader["FullName"] == DBNull.Value ? default : reader.GetString("FullName"),
                            MobileNumber = reader["MobileNumber"] == DBNull.Value ? default : reader.GetInt64("MobileNumber"),
                            AddressType = reader["AddressType"] == DBNull.Value ? default : reader.GetInt32("AddressType"),
                            FullAddress = reader["FullAddress"] == DBNull.Value ? default : reader.GetString("FullAddress"),
                            City = reader["City"] == DBNull.Value ? default : reader.GetString("City"),
                            State = reader["State"] == DBNull.Value ? default : reader.GetString("State")
                        };
                        list.Add(addressdetails);
                    }
                    return list;

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

        public bool DeleteByAddressId( int AddressId , int UserId)
        {
            using SqlConnection connection = new SqlConnection(configuration["ConnectionString:BookStoreDB"]);

            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("spDeleteByAddressId", connection)
                    {
                        CommandType=CommandType.StoredProcedure
                    };

                    command.Parameters.AddWithValue("@UserId", UserId);
                    command.Parameters.AddWithValue("@AddressId", AddressId);

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

        public bool UpdateAddressbyId(int UserId, AddressUpdateModel updateAddress)
        {
            using SqlConnection connection = new SqlConnection(configuration["ConnectionString:BookStoreDB"]);
            try
            {
                {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand("spUpdateAddressbyId", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@AddressId", updateAddress.AddressId);
                    cmd.Parameters.AddWithValue("@AddressType", updateAddress.AddressType);
                    cmd.Parameters.AddWithValue("@FullAddress", updateAddress.FullAddress);
                    cmd.Parameters.AddWithValue("@City", updateAddress.City);
                    cmd.Parameters.AddWithValue("@State", updateAddress.State);

                    var result = cmd.ExecuteNonQuery();
                    if (result > 0)
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


    }
}
