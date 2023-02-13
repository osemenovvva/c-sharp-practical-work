using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new TelegramBotClient("***");
            client.StartReceiving(Update, Error);
            Console.ReadLine();
        }

        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message;
            Console.WriteLine($"{message.From.Username}  | {message.Chat.Id}");
            
            if (message.Text == null)
            {
                await botClient.SendTextMessageAsync(message.Chat, "Please send me some text description");
                return;
            }
            
            if (message.Text.ToLower() == "/start")
            {
                await botClient.SendTextMessageAsync(message.Chat, 
                        "Hello, my friend! Write down what you want to see and i'll send you a picture of it!");
                return;
            }
            else
            {
                await botClient.SendTextMessageAsync(message.Chat, "OK! Please wait for it...");
                
                var imageDescription = message.Text;
                OpenAiImageGenerator openAiImageGenerator = OpenAiImageGenerator.GetInstance();
                var imageUrl = await openAiImageGenerator.CreateImage(imageDescription);
                
                await botClient.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: new InputFileUrl(imageUrl));
            }
        }

        private static Task Error(ITelegramBotClient botClient, Exception update, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}