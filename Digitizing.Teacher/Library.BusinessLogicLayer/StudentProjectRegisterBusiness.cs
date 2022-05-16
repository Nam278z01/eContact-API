using Library.BusinessLogicLayer;
using Library.DataAccessLayer;
//using Library.DataAccessLayer;
using Library.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer
{
    public partial class StudentProjectRegisterBusiness : IStudentProjectRegisterBusiness
    {
        private IStudentProjectRegisterRepository _res;
        public StudentProjectRegisterBusiness(IStudentProjectRegisterRepository res)
        {
            _res = res;
        }
        //public bool Create(ScientificResearchModel model)
        //{
        //    return _res.Create(model);
        //}
        public bool Update(StudentProjectRegisterModel model)
        {
            return _res.Update(model);
        }
        //public ScientificResearchModel GetById(string id)
        //{
        //    return _res.GetById(id);
        //}

        /// <summary>
        /// Searching information in the table WebsiteTag
        /// </summary>
        /// <param name="pageIndex">which page?</param>
        /// <param name="pageSize">the number of records in a page</param>
        /// <param name="lang"> Language used to display data</param>
        /// <param name="total">the total number of records</param> 
        /// <returns></returns>
        public List<StudentProjectRegisterSearchModel> Search(int pageIndex, int pageSize, string student_project_name,
            string academic_year, int semester, string class_id, out long total)
        {
            return _res.Search(pageIndex, pageSize, student_project_name, academic_year, semester, class_id, out total);
        }

        //public List<ScientificResearchModel> Delete(string json_list_id, Guid updated_by)
        //{
        //    return _res.Delete(json_list_id, updated_by);
        //}
        /// <summary>
        /// Get list internship class dropdown
        /// </summary>
        /// <returns></returns>
        //public List<DropdownOptionModel> GetCategoryListDropdown()
        //{
        //    return _res.GetCategoryListDropdown();
        //}
        /// <summary>
        /// Get list internship class dropdown
        /// </summary>
        /// <returns></returns>
        //public List<DropdownOptionModel> GetStudentListDropdown()
        //{
        //    return _res.GetStudentListDropdown();
        //}
        //public List<DropdownOptionModel> GetTeacherListDropdown()
        //{
        //    return _res.GetTeacherListDropdown();
        //}
        //public List<DropdownOptionModel> GetTeacherRateListDropdown()
        //{
        //    return _res.GetTeacherRateListDropdown();
        //}
    }
}
