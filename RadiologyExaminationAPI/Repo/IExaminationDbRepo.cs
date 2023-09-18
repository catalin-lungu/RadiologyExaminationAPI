namespace RadiologyExaminationAPI.Repo
{
    public interface IExaminationDbRepo
    {
        IEnumerable<string> GetExaminations(string id);
        IEnumerable<string> GetExaminations(string id, string date);
    }
}