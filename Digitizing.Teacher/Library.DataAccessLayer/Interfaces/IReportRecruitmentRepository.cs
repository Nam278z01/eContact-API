﻿using System;
using System.Collections.Generic;
using Library.DataModel;
namespace Library.DataAccessLayer
{
    public partial interface IReportRecruitmentRepository
    {
        List<RecruitmentReportSearchModel> Search(int pageIndex, int pageSize, string user_id, string class_id,
            string student_rcd, string student_name, string academic_year, int report_week, string company_rcd, out long total);
        List<RecruitmentReportModel> GetReportDetail(int page, int pageSize, string student_rcd,
                     int report_week, out long total);
        List<DropdownOptionModel> GetClassListDropdown();
        List<DropdownOptionModel> GetCompanyListDropdown();

    }
}