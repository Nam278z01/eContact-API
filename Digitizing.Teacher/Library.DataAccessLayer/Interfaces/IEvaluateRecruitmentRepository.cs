using System;
using System.Collections.Generic;
using Library.DataModel;
namespace Library.DataAccessLayer
{
    public partial interface IEvaluateRecruitmentRepository
    {
        bool Create(EvaluateRecruitmentModel model);
        bool Update(EvaluateRecruitmentModel model);
        //List<WebsiteTagModel> Search(int pageIndex, int pageSize, char lang, out long total, string tag_name);
        EvaluateRecruitmentModel GetById(Guid? id);
        //List<WebsiteTagModel> Delete(string json_list_id, Guid updated_by);
        //List<DropdownOptionModel> GetListDropdown(char lang);

    }
}