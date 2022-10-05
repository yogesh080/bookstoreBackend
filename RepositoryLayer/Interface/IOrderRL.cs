using CommonLayer.OrderModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IOrderRL
    {
        public bool AddOrder(OrderDataModel postModel);
        public List<GetOrderModel> GetAllOrder(int userId);


    }
}
