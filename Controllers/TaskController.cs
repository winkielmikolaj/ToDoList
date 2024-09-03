using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Core.ViewModels;
using ToDoList.Persistance;
using ToDoList.Persistance.Extensions;
using ToDoList.Persistance.Repository;
using ToDoList.Persistance.Services;

namespace ToDoList.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {


        private TaskService _taskService;

        public TaskController(ApplicationDbContext context)
        {
            _taskService = new TaskService(new UnitOfWork(context));
        }
        public IActionResult Tasks()
        {
            var userId = User.GetUserId();
            var vm = new TasksViewModel
            {
                FilterTasks = new ToDoList.Core.Models.FilterTasks(),
                Tasks = _taskService.Get(userId),
                Categories = _taskService.GetCategories()
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Tasks(TasksViewModel viewModel)
        {
            var userId = User.GetUserId();

            var tasks = _taskService.Get(userId,
                viewModel.FilterTasks.IsExecuted,
                viewModel.FilterTasks.CategoryId,
                viewModel.FilterTasks.Title);

            return PartialView("_TasksTable", tasks);
        }
        public IActionResult Task(int id = 0)
        {
            var userId = User.GetUserId();

            var task = id == 0 ?
                new ToDoList.Core.Models.Domains.Task { Id = 0, UserId = userId, Term = DateTime.Today } : _taskService.Get(id, userId);

            var vm = new TaskViewModel()
            {
                Task = task,
                Heading = id == 0 ?
                "Dodawanie nowego zadania" : "Edytowanie nowego zadania",
                Categories = _taskService.GetCategories()
            };
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Task(ToDoList.Core.Models.Domains.Task task)
        {
            var userId = User.GetUserId();
            task.UserId = userId;

            if (!ModelState.IsValid)
            {
                var vm = new TaskViewModel()
                {
                    Task = task,
                    Heading = task.Id == 0 ?
                "Dodawanie nowego zadania" : "Edytowanie nowego zadania",
                    Categories = _taskService.GetCategories()
                };
                return View("Task", vm);
            }

            if (task.Id == 0)
            {
                _taskService.Add(task);
            }
            else
            {
                _taskService.Update(task);

            }

            return RedirectToAction("Tasks");
        }

        private IActionResult Tasks1()
        {
            var userId = User.GetUserId();
            var vm = new TasksViewModel
            {
                FilterTasks = new ToDoList.Core.Models.FilterTasks(),
                Tasks = _taskService.Get(userId),
                Categories = _taskService.GetCategories()
            };
            return View(vm);
        }

        private IActionResult Task1(int id = 0)
        {
            var userId = User.GetUserId();

            var task = id == 0 ?
                new ToDoList.Core.Models.Domains.Task { Id = 0, UserId = userId, Term = DateTime.Today } : _taskService.Get(id, userId);

            var vm = new TaskViewModel()
            {
                Task = task,
                Heading = id == 0 ?
                "Dodawanie nowego zadania" : "Edytowanie nowego zadania",
                Categories = _taskService.GetCategories()
            };
            return View(vm);
        }

        [HttpPost]
        private IActionResult Delete1(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _taskService.Finish(id, userId);
            }
            catch (Exception ex)
            {
                //logowanie
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true });
        }

        [HttpPost]
        private IActionResult Finish1(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _taskService.Finish(id, userId);
            }
            catch (Exception ex)
            {
                //logowanie
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _taskService.Finish(id, userId);
            }
            catch (Exception ex)
            {
                //logowanie
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult Finish(int id)
        {
            try
            {
                var userId = User.GetUserId();
                _taskService.Finish(id, userId);
            }
            catch (Exception ex)
            {
                //logowanie
                return Json(new { success = false, message = ex.Message });
            }
            return Json(new { success = true });
        }



    }

}

