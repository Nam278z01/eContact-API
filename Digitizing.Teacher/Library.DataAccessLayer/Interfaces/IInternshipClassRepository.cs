using Library.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DataAccessLayer
{
    public partial interface IInternshipClassRepository
    {
        List<InternshipClassSearchModel> Search(int pageIndex, int pageSize, string class_id_rcd,
            string academic_year, int semester, string internship_id_rcd, out long total);
        InternshipClassSearchModel GetById(string id);
        List<DropdownOptionModel> GetClassListDropdown();
        List<DropdownOptionModel> GetInternshipListDropdown();
    }
}
