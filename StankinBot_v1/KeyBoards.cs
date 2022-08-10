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
            new [] { InlineKeyboardButton.WithCallbackData(text: "Метрические задачи", callbackData: "metrich"),
                InlineKeyboardButton.WithCallbackData(text: "Тело с окном", callbackData: "telo_s_oknom")},
            new [] { InlineKeyboardButton.WithCallbackData(text: "4 задачи на А4", callbackData: "on_A4"),
                InlineKeyboardButton.WithCallbackData(text: "Контрольная работа", callbackData: "control_work")},
            new [] { InlineKeyboardButton.WithCallbackData(text: "Другое", callbackData: "any_nachert")},
            new [] { InlineKeyboardButton.WithCallbackData(text: "Назад", callbackData: "back")},
            //new KeyboardButton[] { "4 задачки на А4", "Контрольная работа" },
            //new KeyboardButton[] { "Назад" },
        });

        public static ReplyKeyboardMarkup replySearchKeybord = new(new[]
        {
            new KeyboardButton[] { "Лабораторная работа"},
            new KeyboardButton[] { "Лабораторная работа+Отчёт+Блоксхема" },
            new KeyboardButton[] { "Блоксхема по вашему коду" },
            new KeyboardButton[] { "Назад" },
        })
        {
            ResizeKeyboard = true
        };

        public static ReplyKeyboardMarkup replyEmptyKeybord = new(new[]
        {
            new KeyboardButton[] {""},
        })
        {
            ResizeKeyboard = true
        };

        public static InlineKeyboardMarkup lessionSelect = new (new[]
        {
            new [] {InlineKeyboardButton.WithCallbackData(text: "Начертательная геометрия", callbackData: "nachert") },
            new [] {InlineKeyboardButton.WithCallbackData(text: "Программирование", callbackData: "proga") },
            new [] {InlineKeyboardButton.WithCallbackData(text: "Технические средства информационных систем", callbackData: "tsis") },

        });


        #endregion
    }
}
