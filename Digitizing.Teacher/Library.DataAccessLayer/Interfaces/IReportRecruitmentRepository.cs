using System;
using System.Collections.Generic;
using Library.DataModel;
namespace Library.DataAccessLayer
{
    public partial interface IReportRecruitmentRepository
    {
        List<ReportRecruitmentModel> Search(int pageIndex, int pageSize, string class_id,
            string student_rcd, string student_name, int report_week_rcd, out long total);
        List<RecruitmentReportWeeklyModel> GetReportDetail(int page, int pageSize, string student_rcd,
                     int report_week_rcd, out long total);
        List<DropdownOptionModel> GetClassListDropdown();

    }
}