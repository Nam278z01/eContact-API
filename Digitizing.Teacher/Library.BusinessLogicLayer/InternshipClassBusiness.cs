using Library.BusinessLogicLayer.Interfaces;
using Library.DataAccessLayer;
using Library.DataAccessLayer.Interfaces;
using Library.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.BusinessLogicLayer
{
    public partial class InternshipClassBusiness : IInternshipClassBusiness
    {
        private IInternshipClassRepository _res;
        public InternshipClassBusiness(IInternshipClassRepository res)
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
        public List<InternshipClassSearchModel> Search(int pageIndex, int pageSize, string class_id_rcd,
            string academic_year, int semester, string internship_id_rcd, out long total)
        {
            return _res.Search(pageIndex, pageSize, class_id_rcd,
                academic_year, semester, internship_id_rcd, out total);
        }
        /// <summary>
        /// Get the information by using id of the table WebsiteTag
        /// </summary>
        /// <param name="id">Id used to get the information</param>
        /// <returns></returns>
        public InternshipClassSearchModel GetById(string id)
        {
            var result = _res.GetById(id);
            return result;
        }
        /// <summary>
        /// Get list internship class dropdown
        /// </summary>
        /// <returns></returns>
        public List<DropdownOptionModel> GetClassListDropdown()
        {
            return _res.GetClassListDropdown();
        }
        /// <summary>
        /// Get list internship class dropdown
        /// </summary>
        /// <returns></returns>
        public List<DropdownOptionModel> GetInternshipListDropdown()
        {
            return _res.GetInternshipListDropdown();
        }
    }
}
