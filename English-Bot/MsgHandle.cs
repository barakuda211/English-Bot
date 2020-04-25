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
        const string ACCESS_IS_DENIED = "ACCESS IS DENIED";

        static void NewMessageHandler(object sender, MessageReceivedEventArgs eventArgs)
        {
            VkBot instanse = sender as VkBot;
            var peerId = eventArgs.Message.PeerId.Value;
            var fromId = eventArgs.Message.FromId.Value;
            var text = eventArgs.Message.Text;
            var answer = "Sorry, there is an empty answer :-(";

            /*
            if (text == @"\cross")
            {
                Games.PlayCrossword(peerId);
                return;
            }
            */

            // if audio mess then regognize it

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

                    var ss = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (ss.Length == 2)
                    {
                        if (ss[0] == "/example")
                        {
                            foreach (var s in GetSentenceExemples(ss[1]))
                            {
                                SendMessage(fromId, s);
                            }
                            return;
                        }
                        else if (ss[0] == "/sound")
                        {
                            if (dictionary.eng_ids.ContainsKey(ss[1]))
                                SendSound(fromId, dictionary.eng_ids[ss[1]]);
                        }
                    }
                    // ----------------------------------------------------------------------------
                    {
                        switch (text)
                        {
                            case "Команды бота":
                            case "/commands":
                                answer = "/change_level - изменить свой уровень\n" +
                                         "/my_level - мой уровень\n" +
                                         "/example \'слово\'- примеры использования\n" +
                                         "/crossword - сыграть кроссворд\n" +
                                         "\'слово на русском\' - перевод на английский\n" +
                                         "\'слово на английском\' - перевод на русский\n";
                                break;
                            case "Мой уровень":
                            case "/my_level":
                                answer = "Вы на " + users[fromId].userLevel + " уровне.";
                                break;
                            case "Изменить уровень":
                            case "/change_level":
                                ChangingLevel_Start(fromId);
                                return;
                            case "Игра кроссворд":
                            case "/crossword":
                                Games.Crossvord_start(fromId);
                                return;
                            case "/example":
                                answer = "А к чему пример то?)";
                                break;
                            case "admin::getCommands":
                                if (adminIDs.Contains(fromId))
                                    answer = "getId, wantTest, getCommands...";
                                else answer = ACCESS_IS_DENIED;
                                return;
                            case "admin::getId":
                                if (adminIDs.Contains(fromId))
                                    answer = fromId.ToString();
                                else answer = ACCESS_IS_DENIED;
                                break;
                            case "admin::wantTest":
                                if (adminIDs.Contains(fromId))
                                {
                                    users[fromId].on_Test = true;
                                    Testing_Start(fromId);
                                    return;
                                }
                                else 
                                    answer = ACCESS_IS_DENIED;
                                break;
                            default:
                                if (ss.Length == 1)
                                    answer = Translation(text);
                                else
                                    answer = MultipleTranslation(ss);
                                // answer = SendInfo(eventArgs.Message);
                                // if (text[0] > 'A' && text[0] < 'z' && dictionary.GetEngWordId(text) != -1)
                                //SendPicture(eventArgs.Message.PeerId.Value, dictionary.GetEngWordIds(text).ElementAt(0));
                                //SendFullWordDescription(eventArgs.Message.PeerId.Value, text);
                                break;
                        }
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
            var text =     "Вы на "+userlevel+" уровне изучения.\n"+
                           "Не нравится? Выберите один из следующих:\n"+
                           "1 - учил в школе немецкий\n"+
                           "2 - прогуливал английский\n"+
                           "3 - хорошист по английскому\n"+
                           "4 - занимался с репетитором\n"+
                           "5 - уверенный носитель языка\n"+
                           "-1 - ну точно иностранец";
            SendMessage(id,text);
            var x = WaitWordFromUser_with_Comments(id,new string[] {"1","2","3","4","5","-1"});
            user.ChangeLevel(int.Parse(x));
             SendMessage(id,"Готово!");
            user.on_Test = false;
            users.Save();
        }

        static void ChangingLevel_Start(long id)
        {
            users[id].on_Test = true;
            Thread changing_thread = new Thread(new ParameterizedThreadStart(ChangeLevel));
            changing_thread.Start(id);
        }

        //ждет ответа !определенного! ответа от юзера, 
        //Просит повторить ввод
        static string WaitWordFromUser_with_Comments(long userID, string[] words, string error_msg = "Этого я не ждал!")
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