using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using VkBotFramework;
using VkBotFramework.Models;
using VkNet.Model.RequestParams;
using VkNet.Model;
using System.Collections.Generic;
using System.Threading;
using Crossword;

namespace English_Bot
{
    public partial class EngBot
    {

        static void NewMessageHandler(object sender, MessageReceivedEventArgs eventArgs)
        {
            VkBot instanse = sender as VkBot;
            var peerId = eventArgs.Message.PeerId.Value;
            var fromId = eventArgs.Message.FromId.Value;
            var text = eventArgs.Message.Text;
            var answer = "Sorry, there is a empty answer :-(";

            /*
            if (text == @"\cross")
            {
                Games.PlayCrossword(peerId);
                return;
            }
            */

            if (text != null && text.Length != 0)
            {
                if (!users.HasUser(fromId) || users[fromId].regId != 1)
                {
                    Registration(eventArgs.Message);
                    goto Finn;
                }
                else
                {
                    users[fromId].lastMsg = (text.ToLower(), false, eventArgs.Message.ConversationMessageId.Value);
                        
                    if (users[fromId].on_Test)
                        return;


                    switch (text)
                    {
                        case "������� ����":
                        case "/commands":
                            answer = "/change_level - �������� ���� �������\n" +
                                     "/my_level - ��� �������\n" +
                                     "/crossword - ������� ���������\n"+
                                     "\'����� �� �������\' - ������� �� ����������\n" +
                                     "\'����� �� ����������\' - ������� �� �������";
                            break;
                        case "��� �������":
                        case "/my_level":
                            answer = "�� �� " + users[fromId].userLevel + " ������.";
                            break;
                        case "�������� �������":
                        case "/change_level":
                            ChangingLevel_Start(fromId);
                            return;
                        default:
                            // answer = SendInfo(eventArgs.Message);
                            answer = Translation(text);
                            // if (text[0] > 'A' && text[0] < 'z' && dictionary.GetEngWordId(text) != -1)
                            //SendPicture(eventArgs.Message.PeerId.Value, dictionary.GetEngWordIds(text).ElementAt(0));
                            //SendFullWordDescription(eventArgs.Message.PeerId.Value, text);
                            break;
                    }
                }
            }

            instanse.Api.Messages.Send(new MessagesSendParams()
            {
                RandomId = Environment.TickCount,
                PeerId = eventArgs.Message.PeerId,
                Message = answer
            });
        Finn:;
        }



        static void ChangeLevel(object Idobj)
        {
            long id = (long)Idobj;
            var user = users[id];
            int userlevel = user.userLevel;
            var text =     "�� �� "+userlevel+" ������ ��������.\n"+
                           "�� ��������? �������� ���� �� ���������:\n"+
                           "1 - ���� � ����� ��������\n"+
                           "2 - ���������� ����������\n"+
                           "3 - �������� �� �����������\n"+
                           "4 - ��������� � �����������\n"+
                           "5 - ��������� �������� �����\n"+
                           "-1 - �� ����� ����������";
            SendMessage(id,text);
            var x = WaitWordFromUser_with_Comments(id,new string[] {"1","2","3","4","5","-1"});
            user.ChangeLevel(int.Parse(x));
             SendMessage(id,"������!");
            user.on_Test = false;
            users.Save();
        }

        static void ChangingLevel_Start(long id)
        {
            users[id].on_Test = true;
            Thread changing_thread = new Thread(new ParameterizedThreadStart(ChangeLevel));
            changing_thread.Start(id);
        }

        //���� ������ !�������������! ������ �� �����, 
        //������ ��������� ����
        static string WaitWordFromUser_with_Comments(long userID, string[] words, string error_msg = "����� � �� ����!")
        {
            var user = users.GetUser(userID);
            string text = user.lastMsg.Item1.ToLower();
            while (true)
            {
                if (text != user.lastMsg.Item1.ToLower())
                {
                    text = user.lastMsg.Item1.ToLower();
                    foreach (var w in words)
                    {
                        if (w == text)
                        {
                            user.lastMsg.Item2 = true;
                            return w;
                        }
                    }
                    SendMessage(userID,error_msg);
                }
                Thread.Sleep(100);
            }
        }

        static string SendInfo(Message msg) => $"{msg.PeerId.Value}, i have captured your message: '{msg.Text}'. its length is {msg.Text.Length}. number of spaces: {msg.Text.Count(x => x == ' ')}";
    }
}