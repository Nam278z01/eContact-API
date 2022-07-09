﻿using Library.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccessLayer
{
    public partial interface IInternshipStudentRepository
    {
        List<InternshipStudentSearchModel> Search(int pageIndex, int pageSize,string user_id, string class_id_rcd,
           string school_year, string company_name, string course_year,
             out long total);
        //InternshipClassSearchModel GetById(string id);
        List<DropdownOptionModel> GetClassListDropdown();
        List<DropdownOptionModel> GetCourseYearListDropdown();
        List<DropdownOptionModel> GetSchoolYearListDropdown();
        List<DropdownOptionModel> GetCompanyListDropdown();
    }
}
