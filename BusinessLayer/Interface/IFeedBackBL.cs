using CommonLayer.FeedBackModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IFeedBackBL
    {
        public bool AddFeedback(int UserId, FeedbackDataModel addModel);
        public List<GetFeedbackModel> GetAllFeedbacksByBookId(int BookId);


    }
}
