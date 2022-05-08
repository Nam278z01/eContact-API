using Library.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccessLayer
{
    public partial interface IScientificResearchRepository
    {
        bool Create(ScientificResearchModel model);
        bool Update(ScientificResearchModel model);
        ScientificResearchModel GetById(string id);
        List<ScientificResearchSearchModel> Search(int pageIndex, int pageSize, string category_type_id,
            string student_id, string teacher_id, string research_project_name, string content, string rate_id, string rank, out long total);
        List<DropdownOptionModel> GetCategoryListDropdown();
        List<DropdownOptionModel> GetStudentListDropdown();
        List<DropdownOptionModel> GetTeacherListDropdown();
        List<DropdownOptionModel> GetTeacherRateListDropdown();
    }
}
