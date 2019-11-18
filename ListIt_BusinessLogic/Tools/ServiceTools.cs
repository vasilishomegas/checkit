using ListIt_DataAccess.Repository.Generics;

namespace ListIt_BusinessLogic.Tools
{
    public class ServiceTools<T> where T : class
    {
        public static bool CheckIfExists(Repository<T> repository, int id)
        {
            var existing = repository.Get(id);
            return existing != null;
        }
    }
}
