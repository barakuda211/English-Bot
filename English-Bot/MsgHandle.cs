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
            var text = eventArgs.Message.Text.ToLower();
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
                    return;
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
                            return;
                        }
                        else if (ss[0] == "/description")
                        {
                            if (dictionary.eng_ids.ContainsKey(ss[1]))
                                SendFullWordDescription(fromId, dictionary.eng_ids[ss[1]]);
                            return;
                        }
                    }
                    // ----------------------------------------------------------------------------
                    {
                        switch (text)
                        {
                            case "команды бота":
                            case "/help":
                                answer = "/change_level - сменить свой уровень\n" +
                                         "/my_level - мой уровень\n" +
                                         "/example \'слово\'- примеры использования\n" +
                                         "/crossword - сыграть кроссворд\n" +
                                         "/gallows - сыграть в \"виселицу\"" +
                                         "/easy - простой режим сложности\n" +
                                         // "/medium - средний режим сложности\n" +
                                         "/hard - высокий режим сложности\n" +
                                         "/description \'слово\' - описание слова" +
                                         "/mute - бот не будет присылать слова и проводить тесты\n" + 
                                         "/unmute - бот снова перейдёт в стандартный режим\n" +
                                         "\'слово на русском\' - перевод на английский\n" +
                                         "\'слово на английском\' - перевод на русский\n" + 
                                         "\'текст на английском\' - перевод всех известных боту слов на русский\n";
                                break;
                            case "мой уровень":
                            case "/my_level":
                                answer = "Вы на " + users[fromId].userLevel + " уровне.";
                                break;
                            case "сменить уровень":
                            case "/change_level":
                                ChangingLevel_Start(fromId);
                                return;
                            case "игра кроссворд":
                            case "/crossword":
                                Games.Crossvord_start(fromId);
                                return;
                            case "игра виселица":
                            case "/gallows":
                                Games.Gallows_Start(fromId);
                                return; 
                            case "/example":
                                answer = "А к чему пример то?)";
                                break;
                            case "/description":
                                answer = "Нужно написать и само слово";
                                break; 
                            case "/easy":
                                if (users.Dbase.ContainsKey(fromId))
                                {
                                    users[fromId].mode = Users.Mode.Easy;
                                    answer = "Включен низкий уровень сложности";
                                }
                                break;
                            /*case "/medium":
                                if (users.Dbase.ContainsKey(fromId))
                                {
                                    users[fromId].mode = Users.Mode.Medium;
                                    answer = "Включен средний уровень сложности";
                                }
                                break;*/
                            case "/hard":
                                if (users.Dbase.ContainsKey(fromId))
                                {
                                    users[fromId].mode = Users.Mode.Hard;
                                    answer = "Включен высокий уровень сложности";
                                }
                                break;
                            case "/mute":
                                users[fromId].bot_muted = true;
                                answer = "Бот перешел в режим ожидания\nОн не будет присылать слова и тесты, но по прежнему будет выполнять команды";
                                break;
                            case "/unmute":
                                users[fromId].bot_muted = false;
                                answer = "Боты вернулся к стандартному режиму работы"; 
                                break;
                            case "admin::getсommands":
                                if (adminIDs.Contains(fromId))
                                    answer = "getId, wantTest, getCommands, usersCount";
                                else answer = ACCESS_IS_DENIED;
                                break;
                            case "admin::getid":
                                if (adminIDs.Contains(fromId))
                                    answer = fromId.ToString();
                                else answer = ACCESS_IS_DENIED;
                                break;
                            case "admin::userscount":
                                if (adminIDs.Contains(fromId))
                                    answer = "" + users.Dbase.Count;
                                else answer = ACCESS_IS_DENIED;
                                break;
                            case "admin::wanttest":
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
                                    answer = MultipleTranslation(ss, users[fromId].mode);
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
                Message = answer,
                Keyboard = User.Main_Keyboard.ToMessageKeyboard()
            });
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
            SendMessage(id,text,null,true);
            var x = WaitWordFromUser_with_Comments(id,new string[] {"1","2","3","4","5","-1"},1);
            if (x == "time")
                return;
            user.ChangeLevel(int.Parse(x));
            user.keyb = User.Main_Keyboard;
            SendMessage(id,"Готово!",null,true);
            user.on_Test = false;
            users.Save();
        }

        static void ChangingLevel_Start(long id)
        {
            users[id].on_Test = true;
            users[id].keyb = User.ChangingLevel_Keyboard;
            Thread changing_thread = new Thread(new ParameterizedThreadStart(ChangeLevel));
            changing_thread.Start(id);
        }

        //ждет ответа !определенного! ответа от юзера, 
        //Просит повторить ввод
        static string WaitWordFromUser_with_Comments(long userID, string[] words, int wait_time, string time_error_msg = "Ладно, потом сменим уровень.",string error_msg = "Этого я не ждал!")
        {
            var user = users.GetUser(userID);
            var ind = Timers.IndicatorTimer(wait_time);

            long ident_msg = user.lastMsg.Item3;
            while (true)
            {
                if (ind.x)
                {
                    user.keyb = User.Main_Keyboard;
                    SendMessage(userID, time_error_msg,null,true);
                    return "time";
                }
                if (ident_msg == user.lastMsg.Item3)
                {
                    Thread.Sleep(100);  //ожидание согласия
                    continue;
                }
                ident_msg = user.lastMsg.Item3;
                var text = user.lastMsg.Item1.ToLower();
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
        }

        static string SendInfo(Message msg) => $"{msg.PeerId.Value}, i have captured your message: '{msg.Text}'. its length is {msg.Text.Length}. number of spaces: {msg.Text.Count(x => x == ' ')}";
    }
}