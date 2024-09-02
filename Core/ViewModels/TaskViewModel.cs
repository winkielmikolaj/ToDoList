using ToDoList.Core.Models.Domains;

namespace ToDoList.Core.ViewModels
{
    public class TaskViewModel
    {
        public string Heading { get; set; }

        public Models.Domains.Task Task { get; set; }

        public IEnumerable<Category> Categories { get; set; }
    }
}
