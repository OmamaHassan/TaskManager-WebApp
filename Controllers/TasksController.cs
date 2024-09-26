using Azure.Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerWebApp.Data;
using TaskManagerWebApp.Models;

namespace TaskManagerWebApp.Controllers
{
    public class TasksController : Controller
    {
        private readonly AppDbContext appDbContext;
        public TasksController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var tasks = await appDbContext.TaskItems.ToListAsync();
            return View(tasks);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTaskViewModel addViewModel)
        {
            var taskItem = new TaskItem
            {
                TaskName = addViewModel.TaskName,
                Description = addViewModel.Description,
                DueDate = addViewModel.DueDate,
                IsCompleted = addViewModel.IsCompleted

            };
            await appDbContext.TaskItems.AddAsync(taskItem);
            await appDbContext.SaveChangesAsync();
            TempData["Message"] = $"Task '{taskItem.TaskName}' Added To The List!";

            return RedirectToAction("List");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var taskItem = await appDbContext.TaskItems.FindAsync(Id);

            return View(taskItem);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TaskItem viewModel)
        {
            var taskItem = await appDbContext.TaskItems.FindAsync(viewModel.Id);
            if (taskItem is not null)
            {
                taskItem.TaskName = viewModel.TaskName;
                taskItem.Description = viewModel.Description;
                taskItem.IsCompleted = viewModel.IsCompleted;
                taskItem.DueDate = taskItem.DueDate;

                await appDbContext.SaveChangesAsync();

                TempData["Message"] = $"Task '{taskItem.TaskName}' Edited!";

            }
            return RedirectToAction("List");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var taskItem = await appDbContext.TaskItems.FindAsync(Id);

            return View(taskItem);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TaskItem viewModel)
        {
            var taskItem = await appDbContext.TaskItems.FindAsync(viewModel.Id);
            if (taskItem is not null)
            {
                appDbContext.TaskItems.Remove(taskItem);
                await appDbContext.SaveChangesAsync();

                TempData["Message"] = $"Task '{taskItem.TaskName}' Deleted!";
            }
            return RedirectToAction("List");
        }


        [HttpGet]
        public async Task<IActionResult> ViewTask(int Id)
        {
            var taskItem = await appDbContext.TaskItems.FindAsync(Id);

            return View(taskItem);
        }

    }
}

