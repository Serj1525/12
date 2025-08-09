using TiskTask.TelegramBot;

Console.WriteLine("Запуск Telegram-бота...");

var cts = new CancellationTokenSource();

// 🔐 Замени на свой токен
const string BotToken = "8409911557:AAEGd_VCn7cxYmaH3OscEkVwM8QWYuBO4RA";

var botService = new TelegramBot(BotToken);

// Запускаем бота
await botService.StartAsync(cts.Token);

Console.WriteLine("Бот остановлен.");