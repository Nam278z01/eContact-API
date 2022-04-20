using Library.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer
{
    public partial interface IStudentClassBusiness
    {
        List<StudentClassModel> Search(int pageIndex, int pageSize, string class_id,
             string student_name,
             out long total);
        //InternshipClassSearchModel GetById(string id);
        List<DropdownOptionModel> GetClassListDropdown();
        //List<DropdownOptionModel> GetInternshipListDropdown();
    }
}
