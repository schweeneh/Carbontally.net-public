using Carbontally.Domain.Entities;

namespace Carbontally.Domain.Persistence
{
    public class StudyRepository : IStudyRepository
    {
        public void Save(Study study)
        {
            using(var context = new CarbontallyContext())
            {
                context.Studies.Add(study);
                context.SaveChanges();
            }
        }
    }

    public interface IStudyRepository
    {
        void Save(Study study);
    }
}
