using Library.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer
{
    public partial interface IStudentInformationBusiness
    {
        List<StudentInformationModel> Search(int pageIndex, int pageSize,
            string school_year,
            string class_id,
            string student_rcd,
             string student_name,
             out long total);
        //StudentRefModel GetById(string id);
        List<DropdownOptionModel> GetClassListDropdown();
        List<DropdownOptionModel> GetSchoolYearListDropdown();
    }
}
