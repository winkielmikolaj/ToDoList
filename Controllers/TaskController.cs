using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoList.Core.Models;
using ToDoList.Core.ViewModels;
using ToDoList.Persistance;
using ToDoList.Persistance.Extensions;
using ToDoList.Persistance.Repository;
using Task = ToDoList.Core.Models.Domains.Task;

namespace ToDoList.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private TaskRepository _taskRepository;

        public TaskController(ApplicationDbContext context)
        {
            _taskRepository = new TaskRepository(context);
        }

        // Akcja do listowania zadań
        [HttpGet("tasks/list")]
        public IActionResult ListTasks()
        {
            var userId = User.GetUserId();

            var vm = new TasksViewModel
            {
                FilterTasks = new FilterTasks(),
                Tasks = (IEnumerable<Core.Models.Domains.Task>)_taskRepository.Get(userId),
                Categories = (IEnumerable<Core.Models.Domains.Category>)_taskRepository.GetCategories()
            };

            return View(vm);
        }

        // Akcja do filtrowania zadań
        [HttpPost("tasks/filter")]
        public IActionResult FilterTasks(TasksViewModel viewModel)
        {
            var userId = User.GetUserId();

            var tasks = _taskRepository.Get(userId,
                viewModel.FilterTasks.IsExecuted,
                viewModel.FilterTasks.CategoryId,
                viewModel.FilterTasks.Title);

            return PartialView("_TasksTable", tasks);
        }

        // Akcja do edytowania lub tworzenia zadania
        [HttpGet("tasks/edit/{id?}")]
        public IActionResult EditTask(int id = 0)
        {
            var userId = User.GetUserId();

            var task = id == 0
                ? new Task { Id = 0, UserId = userId, Term = DateTime.Today }
                : _taskRepository.Get(id, userId);

            var vm = new TaskViewModel
            {
                Task = task,
                Heading = id == 0 ? "Dodawanie nowego zadania" : "Edytowanie zadania",
                Categories = (IEnumerable<Core.Models.Domains.Category>)_taskRepository.GetCategories()
            };

            return View(vm);
        }

        // Akcja do zapisywania zadania (dodawanie/edycja)
        [HttpPost("tasks/save")]
        [ValidateAntiForgeryToken]
        public IActionResult SaveTask(Task task)
        {
            var userId = User.GetUserId();
            task.UserId = userId;

            if (!ModelState.IsValid)
            {
                var vm = new TaskViewModel
                {
                    Task = task,
                    Heading = task.Id == 0 ? "Dodawanie nowego zadania" : "Edytowanie zadania",
                    Categories = (IEnumerable<Core.Models.Domains.Category>)_taskRepository.GetCategories()
                };

                return View("Task", vm);
            }

            if (task.Id == 0)
            {
                _taskRepository.Add(task);
            }
            else
            {
                _taskRepository.Update(task);
            }

            return RedirectToAction("ListTasks");
        }

        // Akcja do oznaczania zadania jako ukończonego
        [HttpPost("tasks/finish")]
        public IActionResult Finish(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _taskRepository.Finish(id, userId);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }

            return Json(new { success = true });
        }

        // Akcja do usuwania zadania
        [HttpPost("tasks/delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _taskRepository.Delete(id, userId);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }

            return Json(new { success = true });
        }
    }
}
