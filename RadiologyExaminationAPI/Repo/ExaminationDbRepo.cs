using RadiologyExaminationData;
using RadiologyExaminationData.Entities;

namespace RadiologyExaminationAPI.Repo
{
    public class ExaminationDbRepo : IExaminationDbRepo
    {
        private QueryService _queryService;

        public ExaminationDbRepo(ExamsContext examsContext)
        {
             _queryService = new QueryService(examsContext);
        }

        public IEnumerable<string> GetExaminations(string id) => _queryService.GetPathExaminations(id);

        public IEnumerable<string> GetExaminations(string id, string date) => _queryService.GetPathExaminations(id, date);
    }
}
