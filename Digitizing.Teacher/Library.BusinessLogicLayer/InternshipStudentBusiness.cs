﻿using Library.BusinessLogicLayer;
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
        public List<InternshipStudentSearchModel> Search(int pageIndex, int pageSize, string user_id, string class_id_rcd,
            string internship, string company_name,
             out long total)
        {
            return _res.Search(pageIndex, pageSize, user_id, class_id_rcd,
                internship, company_name, 
                 out total);
        }
        /// <summary>
        /// Get list internship class dropdown
        /// </summary>
        /// <returns></returns>
        public List<DropdownOptionModel> GetClassListDropdown(string teacher_id_rcd)
        {
            return _res.GetClassListDropdown(teacher_id_rcd);
        }
        public List<DropdownOptionModel> GetCourseYearListDropdown()
        {
            return _res.GetCourseYearListDropdown();
        }
        public List<DropdownOptionModel> GetSchoolYearListDropdown()
        {
            return _res.GetSchoolYearListDropdown();
        }
        public List<DropdownOptionModel> GetCompanyListDropdown(string teacher_id_rcd)
        {
            return _res.GetCompanyListDropdown(teacher_id_rcd);
        }
        public List<DropdownOptionModel> GetInternshipListDropdown()
        {
            return _res.GetInternshipListDropdown();
        }
    }
}
