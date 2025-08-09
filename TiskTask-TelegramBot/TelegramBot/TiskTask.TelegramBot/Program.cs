using Telegram.Bot;

namespace TelegramBot
{
    public static class CommandManager
    {
        // Класс для представления задачи
        private static readonly TaskManager _taskManager = new TaskManager();
        
        /// <summary>
        /// Метод для обработки команды /all
        /// </summary>
        /// <param name="botClient"></param>
        /// <param name="chatId"></param>
        /// <param name="cancellationToken"></param>
        public static async Task TakeAllTasksCommand(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
        {
            var tasks = _taskManager.GetAllTasks();

            if (!tasks.Any())
            {
                await botClient.SendMessage(
                    chatId: chatId,
                    text: "У вас нет задач.",
                    cancellationToken: cancellationToken
                );
                return;
            }

            foreach (var task in tasks)
            {
                string message = $"ID: {task.Id}\nЗаголовок: {task.Title}\nОписание: {task.Description}";
                await botClient.SendMessage(
                    chatId: chatId,
                    text: message,
                    cancellationToken: cancellationToken
                );
            }
        }
        /// <summary>
        /// Задача при вводе команды "создать задачу".
        /// </summary>
        /// <param name="botClient"></param>
        /// <param name="chatId"></param>
        /// <param name="cancellationToken"></param>
        public static async Task CreateCommand(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
        {
            await botClient.SendMessage(
                chatId: chatId,
                text: "Введите данные задачи в формате:\n" +
                      "Заголовок: <текст>; \n" +
                      "Описание: <текст>\n",
                cancellationToken: cancellationToken
            );
        }
        
        public static void ParseTaskData(string input, out string title, out string description)
        {
            title = string.Empty;
            description = string.Empty;

            var parts = input.Split(';');
            if (parts.Length >= 2)
            {
                title = parts[0].Split(':').ElementAtOrDefault(1)?.Trim();
                description = parts[1].Split(':').ElementAtOrDefault(1)?.Trim();
            }
        }
        public static async Task CreateTask(string title, string description)
        {
            // Логика создания задачи
            // В этом методе можно вызвать менеджер задач для создания задачи с заголовком и описанием
            Console.WriteLine($"Создана задача с заголовком: {title} и описанием: {description}");
        }
    }
}