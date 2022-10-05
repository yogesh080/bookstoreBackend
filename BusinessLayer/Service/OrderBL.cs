using BusinessLayer.Interface;
using CommonLayer.OrderModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class OrderBL : IOrderBL
    {
        private readonly IOrderRL orderRL;
        public OrderBL(IOrderRL orderRL)
        {
            this.orderRL = orderRL;
        }
        public bool AddOrder(OrderDataModel postModel)
        {
            try
            {
                return orderRL.AddOrder(postModel);

            }
            catch (Exception)
            {

                throw;
            }
        }





    }
}
