using System;
using System.Collections.Generic;
using Library.DataModel;

namespace Library.DataAccessLayer
{
    public interface INotification2Repository
    {
        bool Create(NotificationInfoModel model);
        List<NotificationInfoSearchModel> Search(NotificarionInfoRequest request, out long total);
        List<DropdownOptionModel> GetParentsListDropdownByClass(string class_id);
        NotificationInfoModel GetById(Guid? id);
        List<NotificationInfoModel> Delete(string json_list_id, Guid updated_by);
    }
}
