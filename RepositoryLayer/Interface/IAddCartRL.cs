using CommonLayer.CardModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IAddCartRL
    {
        public string AddToCart(int UserId, CartPostModel cartModel);
        public List<CartRetrieveModel> GetAllCartItems(int UserId);
        public string DeleteFromCart(int CartId, int UserId);
        public string UpdateCart(int UserId, CartUpdateModel cartUpdateModel);




    }
}
