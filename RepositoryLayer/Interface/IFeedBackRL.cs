using CommonLayer.FeedBackModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IFeedBackRL
    {
        public bool AddFeedback(int UserId, FeedbackDataModel addModel);

    }
}
