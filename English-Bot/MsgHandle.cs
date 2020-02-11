using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using VkBotFramework;
using VkBotFramework.Models;
using VkNet.Model.RequestParams;
using VkNet.Model;
using System.Collections.Generic;


namespace English_Bot
{
    public partial class EngBot
    {

        static void NewMessageHandler1(object sender, MessageReceivedEventArgs eventArgs)
        {
            VkBot instanse = sender as VkBot;
            var peerId = eventArgs.Message.PeerId.Value;
            var fromId = eventArgs.Message.FromId.Value;
            var text = eventArgs.Message.Text;
            var answer = "Sorry, there is a empty answer :-(";

            

            if (!users.HasUser(fromId) || users[fromId].regId != 1)
                Registration(eventArgs.Message);
            else
            {
                users[fromId].lastMsg = (text.ToLower(), false, eventArgs.Message.ConversationMessageId.Value);
                if (users[fromId].on_Test)      //������ �� ����� ��� �����
                  return;

                switch (text)
                {
                    default:
                        answer = SendInfo(eventArgs.Message);
                        break;
                }

                instanse.Api.Messages.Send(new MessagesSendParams()
                {
                    RandomId = Environment.TickCount,
                    PeerId = eventArgs.Message.PeerId,
                    Message = answer
                });
            }   
        }

        static string SendInfo(Message msg) => $"{msg.PeerId.Value}, i have captured your message: '{msg.Text}'. its length is {msg.Text.Length}. number of spaces: {msg.Text.Count(x => x == ' ')}";
    }
}