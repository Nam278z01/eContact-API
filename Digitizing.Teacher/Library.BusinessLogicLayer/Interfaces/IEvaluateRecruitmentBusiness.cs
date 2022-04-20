using System;
using System.Collections.Generic;
using Library.DataModel;

namespace Library.BusinessLogicLayer
{
    public partial interface IEvaluateRecruitmentBusiness
    {
        bool Create(EvaluateRecruitmentModel model);
        bool Update(EvaluateRecruitmentModel model);
        //List<EvaluateRecruitmentModel> Search(int pageIndex, int pageSize, char lang, out long total, string tag_name);
        EvaluateRecruitmentModel GetById(string id);
        //List<EvaluateRecruitmentModel> Delete(string json_list_id, Guid updated_by);
        //List<DropdownOptionModel> GetListDropdown(char lang);

    }
}