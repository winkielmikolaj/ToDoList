using ToDoList.Core.Models;
using ToDoList.Core.Models.Domains;

namespace ToDoList.Core.ViewModels
{
    public class TasksViewModel
    {
        public IEnumerable<Models.Domains.Task> Tasks { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public FilterTasks FilterTasks { get; set; }
    }
}
