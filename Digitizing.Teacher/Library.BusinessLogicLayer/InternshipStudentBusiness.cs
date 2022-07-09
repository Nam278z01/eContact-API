using Library.BusinessLogicLayer;
using Library.DataAccessLayer;
using Library.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer
{
    public partial class InternshipStudentBusiness : IInternshipStudentBusiness
    {
        private IInternshipStudentRepository _res;
        public InternshipStudentBusiness(IInternshipStudentRepository res)
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
        public List<InternshipStudentSearchModel> Search(int pageIndex, int pageSize,string user_id, string class_id_rcd,
            string school_year,string company_name, string course_year,
             out long total)
        {
            return _res.Search(pageIndex, pageSize, user_id, class_id_rcd,
                school_year, company_name, course_year,
                 out total);
        }
        /// <summary>
        /// Get list internship class dropdown
        /// </summary>
        /// <returns></returns>
        public List<DropdownOptionModel> GetClassListDropdown()
        {
            return _res.GetClassListDropdown();
        }
        public List<DropdownOptionModel> GetCourseYearListDropdown()
        {
            return _res.GetCourseYearListDropdown();
        }
        public List<DropdownOptionModel> GetSchoolYearListDropdown()
        {
            return _res.GetSchoolYearListDropdown();
        }
        public List<DropdownOptionModel> GetCompanyListDropdown()
        {
            return _res.GetCompanyListDropdown();
        }
    }
}
