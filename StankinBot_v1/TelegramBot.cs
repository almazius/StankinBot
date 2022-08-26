using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;
using System.Net;
using System.IO;
using System.IO.Compression;

namespace StankinBot_v1
{
    internal class TelegramBot
    {
        readonly private static Random rnd = new (); // DELETE!!!
        private static long count = rnd.Next();

        public static Dictionary<long, States> State = new ();
        
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            
            long chatId = 0;
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                 chatId = update.Message.Chat.Id;
            }
            else if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                 chatId = update.CallbackQuery.Message.Chat.Id;
            }
            // Нужна обработка на типы помимо текста и каллбеков
            State.TryAdd(chatId, States.Start); // создание состояния для конкретого чата

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                await HandleMessage(botClient, chatId, update);
            }
            else if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                await HandleCallback(botClient, chatId, update);
            }
            
        }

        #region Tasks

        private static async Task HandleCallback(ITelegramBotClient botClient, long chatId, Update update)
        {
            if(update.CallbackQuery.Data.StartsWith("nachrt_metr_"))
            {
                await SelectNachertMetrich(botClient, update, chatId);
            }
            switch (update.CallbackQuery.Data)
            {
                case "nachert":
                    await SearchNachert(botClient, update, chatId);
                    break;
                case "metrich":
                    await SearchNachertMetr(botClient, update, chatId);
                    break;
                case "any_nachert":
                    await SearchNachertAny(botClient, update, chatId);
                    State[chatId] = States.SearchNachertAnyWork;
                    break;
                case "control_work":
                    await botClient.SendTextMessageAsync(chatId, "Пока в разработке.");
                    break;
                case "back":
                    await botClient.EditMessageTextAsync(chatId, update.CallbackQuery.Message.MessageId,
                "Выберите интересующий вас предмет", replyMarkup: KeyBoards.lessionSelect);
                    State[chatId] = States.Home;
                    break;
                case "back_nachert":
                    await SearchNachert(botClient, update, chatId);
                    break;
                case "proga":
                    await SearchNachertAny(botClient, update, chatId);
                    State[chatId] = States.SearchProga;
                    break;
                case "tsis":
                    await SearchNachertAny(botClient, update, chatId);
                    State[chatId] = States.SearchTSIS;
                    break;
                case "success_order":
                    if (ArchiveAndSend(chatId))
                    {
                        await botClient.SendTextMessageAsync(chatId, "Ваш заказ принят к выполнению! Ожидайте.");
                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(chatId, "Произошла какая-то ошиибка! Попробуйте снова.");
                    }
                    State[chatId] = States.Home;
                    // тут будут обработка заказа
                    break;
                default:
                    break;
            }
        }

        private static async Task HandleMessage(ITelegramBotClient botClient, long chatId, Update update)
        {
            if (update.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                if (update.Message.Text == "/start")
                {
                    State[chatId] = States.Start;
                }
                else if (update.Message.Text.ToLower() == "главная")
                {
                    State[chatId] = States.Home;
                }
                switch (State[chatId])
                {
                    case States.Start:
                        await Start(botClient, chatId); //
                        break;
                    case States.Home:
                        await Home(botClient, chatId); //
                        break;
                    case States.CategorySelect: //
                        await CategorySelect(botClient, update, chatId);
                        break;
                    case States.SearchLesson:
                        //await SearchLesson(botClient, update, chatId);
                        break;
                    case States.SearchNachert:
                        await SearchNachert(botClient, update, chatId);
                        break;
                    case States.SearchNachertMetr:
                        await SearchNachertMetr(botClient, update, chatId);
                        break;
                    case States.SearchNachertAnyWork:
                        await CheckQuestNachert(botClient, update, chatId);
                        break;
                    case States.SearchProga:
                        await CheckQuestNachert(botClient, update, chatId);
                        break;
                    default:
                        await botClient.SendTextMessageAsync(chatId, "Неизвестная ошибка!");
                        break;
                }
            }
            else if(update.Message.Type == Telegram.Bot.Types.Enums.MessageType.Photo)
            {
                switch (State[chatId])
                {
                    case States.SearchNachertAnyWork:
                        await CheckQuestNachert(botClient, update, chatId);
                        break;
                    case States.SearchProga:
                        await CheckQuestNachert(botClient, update, chatId);
                        break;
                    case States.SearchTSIS:
                        await CheckQuestNachert(botClient, update, chatId);
                        break;
                    default:
                        await botClient.SendTextMessageAsync(chatId, "Классаня картинка, но я не понимаю её. Напиши текстом \nпжпжпжпж");
                        break;
                }
            }
            else if (update.Message.Type == Telegram.Bot.Types.Enums.MessageType.Sticker)
            {
                await botClient.SendTextMessageAsync(chatId, "Прекрасый стикер, но я не понимаю его. Напиши текстом \nпжпжпжпж");
            }
        }



        private static async Task SelectNachertMetrich(ITelegramBotClient botClient, Update update, long chatId) // pererabotat !!!
        {
            string variant;
            variant = update.CallbackQuery.Data[12..];
            if (System.IO.File.Exists(@$"C:\Users\boy20\Source\Repos\almazius\StankinBot\StankinBot_v1\image\variant{variant}.jpg"))
            {
                using (var stream = System.IO.File.OpenRead(@$"C:\Users\boy20\Source\Repos\almazius\StankinBot\StankinBot_v1\image\variant{variant}.jpg"))
                {
                    InputOnlineFile input = new (stream);
                    await botClient.SendPhotoAsync(chatId, input);
                }
            }
            else
            {
                // тут нужно сделать обработку заказа и информамирование продавцов
                await botClient.SendTextMessageAsync(chatId, "Ваш заказ успешно оформлен. Ожидайте получение заказа.");
            }
            CompletedOrder();
            await botClient.DeleteMessageAsync(chatId, update.CallbackQuery.Message.MessageId);
            State[chatId] = States.Home;
            await SearchLesson(botClient, chatId);
        }

        private static async Task Start(ITelegramBotClient botClient, long chatId)
        {

            // State[chatId] = States.Home;
            await botClient.SendTextMessageAsync(chatId, "Привет!\n Рад приветсвтовать! Здесь ты всегда можешь запростить помощь по таким предметам как:" +
                ("\n Начертательная геометрия\n Основые программирования\n Объектно-ориентированное программирование\n ТЕХНИЧЕСКИЕ СРЕДСТВА ИНФОРМАЦИОННЫХ СИСТЕМ").ToUpper(),
                replyMarkup: KeyBoards.replyKeyboardHome);
            await Home(botClient, chatId);
            //State[chatId] = States.CategorySelect;
        }

        private static async Task Home(ITelegramBotClient botClient, long chatId)
        {
            await botClient.SendTextMessageAsync(chatId, "Выберите действие:",
                replyMarkup: KeyBoards.replyKeyboardHome);
            State[chatId] = States.CategorySelect;
        }

        private static async Task CategorySelect(ITelegramBotClient botClient, Update update, long chatId)
        {
            if (update.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                switch (update.Message.Text.ToLower())
                {
                    case "купить":
                        await SearchLesson(botClient, chatId);
                        break;
                    case "профиль":
                        // ?
                        break;
                    case "бонус":
                        // ?
                        break;
                    default:
                        break;
                }
            }
            
        }


        private static async Task SearchLesson(ITelegramBotClient botClient, long chatId)
        {
            await botClient.SendTextMessageAsync(chatId, "Выберите интересующий вас предмет", replyMarkup: KeyBoards.lessionSelect);
        }

        private static async Task SearchNachert(ITelegramBotClient botClient, Update update, long chatId)
        {   
            await botClient.DeleteMessageAsync(chatId, update.CallbackQuery.Message.MessageId);
            await botClient.SendTextMessageAsync(chatId, "Выберите нужную вам задачу:", replyMarkup: KeyBoards.keyboardNachert);
        }

        private static async Task SearchNachertMetr(ITelegramBotClient botClient, Update update, long chatId) ///
        {
            await botClient.DeleteMessageAsync(chatId, update.CallbackQuery.Message.MessageId);
            //await botClient.EditMessageTextAsync(chatId, update.CallbackQuery.Message.MessageId, "Выберите номер вашего варианта", replyMarkup: KeyBoards.keyboardMetrich);
            using (var stream = System.IO.File.OpenRead(@"C:\Users\boy20\Source\Repos\almazius\StankinBot\StankinBot_v1\image\Metrichki_variants.jpg"))
            {
                InputOnlineFile input = new (stream);
                await botClient.SendPhotoAsync(chatId, input, "Выберите необходимый вариант", replyMarkup: KeyBoards.keyboardMetrich);
            }
            State[chatId] = States.SelectNachertMetrich;
        }

        private static async Task SearchNachertAny(ITelegramBotClient botClient, Update update, long chatId)
        {
            await botClient.EditMessageTextAsync(chatId, update.CallbackQuery.Message.MessageId,
                "Отправьте фотографии и опишите задание и нажимите кнопку \"готово\"", replyMarkup: KeyBoards.keyboardNachertAnyWork);
            Directory.CreateDirectory($@"C:\Users\boy20\Source\Repos\almazius\StankinBot\StankinBot_v1\Task_{chatId}"); 
        }

        private static async Task CheckQuestNachert(ITelegramBotClient botClient, Update update, long chatId)
        {
            var path = $@"C:\Users\boy20\Source\Repos\almazius\StankinBot\StankinBot_v1\Task_{chatId}\{update.Message.MessageId}";
            if (update.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                path += ".txt"; 
                var fileText = System.IO.File.CreateText(path);
                await fileText.WriteAsync(update.Message.Text);
                fileText.Close();
            }
            else if(update.Message.Type == Telegram.Bot.Types.Enums.MessageType.Photo)
            {
                path += ".jpg";
                var file = await botClient.GetFileAsync(update.Message.Photo[update.Message.Photo.Length - 1].FileId);
                using (var saveImageStream = new FileStream(path, FileMode.Create))
                {
                    await botClient.DownloadFileAsync(file.FilePath, saveImageStream);
                }
                if (!String.IsNullOrEmpty(update.Message.Caption))
                {
                    path += "_Caption.txt";
                    var fileText = System.IO.File.CreateText(path);
                    await fileText.WriteAsync(update.Message.Caption);
                    fileText.Close();
                }
            }
            else
            {
                await botClient.SendTextMessageAsync(chatId, "Я понимаю только фотографии или текст! 😾");
            }

        }

        #endregion



        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }

        private static bool ArchiveAndSend(long chatId)
        {
            string nameZip = String.Empty;
            switch (State[chatId])
            {
                
                case States.SearchNachertAnyWork:
                    nameZip += "Nachert";
                    break;
                case States.SearchProga:
                    nameZip += "Proga";
                    break;
                case States.SearchTSIS:
                    nameZip += "TSIS";
                    break;
                default:
                    break;
            }
            nameZip = nameZip + "_" + chatId.ToString() + "_" + count.ToString() + ".zip";
            if (Directory.EnumerateFiles(@$"C:\Users\boy20\Source\Repos\almazius\StankinBot\StankinBot_v1\Task_{chatId}",
                "*.*", SearchOption.AllDirectories).Any())
            {
                ZipFile.CreateFromDirectory(@$"C:\Users\boy20\Source\Repos\almazius\StankinBot\StankinBot_v1\Task_{chatId}",
                nameZip);
                // tyt otpravka na server (drygoi bot)
                Directory.Delete(@$"C:\Users\boy20\Source\Repos\almazius\StankinBot\StankinBot_v1\Task_{chatId}", true);
                CompletedOrder();
                return true;
            }
            Directory.Delete(@$"C:\Users\boy20\Source\Repos\almazius\StankinBot\StankinBot_v1\Task_{chatId}", true);
            return false;
        }

        private static void CompletedOrder()
        {
            count++;
        }
    }

    public enum States
    {
        Start, Home, CategorySelect,SearchLesson ,SearchNachert, SearchNachertMetr, SearchNachertAnyWork, SearchProga, SearchOPLab, SearchOPLabPlus, SearchOPBlock,
        SearchOOP, SearchOOPLab, SearchOOPLabPlus, SearchOOPBlock,
        SelectNachertMetrich,
        SearchTSIS,
    }
}
