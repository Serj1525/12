using System.Collections.Generic;
using System.Linq;

namespace TelegramBot
{
    public class TaskManager
    {
        private readonly List<TaskItem> _tasks;

        public TaskManager()
        {
            _tasks = new List<TaskItem>
            {
                new TaskItem { Id = 1, Title = "Задача 1", Description = "Взять пиво из холодильника" },
                new TaskItem { Id = 2, Title = "Задача 2", Description = "Открыть крышку" },
                new TaskItem { Id = 3, Title = "Задача 3", Description = "Сесть на стул" },
                new TaskItem { Id = 4, Title = "Задача 4", Description = "Пить пиво" },
                new TaskItem { Id = 5, Title = "Задача 5", Description = "Хорошооо..." }
            };
        }
        public IEnumerable<TaskItem> GetAllTasks()
        {
            return _tasks;
        }
    }

    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}