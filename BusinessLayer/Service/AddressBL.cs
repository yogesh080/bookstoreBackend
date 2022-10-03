using BusinessLayer.Interface;
using CommonLayer.AddressModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class AddressBL: IAddressBL
    {
        private readonly IAddressRL addressRL;
        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }

        public bool AddAddress(int UserId, AddressDataModel addAddress)
        {
            try
            {
                return addressRL.AddAddress(UserId, addAddress);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<GetAddressModel> GetAllAddress(int UserId)
        {
            try
            {
                return addressRL.GetAllAddress(UserId);

            }
            catch (Exception)
            {

                throw;
            }
        }



    }
}
