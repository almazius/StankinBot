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
    internal class KeyBoards
    {
        #region Keyboards
        public static ReplyKeyboardMarkup replyKeyboardHome = new(new[]
        {
            new KeyboardButton[] { "Купить", "Профиль"},
            new KeyboardButton[] { "БОНУС" },
            new KeyboardButton[] { "Главная", "Назад" },
        })
        {
            ResizeKeyboard = true
        };

        public static InlineKeyboardMarkup keyboardNachert = new(new[]
        {
            new [] { InlineKeyboardButton.WithCallbackData(text: "Метрические задачи", callbackData: "metrich") },
            new [] { InlineKeyboardButton.WithCallbackData(text: "Контрольная работа", callbackData: "control_work")},
            new [] { InlineKeyboardButton.WithCallbackData(text: "Другое", callbackData: "any_nachert")},
            new [] { InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "back")},
        });

        public static InlineKeyboardMarkup keyboardNachertAnyWork = new(new[]
        {
            new [] { InlineKeyboardButton.WithCallbackData(text: "Готово ✅", callbackData: "success_nachert_any") },
            new [] { InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "back")},
        });

        public static InlineKeyboardMarkup keyboardMetrich = new(new[]
        {
            new[] {InlineKeyboardButton.WithCallbackData(text: "1️⃣", callbackData: "nachrt_metr_1"),
                InlineKeyboardButton.WithCallbackData(text: "2️⃣", callbackData: "nachrt_metr_2"),
                InlineKeyboardButton.WithCallbackData(text: "3️⃣", callbackData: "nachrt_metr_3"),
                InlineKeyboardButton.WithCallbackData(text: "4️⃣", callbackData: "nachrt_metr_4"),
                InlineKeyboardButton.WithCallbackData(text: "5️⃣", callbackData: "nachrt_metr_5") },
            new[] {InlineKeyboardButton.WithCallbackData(text: "6️⃣", callbackData: "nachrt_metr_6"),
                InlineKeyboardButton.WithCallbackData(text: "7️⃣", callbackData: "nachrt_metr_7"),
                InlineKeyboardButton.WithCallbackData(text: "8️⃣", callbackData: "nachrt_metr_8"),
                InlineKeyboardButton.WithCallbackData(text: "9️⃣", callbackData: "nachrt_metr_9"),
                InlineKeyboardButton.WithCallbackData(text: "1️⃣0️⃣", callbackData: "nachrt_metr_10") },

            new[] {InlineKeyboardButton.WithCallbackData(text: "1️⃣1️⃣", callbackData: "nachrt_metr_11"),
                InlineKeyboardButton.WithCallbackData(text: "1️⃣2️⃣", callbackData: "nachrt_metr_12"),
                InlineKeyboardButton.WithCallbackData(text: "1️⃣3️⃣", callbackData: "nachrt_metr_13"),
                InlineKeyboardButton.WithCallbackData(text: "1️⃣4️⃣", callbackData: "nachrt_metr_14"),
                InlineKeyboardButton.WithCallbackData(text: "1️⃣5️⃣", callbackData: "nachrt_metr_15") },
            new[] {InlineKeyboardButton.WithCallbackData(text: "1️⃣6️⃣", callbackData: "nachrt_metr_16"),
                InlineKeyboardButton.WithCallbackData(text: "1️⃣7️⃣", callbackData: "nachrt_metr_17"),
                InlineKeyboardButton.WithCallbackData(text: "1️⃣8️⃣", callbackData: "nachrt_metr_18"),
                InlineKeyboardButton.WithCallbackData(text: "1️⃣9️⃣", callbackData: "nachrt_metr_19"),
                InlineKeyboardButton.WithCallbackData(text: "2️⃣0️⃣", callbackData: "nachrt_metr_20") },

            new[] {InlineKeyboardButton.WithCallbackData(text: "2️⃣1️⃣", callbackData: "nachrt_metr_21"),
                InlineKeyboardButton.WithCallbackData(text: "2️⃣2️⃣", callbackData: "nachrt_metr_22"),
                InlineKeyboardButton.WithCallbackData(text: "2️⃣3️⃣", callbackData: "nachrt_metr_23"),
                InlineKeyboardButton.WithCallbackData(text: "2️⃣4️⃣", callbackData: "nachrt_metr_24"),
                 },
            new[] { InlineKeyboardButton.WithCallbackData(text: "2️⃣5️⃣", callbackData: "nachrt_metr_25"),
                InlineKeyboardButton.WithCallbackData(text: "2️⃣6️⃣", callbackData: "nachrt_metr_26"),
                InlineKeyboardButton.WithCallbackData(text: "2️⃣7️⃣", callbackData: "nachrt_metr_27"),
                InlineKeyboardButton.WithCallbackData(text: "2️⃣8️⃣", callbackData: "nachrt_metr_28"),
                 },

            new[] {InlineKeyboardButton.WithCallbackData(text: "2️⃣9️⃣", callbackData: "nachrt_metr_29"),
                InlineKeyboardButton.WithCallbackData(text: "3️⃣0️⃣", callbackData: "nachrt_metr_30"),
                InlineKeyboardButton.WithCallbackData(text: "3️⃣1️⃣", callbackData: "nachrt_metr_31"),
                InlineKeyboardButton.WithCallbackData(text: "3️⃣2️⃣", callbackData: "nachrt_metr_32"),
                 },

            new[] {InlineKeyboardButton.WithCallbackData(text: "У меня другие точки", callbackData: "any_task_metich"),
                InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "back_nachert")},
        });



        public static InlineKeyboardMarkup lessionSelect = new (new[]
        {
            new [] {InlineKeyboardButton.WithCallbackData(text: "Начертательная геометрия", callbackData: "nachert") },
            new [] {InlineKeyboardButton.WithCallbackData(text: "Программирование", callbackData: "proga") },
            new [] {InlineKeyboardButton.WithCallbackData(text: "Технические средства информационных систем", callbackData: "tsis") },

        });


        #endregion
    }
}
