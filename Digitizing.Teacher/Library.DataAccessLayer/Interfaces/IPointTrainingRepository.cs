using System;
using System.Collections.Generic;
using Library.DataModel;
namespace Library.DataAccessLayer
{
    public partial interface IPointTrainingRepository
    {
        bool Create(PointTrainingModel model);
        bool Update(PointTrainingModel model);
        List<PointTrainingSearchModel> Search(PointTrainingRequest request, out long total);
        PointTrainingModel GetById(Guid? id);
        List<PointTrainingModel> Delete(string json_list_id, Guid updated_by);
    }
}
