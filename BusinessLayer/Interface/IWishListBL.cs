using CommonLayer.WishListItem;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IWishListBL
    {
        public string AddToWishList(int UserId, WishListPostModel postModel);
        public string DeleteFromWishList(int UserId, int WishListId);
        public List<WishListResponseModel> GetWishListByUserId(int UserId);

    }
}
