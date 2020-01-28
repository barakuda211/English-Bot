using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using VkBotFramework;
using VkBotFramework.Models;
//using VkBotFramework.Examples;
using VkNet.Model.RequestParams;
using English_Bot.Properties;
using static System.Console;

namespace English_Bot
{
    class EngBot
    {
        static VkBot bot = new VkBot(Resources.AccessToken, Resources.groupUrl);
        static void Main(string[] args)
        {
            //ILoggerFactory loggerFactory = new LoggerFactory().AddConsole();
            //ILogger<VkBot> logger = loggerFactory.CreateLogger<VkBot>();

            //ExampleSettings settings = ExampleSettings.TryToLoad(logger);

            SendMessage(210036813, "hi");

            bot.OnMessageReceived += NewMessageHandler;

            bot.Start();

            WriteLine("Bot started!");
            ReadLine();
        }
        static void NewMessageHandler(object sender, MessageReceivedEventArgs eventArgs)
        {

            VkBot instanse = sender as VkBot;
            var peerId = eventArgs.Message.PeerId;
            var fromId = eventArgs.Message.FromId;
            var text = eventArgs.Message.Text;

            instanse.Logger.LogInformation($"new message captured. peerId: {peerId},userId: {fromId}, text: {text}");
            instanse.Api.Messages.Send(new MessagesSendParams()
            {
                RandomId = Environment.TickCount,
                PeerId = eventArgs.Message.PeerId,
                Message =
                    $"{fromId.Value}, i have captured your message: '{text}'. its length is {text.Length}. number of spaces: {text.Count(x => x == ' ')}"
            });
        }
        static void SendMessage(long userID, string message)
        {
            bot.Api.Messages.Send(new MessagesSendParams()
            {
                UserId = userID,
                Message = message,
                RandomId = Environment.TickCount
            });
        }
    }
}
