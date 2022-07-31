using Library.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccessLayer
{
    public partial interface IInternshipStudentRepository
    {
        List<InternshipStudentSearchModel> Search(int pageIndex, int pageSize, string user_id, string class_id_rcd,
            string internship, string company_name,
             out long total);
        //InternshipClassSearchModel GetById(string id);
        List<DropdownOptionModel> GetClassListDropdown(string teacher_id_rcd);
        List<DropdownOptionModel> GetCourseYearListDropdown();
        List<DropdownOptionModel> GetSchoolYearListDropdown();
        List<DropdownOptionModel> GetCompanyListDropdown(string teacher_id_rcd);
        List<DropdownOptionModel> GetInternshipListDropdown();
    }
}
