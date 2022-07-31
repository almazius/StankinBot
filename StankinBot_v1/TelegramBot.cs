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

namespace StankinBot_v1
{
    internal class TelegramBot
    {
        public static Dictionary<long, States> State = new Dictionary<long, States>();
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var chatId = update.Message.Chat.Id;
            State.TryAdd(chatId, States.Start); // создание состояния для конкретого чата
            if (update.Message.Text == "/start")
            {
                State[chatId] = States.Start;
            }
            switch (State[chatId])
            {
                case States.Start:
                    await Start(botClient, chatId);
                    break;
                case States.Home:
                    await Home(botClient, chatId);
                    break;
                case States.SearchLesson:
                    await SearchLesson(botClient, update, chatId);
                    break;
                case States.SearchNachert:
                    await SearchNachert(botClient, update, chatId);
                    break;
                case States.SearchNachertMetr:
                    await SearchNachertMetr(botClient, update, chatId);
                    break;
                case States.SearchNachertBodyWindow:
                    break;
                case States.SearchOP:
                    await SearchOP(botClient, update, chatId);
                    break;
                case States.SearchOPLab:
                    await SearchOPLab(botClient, update, chatId);
                    break;
                case States.SearchOPLabPlus:
                    await SearchOpLabPlus(botClient, update, chatId);
                    break;
                case States.SearchOPBlock:
                    // empty
                    break;
                case States.SearchOOP:
                    await SearchOOP(botClient, update, chatId);
                    break;
                case States.SearchOOPLab:
                    await SearchOOPLab(botClient, update, chatId);
                    break;
                case States.SearchOOPLabPlus:
                    await SearchOOpLabPlus(botClient, update, chatId);
                    break;
                default:
                    await botClient.SendTextMessageAsync(chatId, "Неизвестная ошибка!");
                    break;
            }
        }

        #region Tasks
        private static async Task Start(ITelegramBotClient botClient, long chatId)
        {

            State[chatId] = States.Home;
            await botClient.SendTextMessageAsync(chatId, "Привет!\n Рад приветсвтовать! Здесь ты всегда можешь запростить помощь по таким предметам как:" +
                ("\n Начертательная геометрия\n Основые программирования\n Объектно-ориентированное программирование\n ТЕХНИЧЕСКИЕ СРЕДСТВА ИНФОРМАЦИОННЫХ СИСТЕМ").ToUpper(),
                replyMarkup: KeyBoards.replyKeyboardHome);
            State[chatId] = States.SearchLesson;
        }

        private static async Task Home(ITelegramBotClient botClient, long chatId)
        {
            await botClient.SendTextMessageAsync(chatId, "Выберите необходимый предмет:" +
                ("\n Начертательная геометрия\n Основые программирования\n Объектно - ориентированное программирование\n ТЕХНИЧЕСКИЕ СРЕДСТВА ИНФОРМАЦИОННЫХ СИСТЕМ").ToUpper(),
                replyMarkup: KeyBoards.replyKeyboardHome);
            State[chatId] = States.SearchLesson;
        }

        private static async Task SearchLesson(ITelegramBotClient botClient, Update update, long chatId)
        {
            if (update.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text && (update.Message.Text.ToLower() == "начертательная геометрия"))
            {
                State[chatId] = States.SearchNachert;
                await botClient.SendTextMessageAsync(chatId, "Что конкретно Вас интересует?" +
             "\n\t1) Метрические задачи\n\t2) Тело с окном\n\t3) 4 задачки на А4\n\t4) Помощь на контрольной работе(?)",
            replyMarkup: KeyBoards.replyKeyboardNachert);
            }
            else if (update.Message.Text.ToLower() == "основые программирования")
            {
                State[chatId] = States.SearchOP;
                await botClient.SendTextMessageAsync(chatId, "Что конкретно Вас интересует?" +
            "\n\t1) Лабораторная работа\n\t2) Лабораторная работа+Отчёт+Блоксхема\n\t3) Блоксхема по вашему коду(?)", 
            replyMarkup: KeyBoards.replySearchKeybord);
            }
            else if (update.Message.Text.ToLower() == "объектно - ориентированное программирование")
            {
                State[chatId] = States.SearchOOP;
                await botClient.SendTextMessageAsync(chatId, "Что конкретно Вас интересует?" +
            "\n\t1) Лабораторная работа\n\t2) Лабораторная работа+Отчёт+Блоксхема\n\t3) Блоксхема по вашему коду(?)",
            replyMarkup: KeyBoards.replySearchKeybord);
            }
            else if (update.Message.Text.ToLower() == "технические средства информационных систем")
            {
                //
            }
            else if (update.Message.Text == "Назад")
            {
                State[chatId] = State[chatId] - 1;
            }
        }

