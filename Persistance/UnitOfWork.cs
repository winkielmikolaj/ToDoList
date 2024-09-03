using Microsoft.EntityFrameworkCore;
using ToDoList.Persistance.Repository;

namespace ToDoList.Persistance
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            Task = new TaskRepository(context); // Upewnij się, że to nie jest null
        }

        public TaskRepository Task {  get; set; }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
