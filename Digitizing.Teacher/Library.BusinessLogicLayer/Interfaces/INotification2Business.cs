using System;
using System.Collections.Generic;
using Library.DataModel;

namespace Library.BusinessLogicLayer
{
    public interface INotification2Business
    {
        bool Create(NotificationInfoModel model);
        List<NotificationInfoSearchModel> Search(NotificationInfoRequest request, out long total);
        List<DropdownOptionModel> GetParentsListDropdownByClass(string class_id);
        NotificationInfoModel GetById(Guid? id);
        List<NotificationInfoModel> Delete(string json_list_id, Guid updated_by);
        List<Notification2SearchModel> GetMyNotification(Notification2Request request, out long total);
        Notification2SearchModel GetMyNotificationById(Guid? id, Guid user_id);
        bool CheckNotification(Guid user_id);
        int CountUnCheckedNotification(Guid user_id);
    }
}