        private static async Task SearchNachert(ITelegramBotClient botClient, Update update, long chatId)
        {
            switch (update.Message.Text.ToLower())        // Проверка условия: что интересует
            {
                case "метрические задачи":
                    State[chatId] = States.SearchNachertMetr;
                    using (var stream = System.IO.File.OpenRead("D:\\DEV\\StankinBot_v1\\StankinBot_v1\\image/Metrichki_variants.jpg"))
                    {
                        InputOnlineFile input = new InputOnlineFile(stream);
                        await botClient.SendPhotoAsync(chatId, stream, "Сверьте точки и напишите ваш вариант.");
                    }
                    break;
                case "тело с окном":
                    //
                    break;
                case "4 задачки на а4":
                    //
                    break;
                case "помощь на контрольной работе(?)":
                    //
                    break;
                case "назад":
                    State[chatId] = State[chatId] - 1;
                    break;
                default:
                    await botClient.SendTextMessageAsync(chatId, "Ошибка, пробуй снова!");
                    break;
            }
        }

        private static async Task SearchNachertMetr(ITelegramBotClient botClient, Update update, long chatId)
        {
            if (update.Message.Text == "Назад")
            {
                State[chatId] = State[chatId] - 1;
            }
            else if (Convert.ToInt32(update.Message.Text) > 0 && Convert.ToInt32(update.Message.Text) < 33)
            {
                using (var stream = System.IO.File.OpenRead($"D:\\DEV\\StankinBot_v1\\StankinBot_v1\\image\\variant{update.Message.Text}.jpg"))
                {
                    InputOnlineFile input = new InputOnlineFile(stream);
                    await botClient.SendPhotoAsync(chatId, stream);
                }
                State[chatId] = States.Home;
            }
            else
            {
                await botClient.SendTextMessageAsync(chatId, "Вы ошиблись, попробуйте еще раз.");
            }
        }

        private static async Task SearchOP(ITelegramBotClient botClient, Update update, long chatId)
        {
            switch (update.Message.Text.ToLower())
            {
                case "лабораторная работа":
                    State[chatId] = States.SearchOPLab;
                    await botClient.SendTextMessageAsync(chatId, "Введите номер лабораторной и номер задания (1-13):\n" +
                    "Сверка с файлом: https://docs.google.com/document/d/1f5l2JHLtRz8EIwaEBQKja41mwLD4E19x/edit");
                    break;
                case "лабораторная работа+отчёт+блоксхема":
                    State[chatId] = States.SearchOPLabPlus;
                    await botClient.SendTextMessageAsync(chatId, "Введите номер лабораторной и номер задания (1-13):\n" +
                    "Сверка с файлом: https://docs.google.com/document/d/1f5l2JHLtRz8EIwaEBQKja41mwLD4E19x/edit");
                    break;
                case "блоксхема по вашему коду":
                    State[chatId] = States.SearchOPBlock;
                    await botClient.SendTextMessageAsync(chatId, "Пока в разработке"); // ???
                    break;
                case "назад":
                    State[chatId] = States.SearchLesson;
                    break;
                default:
                    await botClient.SendTextMessageAsync(chatId, "Ошибка, пробуй снова!");
                    break;
            }
        }

