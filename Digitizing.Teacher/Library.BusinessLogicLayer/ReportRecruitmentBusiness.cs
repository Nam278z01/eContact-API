using Library.DataAccessLayer;
using Library.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer
{
    public partial class ReportRecruitmentBusiness : IReportRecruitmentBusiness
    {
        private IReportRecruitmentRepository _res;
        public ReportRecruitmentBusiness(IReportRecruitmentRepository res)
        {
            _res = res;
        }

        /// <summary>
        /// Searching information in the table WebsiteTag
        /// </summary>
        /// <param name="pageIndex">which page?</param>
        /// <param name="pageSize">the number of records in a page</param>
        /// <param name="lang"> Language used to display data</param>
        /// <param name="total">the total number of records</param> 
        /// <returns></returns>
        public List<RecruitmentReportSearchModel> Search(int pageIndex, int pageSize, string class_id,
            string student_rcd, string student_name, string academic_year, int report_week, out long total)
        {
            return _res.Search(pageIndex, pageSize, class_id, student_rcd,
            student_name, academic_year, report_week, out total);
        }
        /// <summary>
        /// Get the information by using id of the table WebsiteTag
        /// </summary>
        /// <param name="id">Id used to get the information</param>
        /// <returns></returns>
        public List<RecruitmentReportModel> GetReportDetail(int page, int pageSize, string student_rcd,
                     int report_week, out long total)
        {
            var result = _res.GetReportDetail(page, pageSize, student_rcd,
                     report_week, out total);
            return result;
        }
        /// <summary>
        /// Get list internship class dropdown
        /// </summary>
        /// <returns></returns>
        public List<DropdownOptionModel> GetClassListDropdown()
        {
            return _res.GetClassListDropdown();
        }
    }
}
