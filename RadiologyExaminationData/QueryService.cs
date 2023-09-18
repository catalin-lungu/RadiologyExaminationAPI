using Microsoft.IdentityModel.Tokens;
using RadiologyExaminationData.Entities;

namespace RadiologyExaminationData
{
    public sealed class QueryService
    {

        private ExamsContext _context;
        public QueryService(ExamsContext examsContext)
        {
            _context = examsContext;
        }

        public IEnumerable<string> GetPathExaminations(string id)
        {
            var exams = _context.Exams.Where(e => e.Cnp == id).Select(e => e.Path ?? "").ToList();
            return exams;
        }

        public IEnumerable<string> GetPathExaminations(string id, string date)
        {
            var exams = (from exam in _context.Exams
                             join examPatient in _context.ExamPatients on exam.Id equals examPatient.ExamId
                             where examPatient.PatientId == id && examPatient.Date == date
                             select exam.Path ?? "").ToList();
            return exams;
        }
    }
}
