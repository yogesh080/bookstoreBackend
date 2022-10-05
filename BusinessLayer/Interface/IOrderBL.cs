using CommonLayer.OrderModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IOrderBL
    {
        public bool AddOrder( OrderDataModel postModel);
        public List<GetOrderModel> GetAllOrder(int userId);


    }
}
