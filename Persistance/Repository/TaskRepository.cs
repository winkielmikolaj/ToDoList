
using Microsoft.EntityFrameworkCore;
using ToDoList.Core.Models.Domains;

namespace ToDoList.Persistance.Repository
{
    public class TaskRepository
    {
        private ApplicationDbContext _context;
        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _context.Categories.OrderBy(x => x.Name).ToList();
        }

        public IEnumerable<Core.Models.Domains.Task> Get(string userId, bool isExecuted = false, int categoryId = 0, string title = null)
        {
            var tasks = _context.Tasks.Include(x => x.Category)
            .Where(x => x.UserId == userId && x.IsExecuted == isExecuted);


            if (categoryId != 0)
            {
                tasks = tasks.Where(x => x.CategoryId == categoryId);
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                tasks = tasks.Where(x => x.Title.Contains(title));
            }

            return (IEnumerable<Core.Models.Domains.Task>)tasks.OrderBy(x => x.Term).ToList();
        }

        public Core.Models.Domains.Task Get(int id, string userId)
        {
            var task = _context.Tasks.Single(x => x.Id == id && x.UserId == userId);

            return task;
        }

        public void Add(Core.Models.Domains.Task task)
        {

            try
            {
                _context.Tasks.Add(task);
                
            }
            catch (Exception ex)
            {
                // Log or inspect the exception details
                Console.WriteLine(ex.Message);
            }



            _context.Tasks.Add(task);
            
            

        }


        public void Update(Core.Models.Domains.Task task)
        {
            var taskToUpdate = _context.Tasks.Single(x => x.Id == task.Id);

            taskToUpdate.Category = task.Category;
            taskToUpdate.Description = task.Description;
            taskToUpdate.IsExecuted = task.IsExecuted;
            taskToUpdate.Term = task.Term;
            taskToUpdate.Title = task.Title;

            
        }

        public void Finish(int id, string userId)
        {
            var taskToUpdate = _context.Tasks.Single(x => x.Id == id && x.UserId == userId);

            taskToUpdate.IsExecuted = true;

            
        }

        public void Delete(int id, string userId)
        {
            var taskToDelete = _context.Tasks.Single(x => x.Id == id && x.UserId==userId);

            _context.Tasks.Remove(taskToDelete);

            
        }
    }
}
