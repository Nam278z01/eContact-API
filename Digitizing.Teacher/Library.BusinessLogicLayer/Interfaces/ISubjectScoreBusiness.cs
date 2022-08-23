using System;
using System.Collections.Generic;
using Library.DataModel;

namespace Library.BusinessLogicLayer
{
    public partial interface ISubjectScoreBusiness
    {
        bool Create(SubjectScoreModel model);
        bool Update(SubjectScoreModel model);
        List<SubjectScoreSearchModel> Search(SubjectScoreRequest request, out long total);
        SubjectScoreModel GetById(Guid? id);
        List<SubjectScoreModel> Delete(string json_list_id, Guid updated_by);
    }
}
