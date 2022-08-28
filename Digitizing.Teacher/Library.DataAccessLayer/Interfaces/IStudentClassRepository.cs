using Library.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccessLayer
{
    public partial interface IStudentClassRepository
    {
        List<StudentClassModel> Search(int pageIndex, int pageSize, string user_id, string class_id,
             string student_name,
             out long total);
        StudentRefModel GetById(string id);
        List<DropdownOptionModel> GetClassListDropdown(string teacher_id_rcd);
        bool Update(StudentClassModel model);

        //List<DropdownOptionModel> GetInternshipListDropdown();
        List<DropdownOptionModel> GetStudentListDropdownByFamily(Guid user_id);
    }
}
