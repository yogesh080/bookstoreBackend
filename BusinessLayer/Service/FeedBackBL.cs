using BusinessLayer.Interface;
using CommonLayer.FeedBackModel;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class FeedBackBL: IFeedBackBL
    {
        private readonly IFeedBackRL feedBackRL;
        public FeedBackBL(IFeedBackRL feedBackRL)
        {
            this.feedBackRL = feedBackRL;
        }

        public bool AddFeedback(int UserId, FeedbackDataModel addModel)
        {
            try
            {
                return feedBackRL.AddFeedback(UserId, addModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<GetFeedbackModel> GetAllFeedbacksByBookId(int BookId)
        {
            try
            {
                return feedBackRL.GetAllFeedbacksByBookId(BookId);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
