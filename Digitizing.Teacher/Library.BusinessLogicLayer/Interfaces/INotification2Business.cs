using System;
using System.Collections.Generic;
using Library.DataModel;

namespace Library.BusinessLogicLayer
{
    public interface INotification2Business
    {
        bool Create(NotificationInfoModel model);
    }
}
