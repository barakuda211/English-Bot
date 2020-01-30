using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using VkBotFramework;
using VkBotFramework.Models;
using VkNet.Model.RequestParams;
using VkNet.Model;
using System.Collections.Generic;
using English_Bot.Properties;
using static System.Console;
using System.Threading;

namespace English_Bot
{
    public partial class EngBot
    {

        static void NewMessageHandler(object sender, MessageReceivedEventArgs eventArgs)
        {
            
                var fromId = eventArgs.Message.FromId;
                var text = eventArgs.Message.Text;
                var peerId = eventArgs.Message.PeerId;
                VkBot instanse = sender as VkBot;

                if (users.GetUserVKID(fromId.Value) == null) 
                    users.AddUser(new User(fromId.Value, 0, new HashSet<string>(), new HashSet<long>(), new HashSet<long>()));//��������� ������������, ���� ��� �� ���� � users
                users.GetUserVKID(fromId.Value).lastMsg = (text.ToLower(), false, eventArgs.Message.ConversationMessageId.Value);

            if (text == "�����")
                WaitWordFromUser(fromId.Value, "�����", true);

                WriteLine($"new message captured. peerId: {peerId},userId: {fromId}, text: {text}");
        }


            //���������� ��������� �����
            static void SendMessage(long userID, string message, long[] msgIDs = null)
            {
                bot.Api.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams()
                {
                    RandomId = Environment.TickCount64,
                    UserId = userID,
                    Message = message,
                    ForwardMessages = msgIDs
                });
            WriteLine("����� ����������");
            }

        //���� ������ !�������������! ������ �� �����, 
        //always �������� �� ����� ��������(false - 1 �������, true - ����, ���� ���� �� ������� ������)
        static long WaitWordFromUser(long userID, string word, bool always)
        {
            var user = users.GetUserVKID(userID);
            if (always)
            {
                while (user.lastMsg.Item1 != word || user.lastMsg.Item2 != false) Thread.Sleep(100);  //�������� ��������
                WriteLine("����� �������");
                user.lastMsg.Item2 = true;
            }
            if (!always)
            {
                while (user.lastMsg.Item2 != false) Thread.Sleep(100);
                WriteLine("����� ��������");
                WriteLine(user.lastMsg.Item3);
                user.lastMsg.Item2 = true;
                return (user.lastMsg.Item1 == word ? 0 : user.lastMsg.Item3);
            }
            return 0;//��������
        }

        //������������ ������������ �� !6! ��������� ��������� ������
        static void Testing(object IDobj)
        {
            long userID = users.GetUser((long)IDobj).userId;
            SendMessage(userID, "��� ����� ��������� ���� �� ������ ���������� ����. " +
                                "�� ����� �������������, �� ����������� ����� ������� ���� ���������� ��������� ��������. " +
                                "��� ����� �������: \"�����\". ");
            WaitWordFromUser(userID, "�����", true);

            //���� ���� ������, �� ��� ���� � ����
            var lastLW = users.GetUser((long)IDobj).learnedWords.TakeLast(6);

            var rand = new Random();

            //���� ��� �������� �������
            List<long> msgIDs = new List<long>();

            foreach (long idx in lastLW)//��������� � ���� ����� �� ��������� ��������� �����
            {
                var word = dictionary.GetWord(idx);
                int r = rand.Next(2);
                SendMessage(userID, (r == 0 ? word.eng : word.rus));
                msgIDs.Add(WaitWordFromUser(userID, (r == 1 ? word.eng : word.rus), false));
            }

            WriteLine("����� ��������");
            SendMessage(userID, $"�� �������� �� {msgIDs.FindAll(x => x == 0).Count()} �� {lastLW.Count()}. ");

            //����������� ������ �����

            if (msgIDs.FindAll(x => x == 0).Count() < lastLW.Count())//���� ���� ������
            {
                SendMessage(userID, "�� �������� � ���������:");

                long[] aError = new long[1];
                foreach (var pnt in msgIDs.Zip(lastLW, (x, y) => new { A = x, B = y }))
                {
                    if (pnt.A > 0)//���� �� �������
                    {
                        var temp = dictionary.GetWord(pnt.B);
                        aError[0] = pnt.A;//������ � 1 ����������� ����������, ��� ���� ������ ������
                        SendMessage(userID, $"\n{temp.eng} - {temp.rus}", aError);
                    }
                }
            }
        }

        static void initDict_Testing()
        {
            users.AddUser(new User(223707460, 1, new HashSet<string>(), new HashSet<long>(), new HashSet<long>()));

            dictionary.AddWord(new Word(1, "one", "����", "", "", "", "", 1, null));
            dictionary.AddWord(new Word(2, "two", "���", "", "", "", "", 1, null));
            dictionary.AddWord(new Word(3, "three", "���", "", "", "", "", 1, null));
            dictionary.AddWord(new Word(4, "four", "������", "", "", "", "", 1, null));
            dictionary.AddWord(new Word(5, "five", "����", "", "", "", "", 1, null));
            dictionary.AddWord(new Word(6, "six", "�����", "", "", "", "", 1, null));
            dictionary.AddWord(new Word(7, "seven", "����", "", "", "", "", 1, null));
            users.GetUser(1).learnedWords.Add(1);
            users.GetUser(1).learnedWords.Add(2);

            long temp = 1;
            Thread testingThread = new Thread(new ParameterizedThreadStart(Testing));
            testingThread.Start(temp);
        }

        static void NewMessageHandler1(object sender, MessageReceivedEventArgs eventArgs)
        {
            VkBot instanse = sender as VkBot;
            var peerId = eventArgs.Message.PeerId;
            var fromId = eventArgs.Message.FromId;
            var text = eventArgs.Message.Text;
            var answer = "Sorry, there is a empty answer :-(";

            switch (text)
            {
                default: answer = SendInfo(eventArgs.Message);
                        break;
            }
            instanse.Logger.LogInformation($"new message captured. peerId: {peerId},userId: {fromId}, text: {text}");
            instanse.Api.Messages.Send(new MessagesSendParams()
            {
                RandomId = Environment.TickCount,
                PeerId = eventArgs.Message.PeerId,
                Message = answer
            });
        }

        static string SendInfo(Message msg) => $"{msg.PeerId.Value}, i have captured your message: '{msg.Text}'. its length is {msg.Text.Length}. number of spaces: {msg.Text.Count(x => x == ' ')}";
    }
}