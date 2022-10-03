using CommonLayer.AddressModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IAddressBL
    {
        public bool AddAddress(int UserId, AddressDataModel addAddress);
        public List<GetAddressModel> GetAllAddress(int UserId);
        public bool DeleteByAddressId(int AddressId, int UserId);



    }
}
