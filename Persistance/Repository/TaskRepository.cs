
namespace ToDoList.Persistance.Repository
{
    public class TaskRepository
    {
        public TaskRepository() { }

        internal object GetCategories()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Task> Get(string userId, bool isExecuted = false, int categoryId = 0, string title = null)
        {
            throw new NotImplementedException();
        }

        internal Core.Models.Domains.Task Get(int id, string userId)
        {
            throw new NotImplementedException();
        }

        internal void Add(Core.Models.Domains.Task task)
        {
            throw new NotImplementedException();
        }

        internal void Update(Core.Models.Domains.Task task)
        {
            throw new NotImplementedException();
        }

        internal void Finish(int id, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
