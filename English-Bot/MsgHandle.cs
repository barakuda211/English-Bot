using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using VkBotFramework;
using VkBotFramework.Models;
using VkNet.Model.RequestParams;
using VkNet.Model;
using English_Bot;
using static System.Console;

namespace English_Bot
{
    partial class EngBot
    {
        static void NewMessageHandler(object sender, MessageReceivedEventArgs eventArgs)
        {
            VkBot instanse = sender as VkBot;
            var peerId = eventArgs.Message.PeerId;
            var fromId = eventArgs.Message.FromId;
            var text = eventArgs.Message.Text;
            var answer = "Sorry, there is a empty answer :-(";


            if (!UsersDictionary.HasUser(fromId) || UsersDictionary[fromId].regId!=3)
                answer = Registration(eventArgs.Message);
            else
            {
                switch (text)
                {
                    default:
                        answer = WriteInfo(eventArgs.Message);
                        break;
                }
            }
            instanse.Logger.LogInformation($"new message captured. peerId: {peerId},userId: {fromId}, text: {text}");
            instanse.Api.Messages.Send(new MessagesSendParams()
            {
                RandomId = Environment.TickCount,
                PeerId = eventArgs.Message.PeerId,
                Message = answer
            });
        }

        static string WriteInfo(Message msg) => $"{msg.PeerId.Value}, i have captured your message: '{msg.Text}'. its length is {msg.Text.Length}. number of spaces: {msg.Text.Count(x => x == ' ')}";
    }
}