using System;
using System.Collections.Generic;
using Library.DataModel;

namespace Library.DataAccessLayer
{
    public interface INotification2Repository
    {
        bool Create(NotificationInfoModel model);
    }
}
