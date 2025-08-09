using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot;

namespace TiskTask.TelegramBot;

public class CommandManager
{
    // Класс для представления задачи
    private static readonly TaskManager _taskManager = new TaskManager();
        
    /// <summary>
    /// Метод для обработки команды /all
    /// </summary>
    /// <param name="botClient">TG Bot API клиента.</param>
    /// <param name="chatId">Идентификатор чата.</param>
    /// <param name="cancellationToken">Прерывание запроса.</param>
    public static async Task TakeAllTasksCommand(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
    {
        var tasks = _taskManager.GetAllTasks();

        if (!tasks.Any())
        {
            await botClient.SendMessage(
                chatId: chatId,
                text: "Опаньки, а задач-то нет.",
                cancellationToken: cancellationToken
            );
            return;
        }
        foreach (var task in tasks)
        {
            string message = $"ID: {task.Id}\nЗаголовок: {task.Title}\nОписание: {task.Description}";
            //Добавила кнопку редактирования к описанию задачи
            var keyboard = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup(new[]{
                new [] // first row
                {
                    InlineKeyboardButton.WithCallbackData("Редактировать",task.Id.ToString())
                }
                });
            await botClient.SendMessage(
                chatId: chatId,
                text: message,
                replyMarkup: keyboard,
                cancellationToken: cancellationToken
            );
        }
    }
    /// <summary>
    /// Обрабатывает команду /create, отправляет запрос пользователю.
    /// </summary>
    /// <param name="botClient">Идентификатор чата.</param>
    /// <param name="update"></param>
    /// <returns></returns>
    public static async Task RequestTaskDescriptionAsync(ITelegramBotClient botClient, Update update)
    {
        var newChatId = update.Message.Chat.Id;
        var message = update.Message;
        if (message.Text == "/create")
        {
            await botClient.SendMessage(
            chatId: newChatId,
            text: "Введите данные задачи в формате: Заголовок: <текст>; Описание: <текст>");
        }
    }

    /// <summary>
    /// Обрабатывает команду /create,распарсивает ввод пользователя, вызывает метод создания задачи. 
    /// </summary>
    /// <param name="botClient">Идентификатор чата.</param>
    /// <param name="update"></param>
    /// <returns></returns>
    public static async Task CreateTaskAsync(ITelegramBotClient botClient, Update update)
    {
        var newChatId = update.Message.Chat.Id;
        var message = update.Message;
        try
        {
            string taskTitle;
            string taskDiscription;
            string userText = message.Text.ToString();
            string[] taskText = userText.Split(";");
            string[] title = taskText[0].Split(":");
            string[] description = taskText[1].Split(":");
            if ((title[0] == "Заголовок") && (description[0] == " Описание"))
            {
                taskTitle = title[1];
                taskDiscription = description[1];
                Dictionary<string,string> taskData = new Dictionary<string,string>();
                taskData.Add("taskTitle", taskTitle);
                taskData.Add("taskDiscription", taskDiscription);
                
                /*TaskManager.CreateTask(taskTitle,taskDiscription);*/
            }
            else
            {
                await botClient.SendMessage(
                chatId: newChatId,
                text: "Неверный формат данных");
            }
        }
        catch (IndexOutOfRangeException)
        {
            await botClient.SendMessage(
            chatId: newChatId,
            text: "Неверный формат данных");
        }
    }
    /*internal static async Task TakeAllTasksCommand(ITelegramBotClient botClient, long chatId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }*/
}