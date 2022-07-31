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
            new KeyboardButton[] { "НАЧЕРТАТЕЛЬНАЯ ГЕОМЕТРИЯ"},
            new KeyboardButton[] { "ОСНОВЫЕ ПРОГРАММИРОВАНИЯ" },
            new KeyboardButton[] { "ОБЪЕКТНО-ОРИЕНТИРОВАННОЕ ПРОГРАММИРОВАНИЕ" },
            new KeyboardButton[] { "ТЕХНИЧЕСКИЕ СРЕДСТВА ИНФОРМАЦИОННЫХ СИСТЕМ" },
        })
        {
            ResizeKeyboard = true
        };

        public static ReplyKeyboardMarkup replyKeyboardNachert = new(new[]
{
            new KeyboardButton[] { "1) Метрические задачи"},
            new KeyboardButton[] { "2) Тело с окном" },
            new KeyboardButton[] { "3) 4 задачки на А4" },
            new KeyboardButton[] { " 4) Помощь на контрольной работе(?)" },
            new KeyboardButton[] { "Назад" },
        })
        {
            ResizeKeyboard = true
        };

        #endregion
    }
}
