using BusinessLayer.Interface;
using CommonLayer.CardModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class AddCartBL:IAddCartBL
    {
        private readonly IAddCartRL addcartRL;
        public AddCartBL(IAddCartRL addcartRL)
        {
            this.addcartRL = addcartRL;
        }
        public string AddToCart(int UserId, CartPostModel cartModel)
        {
            try
            {
                return addcartRL.AddToCart(UserId, cartModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CartRetrieveModel> GetAllCartItems(int UserId)
        {
            try
            {
                return addcartRL.GetAllCartItems(UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string DeleteFromCart(int CartId, int UserId)
        {
            try
            {
                return addcartRL.DeleteFromCart(CartId, UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string UpdateCart(int UserId, CartUpdateModel cartUpdateModel)
        {
            try
            {
                return addcartRL.UpdateCart(UserId, cartUpdateModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
