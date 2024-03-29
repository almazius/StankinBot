﻿using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;

namespace StankinBot_v1
{
    internal class Program
    {
        static ITelegramBotClient bot; // созднание клиента бота

        static void Main(string[] args)
        {
           bot = new TelegramBotClient(token);

           var cts = new CancellationTokenSource();

            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                TelegramBot.HandleUpdateAsync,
                TelegramBot.HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );

            Console.ReadLine();
        }
    }
}
