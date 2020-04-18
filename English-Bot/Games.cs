﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Crossword;
using System.Net;
using English_Bot.Properties;

namespace English_Bot
{
    public static class Games
    {
        public static void Crossvord_start(long id)
        {
            EngBot.users[id].on_Test = true;
            Thread crossvord_thread = new Thread(new ParameterizedThreadStart(Crossvord_thread_start));
            crossvord_thread.Start(id);
        }


        static void Crossvord_thread_start(object Idobj)
        {
            long id = (long)Idobj;

            var scw = new SimpleCross(id);
            EngBot.SendMessage(id, "Итак, твоя задача - перевести пронумерованные слова на английский.");
            EngBot.SendMessage(id, "Полученное слово, выделенное жёлтым, требуется перевести на русский.");
            EngBot.SendMessage(id, "Жду ответы в виде:\n'цифра' 'перевод' ");
            SendMessage(scw);
            Wait_normal_answers(scw);
            EngBot.users[id].on_Test = false;
        }

        static bool Wait_normal_answers(SimpleCross scw)
        {
            long userID = scw.id;
            var user = EngBot.users[scw.id];
            string text = user.lastMsg.Item1.ToLower();
            while (true)
            {
                if (text == user.lastMsg.Item1.ToLower())
                {
                    Thread.Sleep(100);
                    continue;
                }
                text = user.lastMsg.Item1.ToLower();
                var words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (text == "/exit")
                {
                    EngBot.SendMessage(userID, @"Ну как хочешь :-\");
                    return false;
                }

                if (words.Length != 2)
                {
                    EngBot.SendMessage(userID, @"Что-то не так с количеством слов :-\");
                    continue;
                }
                int num = -1;
                if (!int.TryParse(words[0], out num))
                {
                    EngBot.SendMessage(userID, "Это точно цифра?)");
                    continue;
                }
                if (num < 1 || num > scw.words.Count)
                {
                    EngBot.SendMessage(userID, "Такого номера я не вижу...");
                    continue;
                }
                if (scw.is_answered[num - 1])
                {
                    EngBot.SendMessage(userID, "Это слово уже отгадано.");
                    continue;
                }
                if (scw.words[num - 1].Item1 != words[1])
                {
                    EngBot.SendMessage(userID, "Ошибочка, попробуй ещё раз.");
                    continue;
                }
                EngBot.SendMessage(userID, "Отлично");
                scw.DrawWord(num - 1);
                SendMessage(scw);

                if (scw.is_all_answered)
                    break;
            }

            EngBot.SendMessage(userID, "Супер, так что же такое "+scw.MainWord.Item1+"?");
            string ans = EngBot.dictionary[scw.MainWord.Item2].rus;
            while (true)
            {
                if (text == user.lastMsg.Item1.ToLower())
                {
                    Thread.Sleep(100);
                    continue;
                }
                text = user.lastMsg.Item1.ToLower();

                if (text == "/exit")
                {
                    EngBot.SendMessage(userID, @"Ну как хочешь :-\");
                    return false;
                }


                if (ans != text)
                {
                    EngBot.SendMessage(userID, "Ошибочка, попробуй ещё раз.");
                    continue;
                }
                break;
            }
            EngBot.SendMessage(userID, "Правильно, поздравляю!");
            return true;
        }

        static void SendMessage(SimpleCross scw)
        {
            long id = scw.id;
            string legend = "";
            for (int i = 0; i < scw.words.Count; i++)
                if (!scw.is_answered[i])
                    legend = legend + scw.legend[i];


            string url = EngBot.bot.Api.Photo.GetMessagesUploadServer(id).UploadUrl;

            var uploader = new WebClient();
            var uploadResponseInBytes = uploader.UploadFile(url, @"users\" + id + @"\cross.jpg");
            var uploadResponseInString = Encoding.UTF8.GetString(uploadResponseInBytes);
            // VKRootObject response = Methods.DeSerializationObjFromStr<VKRootObject>(uploadResponseInString);
            var photos = EngBot.bot.Api.Photo.SaveMessagesPhoto(uploadResponseInString);
            EngBot.bot.Api.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams()
            {
                RandomId = Environment.TickCount64,
                UserId = id,
                Message = legend,
                Attachments = photos
            });
        }

        public static void PlayCrossword(long user_id)
        {
            CrossMaker CM = new CrossMaker();
            //CM.word_list = new List<long>();
            foreach (long word_id in EngBot.users[user_id].unLearnedWords)
            {
                CM.word_list.Add(word_id);
            }
            CM.CrosswordMaker();

            char[,] field = new char[10, 10];
            foreach (var cross in CM.cross)
            {
                if (cross.direction == Cross.Direction.toRight)
                {
                    int x = cross.pos.x;
                    int y = cross.pos.y;
                    foreach (char c in EngBot.dictionary[cross.word_id].eng)
                    {
                        field[x, y] = c;
                        ++x;
                    }
                }
                else
                {
                    int x = cross.pos.x;
                    int y = cross.pos.y;
                    foreach (char c in EngBot.dictionary[cross.word_id].eng)
                    {
                        field[x, y] = c;
                        ++y;
                    }
                }
            }

            string result = "";
            for (int i = 0; i < 10; ++i)
            {
                for (int j = 0; j < 10; ++j)
                    result += field[i, j];
                result += "\n";
            }

            EngBot.SendMessage(user_id, result);
        }

    }
}