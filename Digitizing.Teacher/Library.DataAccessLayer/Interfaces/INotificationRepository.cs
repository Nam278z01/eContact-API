﻿using Library.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccessLayer
{
    public partial interface INotificationRepository
    {
        List<NotificationModel> Search(int pageIndex, int pageSize, string class_id,
             string student_name,
             out long total);
        //StudentRefModel GetById(string id);
        ///List<DropdownOptionModel> GetClassListDropdown();
        //bool Update(StudentClassModel model);

        //List<DropdownOptionModel> GetInternshipListDropdown();
    }
}
