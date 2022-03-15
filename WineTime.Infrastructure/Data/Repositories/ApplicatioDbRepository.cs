namespace WineTime.Infrastructure.Data.Repositories
{
    using WineTime.Infrastructure.Data;
    using WineTime.Infrastructure.Data.Common;
    using WineTime.Infrastructure.Data.Repositories;

    public class ApplicatioDbRepository : Repository, IApplicatioDbRepository
    {
        public ApplicatioDbRepository(ApplicationDbContext context)
        {
            this.Context = context;
        }
    }
}