        /// <summary>
        /// Поиск номера лабы и номера задания с последующей отправкой
        /// </summary>
        /// <param name="botClient">botClient</param>
        /// <param name="update"></param>
        /// <param name="chatId"></param>
        /// <returns></returns>
        private static async Task SearchOPLab(ITelegramBotClient botClient, Update update, long chatId)
        {
            int[] arr = GetNumbers(update.Message.Text);
            await botClient.SendTextMessageAsync(chatId, $"Num lab - {arr[0]}, num Task - {arr[1]}");
        }

        private static async Task SearchOpLabPlus(ITelegramBotClient botClient, Update update, long chatId)
        {
            int[] arr = GetNumbers(update.Message.Text);
            //await botClient.SendDocumentAsync(chatId, )
            await botClient.SendTextMessageAsync(chatId, $"Тут будет отправка архива, но я хочу спать и сделаю ее позже.\n{arr.GetValue(0)} - {arr.GetValue(1)}\n");
        }

        private static async Task SearchOOP(ITelegramBotClient botClient, Update update, long chatId)
        {
            switch (update.Message.Text.ToLower())
            {
                case "лабораторная работа":
                    State[chatId] = States.SearchOOPLab;
                    await botClient.SendTextMessageAsync(chatId, "Введите номер лабораторной и номер задания (2-13):\n" +
                    "Сверка с файлом: https://docs.google.com/document/d/1qLlPyqKplHw5P3fPJUZ6qqSsHRHnCQiZ49SmAYVRRaI/edit");
                    break;
                case "лабораторная работа+отчёт+блоксхема":
                    State[chatId] = States.SearchOOPLabPlus;
                    await botClient.SendTextMessageAsync(chatId, "Введите номер лабораторной и номер задания (1-13):\n" +
                    "Сверка с файлом: https://docs.google.com/document/d/1qLlPyqKplHw5P3fPJUZ6qqSsHRHnCQiZ49SmAYVRRaI/edit");
                    break;
                case "блоксхема по вашему коду":
                    State[chatId] = States.SearchOOPBlock;
                    await botClient.SendTextMessageAsync(chatId, "Пока в разработке"); // ???
                    break;
                case "назад":
                    State[chatId] = States.SearchLesson;
                    break;
                default:
                    await botClient.SendTextMessageAsync(chatId, "Ошибка, пробуй снова!");
                    break;
            }
        }

        private static async Task SearchOOPLab(ITelegramBotClient botClient, Update update, long chatId)
        {
            int[] arr = GetNumbers(update.Message.Text);
            await botClient.SendTextMessageAsync(chatId, $"Num lab - {arr[0]}, num Task - {arr[1]}");
        }

        private static async Task SearchOOpLabPlus(ITelegramBotClient botClient, Update update, long chatId)
        {
            int[] arr = GetNumbers(update.Message.Text);
            //await botClient.SendDocumentAsync(chatId, )
            await botClient.SendTextMessageAsync(chatId, $"Тут будет отправка архива, но я хочу спать и сделаю ее позже.\n{arr.GetValue(0)} - {arr.GetValue(1)}\n");
        }


        #endregion



        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }


        private static int[] GetNumbers(string s)
        {
            int[] numbers = new int[2];
            char[] separators = new char[] { ' ', '-', '\\', '/' };
            string[] temp = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            numbers[0] = Convert.ToInt32(temp[0]);
            numbers[1] =  Convert.ToInt32(temp[1]);
            return numbers;
        }
    }

    public enum States
    {
        Start, Home,SearchLesson ,SearchNachert, SearchNachertMetr, SearchNachertBodyWindow, SearchOP, SearchOPLab, SearchOPLabPlus, SearchOPBlock,
        SearchOOP, SearchOOPLab, SearchOOPLabPlus, SearchOOPBlock,
    }
}
