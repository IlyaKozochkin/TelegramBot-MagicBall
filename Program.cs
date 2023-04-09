using System;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

class Program
{
    static async Task Main()
    {
        var botClient = new TelegramBotClient("");
        CancellationTokenSource cts = new CancellationTokenSource();
        CancellationToken ct = cts.Token;

        ReceiverOptions receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { }
        };

        botClient.StartReceiving(
            HandleUpdatesAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken: cts.Token);



        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        var me = await botClient.GetMeAsync(); //получаем в переменную me данные о нашем боте
        Console.WriteLine($"Бот начал работу | id бота: {me.Id}");
        Console.ReadLine();
        cts.Cancel(); //вызываем отмену токена
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

    async static Task HandleUpdatesAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
            if (update.Type == UpdateType.Message && update?.Message?.Text != null)  //если получаем message, то идем в следующий метод
            {
                DateTime timeStart = DateTime.Now;
                await HandleMessage(botClient, update.Message);
                DateTime timeEnd = DateTime.Now;
                TimeSpan result = timeEnd - timeStart;
                Console.WriteLine(result);
            }
        }
        catch (Exception)
        {
            Console.WriteLine("БОТ чуть не сломался");
        }
    }

    async static Task HandleMessage(ITelegramBotClient botClient, Message message) //метод в котором будем обрабатывать текст сообщений
    {
        try
        {

            string[] answers = { "CAACAgIAAxkBAAEGUX9jZlrlWQLost7UUYWzLNTE4A1fzQAC5R4AAlICMEsy_e0dRaDPXisE", "CAACAgIAAxkBAAEGUYFjZlrrdbpICdVqVorVnHBilVaEfQACeyIAAkd4MUszgS0h_QG8NCsE",
                "CAACAgIAAxkBAAEGUYNjZlrwX6Q0LZuedcgT8NKQVgpbAAPyIgACW8gxSyzYv3jHcMF8KwQ", "CAACAgIAAxkBAAEGUYVjZlr2YSqSny4AAe-xW6BOy3PG4TQAAi4dAALLUjBLzwABElHL22jlKwQ",
                "CAACAgIAAxkBAAEGUYdjZlr-_JwBKgf1C8sQ5kutBM6z8QACgh0AAuMyMUuBrEpua07jnCsE", "CAACAgIAAxkBAAEGUYljZlsEVkILpOkeKcaHkgfpJ-9qAQACtB8AAhTeOEs6p7O8r5AFjCsE",
                "CAACAgIAAxkBAAEGUYtjZlsKfpTcN8INmr2PntiudaLS4AACiyIAAqtXMUtX0TQaadlc7CsE", "CAACAgIAAxkBAAEGUY1jZlsOAiBsxjKeBuxYqiG5T4waBgACexsAApGGMUtUwOUSaz4jDisE",
                "CAACAgIAAxkBAAEGUY9jZlsTC9fcu09bTuOF0u5yphvcVAACZxwAAoPbOUt_LNVE54A4hSsE", "CAACAgIAAxkBAAEGUZFjZlsYJVCPNEVIHPvTSfPXUt4z2wACbh0AAlaVMEvK1MYkYfRboCsE",
                "CAACAgIAAxkBAAEGUZNjZlsdCSAvBp2zzIX9GXP_noK5kAACOyIAAvS1OEsYSRMV6sPlMisE", "CAACAgIAAxkBAAEGUZVjZlsit9yR_ulq-KjiJ8PBrlDm-gACjh0AAol1MEubhOJJ75vOUSsE",
                "CAACAgIAAxkBAAEGUZdjZlsnt2LADlNf_FcG_IjDwFIatQACsSMAAp1jMUtaMdowqQLgaisE", "CAACAgIAAxkBAAEGUZljZlssBFG9IW6bsHjON3oo4l2nIQACrx8AAgmmMEu90F4EFBP7disE",
                "CAACAgIAAxkBAAEGUZtjZlsyUoj6ODC1gwvFCdpkiqW6lAACnx4AAkPeMUsCP11rBgdYyysE", "CAACAgIAAxkBAAEGUZ1jZls31gzFyKAKzXQeJ4JXy95_OwAC_R4AAu1KMUsThm2ph5n1USsE",
                "CAACAgIAAxkBAAEGUZ9jZls8rNm1OMH4p0PXkIsQz8hzXQAC_h4AAmb4OUt7aOIufDo6JCsE", "CAACAgIAAxkBAAEGUaFjZltBfr15FwdOU-sXFzeKdQhHywACNiEAAsUCMEvdpLKjpL69nisE",
                "CAACAgIAAxkBAAEGUaNjZltFdblKAfiZoMORt_nkFSwLfwACLiAAAqfzMUvzDWTQXXXOsSsE", "CAACAgIAAxkBAAEGUaVjZltKByvVf47X-23uUETxpo53LgAClCEAAoZTMEtDZlz_IiQagisE",
                "CAACAgIAAxkBAAEGUadjZltPEbMLfpD8vtMtCZlp7jBFmwACyh8AAmhQMEvrUW_bWQs5aSsE", "CAACAgIAAxkBAAEGUaljZltU9e_q8BtNU0LWVbZmvFrOcAACZx0AAoCgMUsGD2nkqpX0SysE"
            };

            Random random = new Random();
            int a = random.Next(answers.Length);

            ReplyKeyboardMarkup keyboard1 = new(new[]
            {
                    new KeyboardButton[] { "🎱Узнать ответ🎱"},
            })
            {
                ResizeKeyboard = true
            };

            if (message.Text == "🎱Узнать ответ🎱")
            {
                DateTime time = DateTime.Now;
                await botClient.SendStickerAsync(message.Chat.Id, $"{answers[a]}", replyMarkup: keyboard1);
                Console.WriteLine($"id: {message.Chat.Id} | first name: {message.Chat.FirstName} | last name: {message.Chat.LastName} | Username: {message.Chat.Username} | time: {time} ");
            }
            else if (message.Text == "/start")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Привет, {message.Chat.FirstName}!👋 \n\nЯ магический шар, который подскажет тебе ответ на твой вопрос!🔮 \n\n Жми кнопку снизу и получи ответ👇", replyMarkup: keyboard1);
                return;
            }
            else
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Я только умею давать предсказания...😔", replyMarkup: keyboard1);
            }
        }
        catch (Exception)
        {
            ReplyKeyboardMarkup keyboard1 = new(new[]
            {
                    new KeyboardButton[] { "🎱Узнать ответ🎱"},
            })
            {
                ResizeKeyboard = true
            };
            await botClient.SendTextMessageAsync(message.Chat.Id, $"Я только умею давать предсказания...😔", replyMarkup: keyboard1);
        }
    }
    static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Ошибка телеграм АПИ:\n{apiRequestException.ErrorCode}\n{apiRequestException.Message}",
            _ => exception.ToString()
        };
        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }
}