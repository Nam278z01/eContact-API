using Library.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccessLayer
{
    public partial interface IStudentProjectRegisterRepository
    {
        //bool Create(ScientificResearchModel model);
        bool Update(StudentProjectRegisterModel model);
        //ScientificResearchModel GetById(string id);
        List<StudentProjectRegisterSearchModel> Search(int pageIndex, int pageSize, string student_project_name,
            string academic_year, int semester, string class_id, out long total);
        //List<ScientificResearchModel> Delete(string json_list_id, Guid updated_by);
        //List<DropdownOptionModel> GetCategoryListDropdown();
        //List<DropdownOptionModel> GetStudentListDropdown();
        //List<DropdownOptionModel> GetTeacherListDropdown();
        //List<DropdownOptionModel> GetTeacherRateListDropdown();
    }
}
