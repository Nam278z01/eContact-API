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
    public partial class ScientificResearchBusiness : IScientificResearchBusiness
    {
        private IScientificResearchRepository _res;
        public ScientificResearchBusiness(IScientificResearchRepository res)
        {
            _res = res;
        }
        public bool Create(ScientificResearchModel model)
        {
            return _res.Create(model);
        }
        public bool Update(ScientificResearchModel model)
        {
            return _res.Update(model);
        }
        public ScientificResearchModel GetById(string id)
        {
            return _res.GetById(id);
        }

        /// <summary>
        /// Searching information in the table WebsiteTag
        /// </summary>
        /// <param name="pageIndex">which page?</param>
        /// <param name="pageSize">the number of records in a page</param>
        /// <param name="lang"> Language used to display data</param>
        /// <param name="total">the total number of records</param> 
        /// <returns></returns>
        public List<ScientificResearchSearchModel> Search(int pageIndex, int pageSize, string category_type_id,
            string student_id, string teacher_id, string research_project_name, string content, string rate_id, string rank, out long total)
        {
            return _res.Search(pageIndex, pageSize, category_type_id, student_id, teacher_id, 
                research_project_name, content, rate_id,
                rank, out total);
        }

        /// <summary>
        /// Get list internship class dropdown
        /// </summary>
        /// <returns></returns>
        public List<DropdownOptionModel> GetCategoryListDropdown()
        {
            return _res.GetCategoryListDropdown();
        }
        /// <summary>
        /// Get list internship class dropdown
        /// </summary>
        /// <returns></returns>
        public List<DropdownOptionModel> GetStudentListDropdown()
        {
            return _res.GetStudentListDropdown();
        }
        public List<DropdownOptionModel> GetTeacherListDropdown()
        {
            return _res.GetTeacherListDropdown();
        }
        public List<DropdownOptionModel> GetTeacherRateListDropdown()
        {
            return _res.GetTeacherRateListDropdown();
        }
    }
}
