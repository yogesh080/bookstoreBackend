using BusinessLayer.Interface;
using CommonLayer.WishListItem;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class WishListBL:IWishListBL
    {
        private readonly IWishListRL wishListRL;
        public WishListBL(IWishListRL wishListRL)
        {
            this.wishListRL = wishListRL;
        }
        public string AddToWishList(int UserId, WishListPostModel postModel)
        {
            try
            {
                return wishListRL.AddToWishList(UserId, postModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string DeleteFromWishList(int UserId, int WishListId)
        {
            try
            {
                return wishListRL.DeleteFromWishList(UserId, WishListId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<WishListResponseModel> GetWishListByUserId(int UserId)
        {
            try
            {
                return wishListRL.GetWishListByUserId(UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }



    }
}
