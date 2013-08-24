using System.Data.Entity.ModelConfiguration;
using Carbontally.Domain.Entities;

namespace Carbontally.Domain.Persistence
{
    public class StudyConfiguration : EntityTypeConfiguration<Study>
    {
        public StudyConfiguration()
        {
            // fluent configurations go here.
        }
    }
}
