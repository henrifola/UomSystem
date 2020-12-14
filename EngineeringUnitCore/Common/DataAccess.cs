using Data;

namespace UomRepository.Common
{
    public class DataAcces
    {

        protected DataAcces(RepositoryContext context)
        {
            Context = context;
        }
        protected RepositoryContext Context { get; }
    }
}