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
    public partial class StudentClassBusiness : IStudentClassBusiness
    {
        private IStudentClassRepository _res;
        public StudentClassBusiness(IStudentClassRepository res)
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
        public List<StudentClassModel> Search(int pageIndex, int pageSize, string user_id, string class_id,
             string student_name,
             out long total)
        {
            return _res.Search(pageIndex, pageSize, user_id, class_id, student_name, out total);
        }
        /// <summary>
        /// Get the information by using id of the table WebsiteTag
        /// </summary>
        /// <param name="id">Id used to get the information</param>
        /// <returns></returns>
        //public StudentRefModel GetById(string id)
        //{
        //    var result = _res.GetById(id);
        //    return result;
        //}

        /// <summary>
        /// Add a new records into the table EvaluateRecruitment 
        /// </summary>
        /// <param name="model">the record added </param>
        /// <returns></returns>
        public bool Update(StudentClassModel model)
        {
            return _res.Update(model);
        }


        /// <summary>
        /// Get list internship class dropdown
        /// </summary>
        /// <returns></returns>
        public List<DropdownOptionModel> GetClassListDropdown(string teacher_id_rcd)
        {
            return _res.GetClassListDropdown( teacher_id_rcd);
        }
        /// <summary>
        /// Get list internship class dropdown
        /// </summary>
        /// <returns></returns>
        //public List<DropdownOptionModel> GetInternshipListDropdown()
        //{
        //    return _res.GetInternshipListDropdown();
        //}
    }
}
