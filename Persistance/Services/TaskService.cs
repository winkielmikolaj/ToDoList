using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ToDoList.Core.Models.Domains;

namespace ToDoList.Persistance.Services
{
    public class TaskService
    {
        private readonly UnitOfWork _unitOfWork;


        public TaskService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Core.Models.Domains.Task> Get(string userId, bool isExecuted = false, int categoryId = 0, string title = null)
        {
            return _unitOfWork.Task.Get(userId, isExecuted, categoryId, title);
        }

        public IEnumerable<Category> GetCategories()
        {
            return _unitOfWork.Task.GetCategories();
        }

        public Core.Models.Domains.Task Get(int id, string userId)
        {
            return _unitOfWork.Task.Get(id, userId);
        }

        public void Add(Core.Models.Domains.Task task)
        {
            _unitOfWork.Task.Add(task);
            _unitOfWork.Complete();
        }


        public void Update(Core.Models.Domains.Task task)
        {
            _unitOfWork.Task.Update(task);
            _unitOfWork.Complete();
        }

        public void Finish(int id, string userId)
        {
            _unitOfWork.Task.Finish(id, userId);
            _unitOfWork.Complete();
        }

        public void Delete(int id, string userId)
        {
            _unitOfWork.Task.Delete(id, userId);
            _unitOfWork.Complete();
        }
    }
}
